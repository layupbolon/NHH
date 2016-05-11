using NHH.Framework.Web;
using System;

namespace NHH.WebAPI.Merchant
{
    public class NHHAPIContext
    {
        private NHHAPIContext()
        {
        }



        #region Current
        [ThreadStatic]
        public static NHHAPIContext _Context;

        public static NHHAPIContext Current
        {
            get
            {
                if (_Context == null)
                {
                    _Context = new NHHAPIContext();
                }
                return _Context;
            }
        } 
        #endregion


        /// <summary>
        /// 当前用户编号
        /// </summary>
        public int UserID { get { return NHHWebContext.Current.UserID; } }
        /// <summary>
        /// 当前用户名称
        /// </summary>
        public string UserName { get { return NHHWebContext.Current.UserName; } }
        /// <summary>
        /// 当前用户角色编号
        /// </summary>
        public int RoleID { get { return NHHWebContext.Current.RoleIDs.Count > 0 ? NHHWebContext.Current.RoleIDs[0] : 0; } }
        /// <summary>
        /// 当前用户角色名称
        /// </summary>
        public string RoleName { get { return NHHWebContext.Current.RoleNames.Count > 0 ? NHHWebContext.Current.RoleNames[0] : null; } }
        /// <summary>
        /// 当前用户所属店铺编号
        /// </summary>
        public int StoreID {
            get
            {
                int i = 0;
                return int.TryParse(NHHWebContext.Current.GetUserData("StoreID"), out i) ? i : 0;
            } 
        }
        /// <summary>
        /// 当前用户所属店铺名称
        /// </summary>
        public string StoreName { get { return string.Empty; } }
        /// <summary>
        /// 当前用户所属商户编号
        /// </summary>
        public int MerchantID
        {
            get
            {
                int i = 0;
                return int.TryParse(NHHWebContext.Current.GetUserData("MerchantID"), out i) ? i : 0;
            }
        }
        /// <summary>
        /// 当前用户所属商户名称
        /// </summary>
        public string MerchantName { get { return string.Empty; }}
        /// <summary>
        /// 当前用户所属商户的项目号
        /// </summary>
        public int ProjectID
        {
            get
            {
                int i = 0;
                return int.TryParse(NHHWebContext.Current.GetUserData("ProjectID"), out i) ? i : 0;
            }
        }
    }
}