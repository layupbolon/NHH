using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHH.Models.Common;
using NHH.Models.Official.ADPosition;
using NHH.Models.Official.Common;
using NHH.Models.Official.Feedback;
using NHH.Models.Official.Kiosk;
using NHH.Models.Official.Project;
using NHH.Models.Official.Unit;

namespace NHH.Service.Official.IService
{
    public interface INHHCGService
    {
        #region Common
        /// <summary>
        /// 获取官网楼层下拉列表
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        List<SelectListItemInfo> GetNHHCGFloorDropDownList(NHHCGTypeEnum type);

        /// <summary>
        /// 获取官网楼别下拉列表
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        List<SelectListItemInfo> GetNGGCGBuildingDropDownList(NHHCGTypeEnum type);

        /// <summary>
        /// 获取官网的项目下拉列表
        /// </summary>
        /// <returns></returns>
        List<SelectListItemInfo> GetNHHCGProjectDropDownList();

        /// <summary>
        /// 获取官网的业态类型下拉列表
        /// </summary>
        /// <returns></returns>
        List<SelectListItemInfo> GetNHHCGBizTypeDropDownList();
        #endregion Common

        #region NHHCGProject
        /// <summary>
        /// 获取项目列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        NHHCGProjectListModel GetNHHCGProjectList(NHHCGProjectQueryModel queryInfo);

        /// <summary>
        /// 获取项目
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        NHHCGProjectModel GetNHHCGProject(int projectId);

        /// <summary>
        /// 添加官网项目
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool AddNHHCGProject(NHHCGProjectModel model);

        /// <summary>
        /// 编辑官网项目
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool UpdateNHHCGProject(NHHCGProjectModel model);

        /// <summary>
        /// 获取官网项目列表
        /// </summary>
        /// <returns></returns>
        List<NHHCGProjectModel> GetProjectList();

        #endregion NHHCGProject

        #region NHHCGUnit
        /// <summary>
        /// 获取铺位列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        NHHCGUnitListModel GetNHHCGUnitList(NHHCGUnitQueryModel queryInfo);

        /// <summary>
        /// 获取铺位
        /// </summary>
        /// <param name="kioskId"></param>
        /// <returns></returns>
        NHHCGUnitModel GetNHHCGUnit(int kioskId);

        /// <summary>
        /// 添加官网铺位
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool AddNHHCGUnit(NHHCGUnitModel model);

        /// <summary>
        /// 编辑官网铺位
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool UpdateNHHCGUnit(NHHCGUnitModel model);
        #endregion NHHCGUnit

        #region NHHCGKiosk
        /// <summary>
        /// 获取多经点位列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        NHHCGKioskListModel GetNHHCGKioskList(NHHCGKioskQueryModel queryInfo);

        /// <summary>
        /// 获取多经点位
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        NHHCGKioskModel GetNHHCGKiosk(int kioskId);

        /// <summary>
        /// 添加官网多经点位
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool AddNHHCGKiosk(NHHCGKioskModel model);

        /// <summary>
        /// 编辑官网多经点位
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool UpdateNHHCGKiosk(NHHCGKioskModel model);
        #endregion NHHCGKiosk

        #region NHHCGADPosition
        /// <summary>
        /// 获取广告位列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        NHHCGADPositionListModel GetNHHCGADPositionList(NHHCGADPositionQueryModel queryInfo);

        /// <summary>
        /// 获取广告位
        /// </summary>
        /// <param name="adPositionId"></param>
        /// <returns></returns>
        NHHCGADPositionModel GetNHHCGADPosition(int adPositionId);

        /// <summary>
        /// 添加官网广告位
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool AddNHHCGADPosition(NHHCGADPositionModel model);

        /// <summary>
        /// 编辑官网广告位
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool UpdateNHHCGADPosition(NHHCGADPositionModel model);
        #endregion NHHCGADPosition

        #region NHHCGPic

        /// <summary>
        /// 添加图片
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool AddNHHCGPic(NHHCGPicModel model);

        /// <summary>
        /// 获取图片列表
        /// </summary>
        /// <param name="refID"></param>
        /// <param name="type">1店铺 2广告位 3多经点位</param>
        /// <returns></returns>
        NHHCGPicListModel GetPicList(int refID, int type);

        /// <summary>
        /// 删除图片
        /// </summary>
        /// <param name="picId"></param>
        /// <returns></returns>
        bool DeletePic(int picId);

        #endregion NHHCGPic

        #region NHHFeedback

        /// <summary>
        /// 获取官网反馈意见列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        FeedbackListModel GetFeedbackListModel(FeedbackQueryModel queryInfo);

        /// <summary>
        /// 获取官网反馈意见明细
        /// </summary>
        /// <param name="feedbackID"></param>
        /// <returns></returns>
        FeedbackModel GetFeedbackModel(int feedbackID);

        /// <summary>
        /// 处理反馈意见
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool ProcessFeedback(FeedbackModel model);
        
        #endregion
    }
}
