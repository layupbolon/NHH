using NHH.Entities;
using NHH.Framework.Service;
using NHH.Models.Common;
using NHH.Models.Common.Company;
using NHH.Models.Common.Department;
using NHH.Service.Common.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Common
{
    /// <summary>
    /// 部门服务
    /// </summary>
    public class DepartmentService : NHHService<NHHEntities>, IDepartmentService
    {
        /// <summary>
        /// 获取公司ID
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        public int GetCompanyId(int departmentId)
        {
            return (from d in Context.Department
                    where d.DepartmentID == departmentId
                    select d.CompanyID).FirstOrDefault();
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        public bool AddDept(DepartmentDetailModel model)
        {
            var instance = CreateBizObject<Department>();

            Department entity = new Department
            {
                CompanyID = model.CompanyID,
                DepartmentName = model.DepartmentName,
                Phone = model.Phone,
                InDate = DateTime.Now,
                InUser = model.InUser,
                EditDate = DateTime.Now,
                EditUser = model.EditUser,
                Status = 1//1：可用状态   -1：不可用
            };
            instance.Insert(entity);
            this.SaveChanges();
            return true;
        }

        /// <summary>
        /// 判断部门是否存在
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        public bool DepartmentIsExsit(int departmentId)
        {
            Expression<Func<Department, bool>> where = c => c.DepartmentID == departmentId;
            bool IsExist = CreateBizObject<Department>().Exists(where);

            return IsExist;
        }

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="departmentId"></param>
        public bool DeleteDept(int departmentId)
        {
            var instance = CreateBizObject<Department>();
            instance.Delete(departmentId);
            this.SaveChanges();
            return true;
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateDept(DepartmentDetailModel model)
        {
            var instance = CreateBizObject<Department>();
            var entity = instance.GetBySysNo(model.DepartmentID);
            if (entity != null)
            {
                entity.DepartmentName = model.DepartmentName;
                entity.Phone = model.Phone;
                entity.EditDate = DateTime.Now;
                entity.EditUser = model.EditUser;

                instance.Update(entity);
                this.SaveChanges();
            }
            return true;
        }

        /// <summary>
        /// <summary>
        /// 部门详情
        /// </summary>
        /// <param name="DeptID"></param>
        /// <returns></returns>
        public DepartmentDetailModel GetDeptDetail(int deptId)
        {
            var model = new DepartmentDetailModel();
            Department entity = CreateCacheBizObject<Department>().GetBySysNo(deptId);

            if (entity != null)
            {
                model.DepartmentID = entity.DepartmentID;
                model.DepartmentName = entity.DepartmentName;
                model.Phone = entity.Phone;
                model.InDate = entity.InDate;
                model.CompanyID = entity.CompanyID;
                model.InUser = entity.InUser;
                model.EditDate = entity.EditDate;
                model.EditUser = entity.EditUser;
                //绑定公司类型
            }
            return model;
        }

        /// <summary>
        /// 部门查询页面
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public DepartmentListModel GetDeptList(int companyId)
        {
            var model = new DepartmentListModel();
            model.DepartmentList = new List<DepartmentInfo>();
            model.CompanyInfo = new CompanyCommonInfo();
            model.CompanyInfo.Id = companyId;
            var query = from d in Context.Department
                        join c in Context.Company on d.CompanyID equals c.CompanyID
                        where d.CompanyID == companyId && d.Status == 1
                        select new
                        {
                            d.DepartmentID,
                            d.DepartmentName,
                            d.Phone,
                            c.CompanyID,
                            c.CompanyName,
                            c.BriefName,
                        };


            var entityList = query.ToList();

            if (entityList == null || entityList.Count == 0)
                return model;

            entityList.ForEach(entity =>
            {
                model.DepartmentList.Add(new DepartmentInfo
                {
                    DepartmentID = entity.DepartmentID,
                    DepartmentName = entity.DepartmentName,
                    BriefName = entity.BriefName,
                    CompanyID = entity.CompanyID,
                    Phone = entity.Phone
                });
            });
            return model;
        }
    }
}
