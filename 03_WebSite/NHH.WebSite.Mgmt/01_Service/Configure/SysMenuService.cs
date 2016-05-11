using NHH.Entities;
using NHH.Framework.Service;
using NHH.Models.Configure;
using NHH.Service.Configure.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Configure
{
    /// <summary>
    /// 菜单服务接口
    /// </summary>
    public class SysMenuService : NHHService<NHHEntities>, ISysMenuService
    {
        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public List<SysMenuInfo> GetMenuList(int parentId)
        {
            var list = new List<SysMenuInfo>();
            var query = from m in Context.Sys_Menu
                        where m.Status == 1
                        select m;

            if (parentId > 0)
            {
                query = query.Where(item => item.ParentID == parentId);
            }
            else
            {
                query = query.Where(item => !item.ParentID.HasValue);
            }

            var entityList = query.OrderBy(item => item.SeqNo).ToList();
            if (entityList != null)
            {
                entityList.ForEach(entity =>
                {
                    var info = new SysMenuInfo
                    {
                        MenuID = entity.MenuID,
                        MenuName = entity.MenuName,
                    };
                    list.Add(info);
                });
            }

            return list;
        }

        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public SysMenuListModel GetMenuList(SysMenuListQueryInfo queryInfo)
        {
            var model = new SysMenuListModel();
            model.QueryInfo = queryInfo;
            model.MenuList = new List<SysMenuInfo>();
            model.PagingInfo = new Models.Common.PagingInfo
            {
                PageIndex = queryInfo.Page.HasValue ? queryInfo.Page.Value : 1
            };

            var query = from m in Context.Sys_Menu
                        where m.Status == 1
                        select m;
            if (queryInfo.ParentId.HasValue)
            {
                query = query.Where(item => item.ParentID == queryInfo.ParentId.Value);
            }

            model.PagingInfo.TotalCount = query.Count();
            var entityList = query.OrderBy(item => item.SeqNo).Skip(model.PagingInfo.SkipNum).Take(model.PagingInfo.TakeNum).ToList();
            if (entityList != null)
            {
                entityList.ForEach(entity =>
                {
                    var info = new SysMenuInfo
                    {
                        ParentID = entity.ParentID.HasValue ? entity.ParentID.Value : 0,
                        SeqNo = entity.SeqNo,
                        MenuID = entity.MenuID,
                        MenuName = entity.MenuName,
                        MenuUrl = entity.MenuUrl,
                        MenuIcon = entity.MenuIcon,
                        MenuDescription = entity.MenuDescription,
                    };
                    model.MenuList.Add(info);
                });
            }
            return model;
        }

        /// <summary>
        /// 新增菜单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddMenu(SysMenuDeatilModel model)
        {
            var bll = CreateBizObject<Sys_Menu>();
            var entity = new Sys_Menu();

            entity.ParentID = model.ParentID;
            entity.SeqNo = model.SeqNo;
            entity.MenuName = model.MenuName;
            entity.MenuUrl = model.MenuUrl;
            entity.MenuIcon = model.MenuIcon;
            entity.MenuDescription = model.MenuDescription;
            entity.Status = 1;
            entity.InDate = DateTime.Now;
            entity.InUser = model.InUser;
            entity.EditDate = DateTime.Now;
            entity.EditUser = model.EditUser;

            bll.Insert(entity);
            this.SaveChanges();

            return true;
        }

        /// <summary>
        /// 编辑菜单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateMenu(SysMenuDeatilModel model)
        {
            var bll = CreateBizObject<Sys_Menu>();
            var entity = bll.GetBySysNo(model.MenuID);
            if (entity == null)
                return false;

            entity.ParentID = model.ParentID;
            entity.SeqNo = model.SeqNo;
            entity.MenuName = model.MenuName;
            entity.MenuUrl = model.MenuUrl;
            entity.MenuIcon = model.MenuIcon;
            entity.MenuDescription = model.MenuDescription;
            entity.EditDate = DateTime.Now;
            entity.EditUser = model.EditUser;

            bll.Update(entity);
            this.SaveChanges();

            return true;
        }

        /// <summary>
        /// 获取菜单详情
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public SysMenuDeatilModel GetMenuDetail(int menuId)
        {
            var model = new SysMenuDeatilModel();
            var query = from m in Context.Sys_Menu
                        where m.Status == 1 && m.MenuID == menuId
                        select m;
            var entity = query.FirstOrDefault();
            if (entity != null)
            {
                model.ParentID = entity.ParentID.HasValue ? entity.ParentID.Value : 0;
                model.SeqNo = entity.SeqNo;
                model.MenuID = entity.MenuID;
                model.MenuName = entity.MenuName;
                model.MenuUrl = entity.MenuUrl;
                model.MenuIcon = entity.MenuIcon;
                model.MenuDescription = entity.MenuDescription;
            }
            return model;
        }
    }
}
