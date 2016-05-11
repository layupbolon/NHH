using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Report
{
   public class KpiReportMulInfo
    {
        int projectID;
       /// <summary>
       /// 项目id
       /// </summary>
        public int ProjectID
        {
            get { return projectID; }
            set { projectID = value; }
        }
        int buildingID;
        /// <summary>
        /// /楼宇id
        /// </summary>
        public int BuildingID
        {
            get { return buildingID; }
            set { buildingID = value; }
        }
        int floorNumber;
       /// <summary>
        /// 楼层
       /// </summary>
        public int FloorNumber
        {
            get { return floorNumber; }
            set { floorNumber = value; }
        }
        int bizTypeID;
        /// <summary>
        /// 业态
        /// </summary>
        public int BizTypeID
        {
            get { return bizTypeID; }
            set { bizTypeID = value; }
        }
        int brandID;
       /// <summary>
        /// 品牌
       /// </summary>
        public int BrandID
        {
            get { return brandID; }
            set { brandID = value; }
        }
        int bizCategoryID;
       /// <summary>
       /// 
       /// </summary>
        public int BizCategoryID
        {
            get { return bizCategoryID; }
            set { bizCategoryID = value; }
        }
        int unitType;
        /// <summary>
        /// 类型
        /// </summary>
        public int UnitType
        {
            get { return unitType; }
            set { unitType = value; }
        }
        decimal unitAera;
       /// <summary>
       /// 签约面积
       /// </summary>
        public decimal UnitAera
        {
            get { return unitAera; }
            set { unitAera = value; }
        }
        int rentRange;
       /// <summary>
       /// 租期
       /// </summary>
        public int RentRange
        {
            get { return rentRange; }
            set { rentRange = value; }
        }
        decimal avgRentPrice;
       /// <summary>
       /// 合约租金平均价格
       /// </summary>
        public decimal AvgRentPrice
        {
            get { return avgRentPrice; }
            set { avgRentPrice = value; }
        }
        decimal avgPropertyPrice;
       /// <summary>
       /// 物业费平均价
       /// </summary>
        public decimal AvgPropertyPrice
        {
            get { return avgPropertyPrice; }
            set { avgPropertyPrice = value; }
        }

        int paymentTermsType;
       /// <summary>
       /// 
       /// </summary>
        public int PaymentTermsType
        {
            get { return paymentTermsType; }
            set { paymentTermsType = value; }
        }
    }
}
