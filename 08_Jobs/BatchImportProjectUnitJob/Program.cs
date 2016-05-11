using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;

namespace BatchImportProjectUnitJob
{
    class Program
    {
        static void Main(string[] args)
        {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;

            //查看是否有需要处理的文件
            var preProcessFolder = ConfigurationManager.AppSettings["PreProcessFolder"].ToString();
            var processingFolder = baseDir + "ProcessingFolder/";
            var processedFolder = baseDir + "ProcessedFolder/";

            var preDir = new DirectoryInfo(preProcessFolder);
            if (!preDir.Exists)
                return;

            var preFiles = preDir.GetFiles("*.xls");

            int number = preFiles.Length;
            if (number == 0)
                return;

            string fileName = string.Empty;
            string destFileName = string.Empty;

            for (int n = 0; n < number; n++)
            {
                fileName = preFiles[n].Name;
                destFileName = processingFolder + fileName;
                preFiles[n].MoveTo(destFileName);

                try
                {
                    Console.WriteLine(string.Format("开始处理{0}", fileName));
                    //开始处理
                    ProcessFile(destFileName, processedFolder);
                    Console.WriteLine(string.Format("已处理{0}", fileName));

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

        }

        /// <summary>
        /// 处理Excel文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="folderName"></param>
        private static void ProcessFile(string fileName, string folderName)
        {
            var dataTable = ReadFileData(fileName);
            int number = dataTable.Rows.Count;
            if (number == 0)
            {
                return; 
            }

            for (int n = 0; n < number; n++)
            {
                var row = dataTable.Rows[n];
                try
                {
                    SaveProjectUnit(row);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                } 
            }

            var fileInfo = new FileInfo(fileName);
            //移到已处理文件夹
            var destFileName = folderName + fileInfo.Name;
            fileInfo.MoveTo(destFileName);
        }

        /// <summary>
        /// 保存商铺
        /// </summary>
        /// <param name="row"></param>
        private static void SaveProjectUnit(DataRow row)
        {
            var projectName = row["项目名称"].ToString();
            var buildingName = row["楼宇"].ToString();
            var floor = row["楼层"].ToString();
            var unitNumber = row["商铺编号"].ToString();
            var unitArea = row["建筑面积"].ToString();
            var internalUnitArea = row["计租面积"].ToString();
            var floorMapFileName = row["平面图位置"].ToString();
            var unitType = row["类型"].ToString();

            #region 较验

            if (string.IsNullOrEmpty(projectName) || projectName.Length == 0)
            {
                Console.WriteLine("项目名称为空");
                return;
            }

            if (string.IsNullOrEmpty(buildingName) || buildingName.Length == 0)
            {
                Console.WriteLine("楼宇名称为空");
                return;
            }

            if (string.IsNullOrEmpty(floor) || floor.Length == 0)
            {
                Console.WriteLine("楼层为空");
                return;
            }

            if (string.IsNullOrEmpty(unitNumber) || unitNumber.Length == 0)
            {
                Console.WriteLine("商铺编号为空");
                return;
            }

            if (string.IsNullOrEmpty(unitArea) || unitArea.Length == 0)
            {
                Console.WriteLine("建筑面积为空");
                return;
            }

            if (string.IsNullOrEmpty(internalUnitArea) || internalUnitArea.Length == 0)
            {
                Console.WriteLine("计租面积为空");
                return;
            }

            if (string.IsNullOrEmpty(unitType) || unitType.Length == 0)
            {
                Console.WriteLine("类型为空");
                return;
            }

            #endregion

            var strCmd = string.Format(@"Exec SP_Job_Import_ProjectUnit 
                                              @ProjectName = N'{0}',
                                              @BuildingName = N'{1}',
                                              @FloorNumber = {2},
                                              @UnitNumber = N'{3}',
                                              @UnitArea = {4},
                                              @InternalUnitArea = {5},
                                              @FloorMapFileName = N'{6}',
                                              @UnitType = N'{7}';",
                                    projectName,
                                    buildingName,
                                    floor,
                                    unitNumber,
                                    unitArea,
                                    internalUnitArea,
                                    floorMapFileName,
                                    unitType);

            var connStr = ConfigurationManager.ConnectionStrings["NHHConn"].ToString();
            var conn = new System.Data.SqlClient.SqlConnection(connStr);
            var cmd = new System.Data.SqlClient.SqlCommand(strCmd, conn);
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            var result = cmd.ExecuteScalar();
            if (conn.State != ConnectionState.Closed)
            {
                conn.Close();
            }
        }

        /// <summary>
        /// 读取Excel数据
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private static DataTable ReadFileData(string fileName)
        {
            var connStr = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1\"", fileName);
            var conn = new System.Data.OleDb.OleDbConnection(connStr);
            var cmd = new System.Data.OleDb.OleDbCommand("Select * From [Sheet1$]", conn);
            var adapter = new System.Data.OleDb.OleDbDataAdapter(cmd);
            var table = new DataTable();
            adapter.Fill(table);
            return table; 
        }
    }
}
