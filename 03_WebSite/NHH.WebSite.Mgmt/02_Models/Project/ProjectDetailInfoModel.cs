using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Project
{
    /// <summary>
    /// 项目详细信息
    /// </summary>
    public class ProjectDetailInfoModel : ProjectInfo
    {
        /// <summary>
        /// 效果图列表
        /// </summary>
        public List<string> RenderingFileList
        {
            get
            {
                var list = new List<string>();
                if (RenderingFileName != null) 
                {
                    if (RenderingFileName.EndsWith(","))
                    {
                        RenderingFileName = RenderingFileName.Substring(0, RenderingFileName.Length - 1);
                        list = RenderingFileName.Split(new char[] { ',' }).ToList();
                    }
                }
                return list;
            }
        }
    }
}
