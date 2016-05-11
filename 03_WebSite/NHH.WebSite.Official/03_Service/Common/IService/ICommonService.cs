﻿using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Common.IService
{
    /// <summary>
    /// 公共服务接口
    /// </summary>
    public interface ICommonService
    {
        /// <summary>
        /// 获取维修类型
        /// </summary>
        /// <returns></returns>
        List<RepairTypeInfo> GetRepairType();
        /// <summary>
        /// 获取公共字段列表
        /// </summary>
        /// <param name="fieldType"></param>
        /// <returns></returns>
        List<CommonFieldInfo> GetCommonFieldList(string fieldType);

        /// <summary>
        /// 获取经营业态列表
        /// </summary>
        /// <returns></returns>
        List<BizTypeInfo> GetBizTypeList();

        /// <summary>
        /// 获取经营品类列表
        /// </summary>
        /// <returns></returns>
        List<BizCategoryInfo> GetBizCategoryList();

        /// <summary>
        /// 根据业态id获取品类列表
        /// </summary>
        /// <param name="bizTypeId"></param>
        /// <returns></returns>
        List<BizCategoryInfo> GetBizCategoryList(int bizTypeId);

        /// <summary>
        /// 获取商铺状态列表
        /// </summary>
        /// <returns></returns>
        List<ProjectUnitStatusInfo> GetUnitStatusList();

        /// <summary>
        /// 获取商铺类型列表
        /// </summary>
        /// <returns></returns>
        List<ProjectUnitTypeInfo> GetUnitTypeList();

        /// <summary>
        /// 获取合同状态列表
        /// </summary>
        /// <returns></returns>
        List<ContractStatusInfo> GetContractStatusList();

        /// <summary>
        /// 获取公司类型列表
        /// </summary>
        /// <returns></returns>
        List<CompanyTypeInfo> GetCompanyTypeList();

        /// <summary>
        /// 获取区域列表
        /// </summary>
        /// <returns></returns>
        List<RegionInfo> GetRegionList();

        /// <summary>
        /// 获取省份列表
        /// </summary>
        /// <returns></returns>
        List<ProvinceInfo> GetProvinceList();

        /// <summary>
        /// 获取省份列表
        /// </summary>
        /// <param name="regionId"></param>
        /// <returns></returns>
        List<ProvinceInfo> GetProvinceList(int regionId);

        /// <summary>
        /// 获取城市列表
        /// </summary>
        /// <param name="provinceId"></param>
        /// <returns></returns>
        List<CityInfo> GetCityList(int provinceId);

        /// <summary>
        /// 获取区列表
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        List<DistrictInfo> GetDistriceList(int cityId);

        /// <summary>
        /// 获取品牌列表
        /// </summary>
        /// <returns></returns>
        List<SelectListItemInfo> GetBrandList();

        /// <summary>
        /// 获取数字下拉列表
        /// </summary>
        /// <param name="startNum"></param>
        /// <param name="endNum"></param>
        /// <returns></returns>
        List<SelectListItemInfo> GetSelectItemList(int startNum, int endNum);

        /// <summary>
        /// 获取性别类型
        /// </summary>
        /// <returns></returns>
        List<GenderTypeInfo> GetGenderTypeList();

        /// <summary>
        /// 发送邮件、短信、微信
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool SendMessage(MessageInfo model);

        /// <summary>
        /// 获取消息内容
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        MessageInfo GetMessage(int messageId);


    }
}