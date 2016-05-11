using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Configuration;
using NhhDataImport.Process;

namespace NhhDataImport
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 选择文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.Filter = "Excel文件|*.xlsx";
            this.openFileDialog1.ShowDialog();
        }

        /// <summary>
        /// 选中文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            this.txtFileName.Text = this.openFileDialog1.FileName;
            var list = ExcelHelper.ExcelSheetName(this.txtFileName.Text);
            if (list == null || list.Count == 0)
                return;
            this.cbModule.Items.Clear();
            foreach (var item in list)
            {
                this.cbModule.Items.Add(item.Replace("$", ""), false);
            }
            this.cbModule.Enabled = true;
            this.btnCheck.Enabled = true;
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            int num = 0;
            for (int n = 0; n < this.cbModule.Items.Count; n++)
            {
                if (this.cbModule.GetItemChecked(n))
                {
                    num += 1;
                }
            }
            if (num == 0)
            {
                MessageBox.Show("请选择要处理的模块");
                return;
            }
            this.btnProcess.Enabled = false;
            this.cbModule.Enabled = false;
            this.backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            var sheetNames = new List<string>();

            for (int n = 0; n < this.cbModule.Items.Count; n++)
            {
                if (this.cbModule.GetItemChecked(n))
                {
                    sheetNames.Add(this.cbModule.GetItemText(this.cbModule.Items[n]));
                }
            }
            DoWork(this.backgroundWorker1, sheetNames);
        }

        /// <summary>
        /// 处理
        /// </summary>
        /// <param name="worker"></param>
        /// <param name="sheetNames"></param>
        /// <returns></returns>
        private bool DoWork(BackgroundWorker worker, List<string> sheetNames)
        {
            if (worker.CancellationPending)
            {
                worker.ReportProgress(1, "操作被用户申请中止");
                return false;
            }
            worker.WorkerReportsProgress = true;
            worker.ReportProgress(1, "开始复制文件...");

            //批次号
            var batchNumber = DateTime.Now.ToString("yyMMddhhmmssfff");

            //复制过来
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var processing = Path.Combine(baseDir, "Processing/");
            FileInfo fileInfo = new FileInfo(txtFileName.Text);
            var newFileName = string.Format("{0}_{1}{2}", fileInfo.Name.Replace(fileInfo.Extension, ""), batchNumber, fileInfo.Extension);
            var newFilePath = processing + newFileName;
            File.Copy(txtFileName.Text, newFilePath);

            worker.ReportProgress(1, string.Format("文件复制到{0}", newFilePath));

            worker.ReportProgress(1, "开始导入数据...");

            //开始处理
            NhhDataProcess.Process(newFilePath, sheetNames, worker);

            worker.ReportProgress(1, "数据导入完成，正在整理文件...");
            //处理完成
            var processed = Path.Combine(baseDir, "Processed/");
            fileInfo = new FileInfo(newFilePath);
            File.Move(newFilePath, processed + fileInfo.Name);
            worker.ReportProgress(1, string.Format("文件复制到{0}", processing + fileInfo.Name));

            return true;
        }

        /// <summary>
        /// 变更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            string logType = string.Empty;
            if (e.ProgressPercentage == 1)
            {
                logType = "Info";
                rtxtLog.SelectionColor = Color.Black;
            }
            else if (e.ProgressPercentage == 2)
            {
                logType = "Schedule";
                rtxtLog.SelectionColor = Color.Blue;
            }
            else if (e.ProgressPercentage == 10)
            {
                logType = "Error";
                rtxtLog.SelectionColor = Color.DarkOrange;
            }
            else if (e.ProgressPercentage == 20)
            {
                logType = "Exception";
                rtxtLog.SelectionColor = Color.Red;
            }
            else if (e.ProgressPercentage == 100)
            {
                logType = "Success";
                rtxtLog.SelectionColor = Color.Green;
                rtxtLog.SelectionFont = new Font("宋体", 12, FontStyle.Bold);
            }
            rtxtLog.AppendText(string.Format("{0} [{1}]{2}", DateTime.Now.ToString("HH:mm:ss"), logType, e.UserState.ToString()));
            rtxtLog.AppendText("\r\n");
        }

        /// <summary>
        /// 完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            txtFileName.Text = "";

            //保存日志
            var logs = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs/");
            var logPath = string.Format("{0}{1}.txt", logs, DateTime.Now.ToString("yyMMddHHmmssfff"));
            rtxtLog.SaveFile(logPath, RichTextBoxStreamType.PlainText);
            MessageBox.Show("数据导入完成");
        }

        /// <summary>
        /// 打开模板文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ReProcess/NHH商场管理系统_数据导入模板.xlsx");
            if (File.Exists(filePath))
            {
                System.Diagnostics.Process.Start(filePath);
            }
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            int num = 0;
            for (int n = 0; n < this.cbModule.Items.Count; n++)
            {
                if (this.cbModule.GetItemChecked(n))
                {
                    num += 1;
                }
            }
            if (num == 0)
            {
                MessageBox.Show("请选择要校验的模块");
                return;
            }

            this.backgroundWorker2.RunWorkerAsync();
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            var sheetNames = new List<string>();

            for (int n = 0; n < this.cbModule.Items.Count; n++)
            {
                if (this.cbModule.GetItemChecked(n))
                {
                    sheetNames.Add(this.cbModule.GetItemText(this.cbModule.Items[n]));
                }
            }
            DoWork2(this.backgroundWorker2, sheetNames);
        }

        /// <summary>
        /// 校验
        /// </summary>
        /// <param name="worker"></param>
        /// <param name="sheetNames"></param>
        /// <returns></returns>
        private bool DoWork2(BackgroundWorker worker, List<string> sheetNames)
        {
            if (worker.CancellationPending)
            {
                worker.ReportProgress(1, "操作被用户申请中止");
                return false;
            }
            worker.WorkerReportsProgress = true;

            //开始处理
            NhhDataProcess.Check(txtFileName.Text, sheetNames, worker);

            worker.ReportProgress(1, "数据校验完成！");
            
            return true;
        }

        private void backgroundWorker2_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            string logType = string.Empty;
            if (e.ProgressPercentage == 1)
            {
                logType = "Info";
                rtxtLog.SelectionColor = Color.Black;
            }
            else if (e.ProgressPercentage == 2)
            {
                logType = "Schedule";
                rtxtLog.SelectionColor = Color.Blue;
            }
            else if (e.ProgressPercentage == 10)
            {
                logType = "Error";
                rtxtLog.SelectionColor = Color.DarkOrange;
            }
            else if (e.ProgressPercentage == 20)
            {
                logType = "Exception";
                rtxtLog.SelectionColor = Color.Red;
            }
            else if (e.ProgressPercentage == 100)
            {
                logType = "Success";
                rtxtLog.SelectionColor = Color.Green;
                rtxtLog.SelectionFont = new Font("宋体", 12, FontStyle.Bold);
            }
            rtxtLog.AppendText(string.Format("{0} [{1}]{2}", DateTime.Now.ToString("HH:mm:ss"), logType, e.UserState.ToString()));
            rtxtLog.AppendText("\r\n");
        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("数据校验完成");
            this.btnProcess.Enabled = true;
            this.cbModule.Enabled = true;
        }

        /// <summary>
        /// 合同附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAppendix_Click(object sender, EventArgs e)
        {
            this.btnAppendix.Enabled = false;
            this.backgroundWorker3.RunWorkerAsync();
        }

        private void backgroundWorker3_DoWork(object sender, DoWorkEventArgs e)
        {
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            basePath = Path.Combine(basePath, "ReProcess");
            DirectoryInfo dir = new DirectoryInfo(basePath);

            FileInfo[] files = dir.GetFiles("*.pdf");
            DoWork3(this.backgroundWorker3, files);

            files = dir.GetFiles("*.tif");
            DoWork3(this.backgroundWorker3, files);
            files = dir.GetFiles("*.jpg");
            DoWork3(this.backgroundWorker3, files);
            files = dir.GetFiles("*.jpeg");
            DoWork3(this.backgroundWorker3, files);
            files = dir.GetFiles("*.rar");
            DoWork3(this.backgroundWorker3, files);
        }

        /// <summary>
        /// 校验
        /// </summary>
        /// <param name="worker"></param>
        /// <param name="files"></param>
        /// <returns></returns>
        private bool DoWork3(BackgroundWorker worker, FileInfo[] files)
        {
            if (files == null || files.Length == 0) return true;
            worker.WorkerReportsProgress = true;

            //开始处理
            var process = new ContractAppendixProcess();
            var table = SqlHelper.ExecuteQuery(@"SELECT [ContractID],[ContractCode],MerchantID FROM [dbo].[Contract](Nolock)");
            
            foreach (FileInfo file in files)
            {
                process.ImportAppendix(table, file, worker);
            }
            return true;
        }

        private void backgroundWorker3_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            rtxtLog.AppendText(string.Format("{0} {1}", DateTime.Now.ToString("HH:mm:ss"), e.UserState.ToString()));
            rtxtLog.AppendText("\r\n");
        }

        private void backgroundWorker3_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("租约附件导入完成");
            this.btnAppendix.Enabled = true;
        }
    }
}
