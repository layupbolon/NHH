using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Merchant
{
    /// <summary>
    /// 商户销售查询条件
    /// </summary>
    public class MerchantRevenueListQueryInfo : QueryInfo
    {
        private DateTime? _fromDate = DateTime.Now.Date.AddDays(-7);
        private DateTime? _toDate = DateTime.Now.Date;
        private int? _unitType = 3;

        /// <summary>
        /// 项目ID
        /// </summary>
        public int? ProjectId { get; set; }

        /// <summary>
        /// 楼宇ID
        /// </summary>
        public int? BuildingId { get; set; }

        /// <summary>
        /// 楼层ID
        /// </summary>
        public int? FloorId { get; set; }

        /// <summary>
        /// 业态
        /// </summary>
        public int? BizType { get; set; }

        /// <summary>
        /// 商铺类型
        /// </summary>
        public int? UnitType
        {
            get { return _unitType; }
            set { _unitType = value; }
        }

        /// <summary>
        /// 仅空数据
        /// </summary>
        public int? OnlyNullData { get; set; }

        /// <summary>
        /// 商铺ID
        /// </summary>
        public int? StoreId { get; set; }

        /// <summary>
        /// 商铺名称
        /// </summary>
        public string StoreName { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? FromDate
        {
            get { return _fromDate; }
            set { _fromDate = value; }
        }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? ToDate
        {
            get { return _toDate; }
            set { _toDate = value; }
        }

        /// <summary>
        /// 日期列表
        /// </summary>
        public List<string> DayList
        {
            get
            {
                var list = new List<string>();
                var date = _fromDate.Value;

                while (date <= _toDate.Value)
                {
                    list.Add(date.ToString("yyyy-MM-dd"));
                    date = date.AddDays(1);
                }

                return list;
            }
        }
    }
}
