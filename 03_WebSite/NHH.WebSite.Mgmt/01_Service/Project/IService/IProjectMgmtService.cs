using NHH.Models.Project;
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
    public interface IProjectMgmtService
    {
        /// <summary>
        /// 创建项目
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int Create(ProjectInfoModel model);

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="model"></param>
        void Edit(ProjectInfoModel model);

        /// <summary>
        /// 保存项目详细信息
        /// </summary>
        /// <param name="model"></param>
        void SaveDetailInfo(ProjectInfoModel model);

        /// <summary>
        /// 添加楼宇
        /// </summary>
        /// <param name="model"></param>
        int AddBuilding(BuildingDetailInfoModel model);

        /// <summary>
        /// 更新楼宇
        /// </summary>
        /// <param name="model"></param>
        void UpdateBuilding(BuildingDetailInfoModel model);

        /// <summary>
        /// 添加楼层
        /// </summary>
        /// <param name="model"></param>
        void AddFloor(FloorDetailModel model);

        /// <summary>
        /// 更新楼层
        /// </summary>
        /// <param name="model"></param>
        void UpdateFloor(FloorDetailModel model);

        /// <summary>
        /// 编辑项目基础信息
        /// </summary>
        /// <param name="model"></param>
        void EditProjectInfoBasic(ProjectBasicInfoModel model);
    }
}
