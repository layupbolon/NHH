namespace NHH.Models.Official
{
    public class UnitQueryInfo
    {
        /// <summary>
        /// 项目ID
        /// </summary>
        public int? projectID { get; set; }

        /// <summary>
        /// 楼宇
        /// </summary>
        public string building { get; set; }

        /// <summary>
        /// 业态类型
        /// </summary>
        public int? bizType { get; set; }

        /// <summary>
        /// 面积范围
        /// </summary>
        public int? unitArea { get; set; }

    }
}
