using NHH.Entities;
using NHH.Models.Common;
using NHH.Models.Project.ProjectInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Project.IService
{
    /// <summary>
    /// 项目管理服务接口
    /// </summary>
    public interface IProjectManageService
    {
        /// <summary>
        /// 获取项目列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        ProjectListModel GetProjectList(ProjectListQueryInfo queryInfo);

        List<Entities.Project> GetProjectResultById(int id);       
        
        /// <summary>
        /// autocomplete不全项目名称
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        List<Entities.Project> GetAutoProjectName(string name);

        List<ProjectInfo> GetProjectInfoFromPro(ProjectInfo project, PageInfoTmp pagingInfo);        
    }
}
