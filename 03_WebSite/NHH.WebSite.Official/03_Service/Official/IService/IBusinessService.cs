using System.Collections.Generic;
using NHH.Models.Common;
using NHH.Models.Official;

namespace NHH.Service.Official.IService
{
    public interface IBusinessService
    {
        /// <summary>
        /// 获取项目列表
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        string GetProjectList(int? projectID);

        /// <summary>
        /// 获取顶部banner
        /// </summary>
        /// <returns></returns>
        string GetTopBanner();

        /// <summary>
        /// 获取banner数量
        /// </summary>
        /// <returns></returns>
        int GetBannerCount();

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

        /// <summary>
        /// 获取官网面积范围下拉列表
        /// </summary>
        /// <returns></returns>
        List<SelectListItemInfo> GetNHHCGAreaDropDownList();

        List<SelectListItemInfo> GetNHHCGFloorDropDownList(NHHCGTypeEnum type);

        /// <summary>
        /// 获取空铺信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        List<UnitInfo> GetUnitInfos(UnitQueryInfo info);

        /// <summary>
        /// 获取相关铺位信息
        /// （除去指定铺位）
        /// </summary>
        /// <param name="info"></param>
        /// <param name="unitID"></param>
        /// <returns></returns>
        List<UnitInfo> GetUnitInfos(UnitQueryInfo info,int unitID);

        /// <summary>
        /// 获取商铺信息
        /// </summary>
        /// <param name="unitID"></param>
        /// <returns></returns>
        UnitInfo GetUnitInfo(int unitID);

        List<SelectListItemInfo> GetNHHCGRegionDropDownList();

        List<SelectListItemInfo> GetNHHCGBusinessScopeDropDownList();

        List<KioskInfo> GetKioskInfos(KioskQueryInfo info);

        List<KioskInfo> GetKioskInfos(KioskQueryInfo info, int kioskID);

        KioskInfo GetKioskInfo(int kioskID);

        List<SelectListItemInfo> GetNHHCGAdPointDropDownList();

        List<SelectListItemInfo> GetNHHCGElectricityTypeDropDownList();

        List<ADPositionInfo> GetADPositionInfos(ADPositionQueryInfo info);

        List<ADPositionInfo> GetADPositionInfos(ADPositionQueryInfo info, int ADPositionID);

        ADPositionInfo GetAdPositionInfo(int ADPositionID);

        List<SelectListItemInfo> GetNGGCGBuildingDropDownList(NHHCGTypeEnum type);
    }
}
