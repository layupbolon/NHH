using NHH.Models.Configure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Configure.IService
{
    /// <summary>
    /// 菜单服务接口
    /// </summary>
    public interface ISysMenuService
    {
        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        List<SysMenuInfo> GetMenuList(int parentId);

        /// <summary>
        /// 根据查询条件获取列表页
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        SysMenuListModel GetMenuList(SysMenuListQueryInfo queryInfo);

        /// <summary>
        /// 新增菜单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool AddMenu(SysMenuDeatilModel model);

        /// <summary>
        /// 更新菜单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool UpdateMenu(SysMenuDeatilModel model);

        /// <summary>
        /// 获取菜单详情
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        SysMenuDeatilModel GetMenuDetail(int menuId);
    }
}
