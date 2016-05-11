using NHH.Entities;
using NHH.Framework.Security.Cryptography;
using NHH.Framework.Service;
using NHH.Models.Common;
using NHH.Models.Common.Employee;
using NHH.Service.Common.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using NHH.Framework.Utility;
namespace NHH.Service.Common
{
    /// <summary>
    /// 员工服务
    /// </summary>
    public class EmployeeService : NHHService<NHHEntities>, IEmployeeService
    {
        /// <summary>
        /// 获取员工详情
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public EmployeeDetailModel GetEmployeeDetail(int employeeId)
        {
            var model = new EmployeeDetailModel();
            model.CompanyInfo = new CompanyCommonInfo();
            model.DeptInfo = new DepartmentCommonInfo();

            var query = from e in Context.Employee
                        join d in Context.Department on e.DepartmentID equals d.DepartmentID
                        join c in Context.Company on d.CompanyID equals c.CompanyID
                        where e.EmployeeID == employeeId && e.Status == 1
                        select new
                        {
                            e.EmployeeID,
                            e.EmployeeCode,
                            e.EmployeeName,
                            e.Gender,
                            e.Title,
                            e.Tag,
                            e.Email,
                            e.Moblie,
                            e.IDNumber,
                            e.Phone,
                            e.Ext,
                            e.Birthday,
                            d.DepartmentID,
                            d.DepartmentName,
                            c.CompanyID,
                            c.BriefName,
                        };

            var entity = query.FirstOrDefault();

            if (entity == null)
            {
                return model;
            }
            model.EmployeeId = entity.EmployeeID;
            model.EmployeeCode = entity.EmployeeCode;
            model.EmployeeName = entity.EmployeeName;
            model.IDNumber = entity.IDNumber;
            model.Title = entity.Title;
            model.Tag = entity.Tag;
            model.Email = entity.Email;
            model.Moblie = entity.Moblie;
            model.Phone = entity.Phone;
            model.Ext = entity.Ext;
            model.DepartmentName = entity.DepartmentName;
            model.Birthday = entity.Birthday;
            model.DepartmentId = entity.DepartmentID;
            model.DeptInfo.DepartmentName = entity.DepartmentName;
            model.CompanyId = entity.CompanyID;
            model.CompanyInfo.Name = entity.BriefName;
            model.GenderId = entity.Gender;
            model.GenderType = new GenderTypeInfo();
            model.GenderType.Name = (from d in Context.Dictionary
                                     where d.FieldType == "GenderType" && d.FieldValue == entity.Gender
                                     select d.FieldName).FirstOrDefault();

            return model;
        }

        /// <summary>
        /// 获取员工列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public EmployeeListModel GetEmployeeList(EmployeeListQueryInfo queryInfo, DepartmentCommonInfo deptInfo)
        {
            var model = new EmployeeListModel();
            model.QueryInfo = queryInfo;
            model.EmployeeList = new List<EmployeeInfo>();
            model.DeptInfo = new DepartmentCommonInfo();
            model.DeptInfo.DepartmentID = deptInfo.DepartmentID;
            model.CompanyInfo = new CompanyCommonInfo();

            model.PagingInfo = new PagingInfo
            {
                PageIndex = queryInfo.Page.HasValue ? queryInfo.Page.Value : 1,
            };

            var query = from e in Context.Employee
                        join d in Context.Department on e.DepartmentID equals d.DepartmentID
                        join c in Context.Company on d.CompanyID equals c.CompanyID
                        where e.Status == 1 && e.DepartmentID == deptInfo.DepartmentID
                        select new
                        {
                            e.EmployeeID,
                            e.EmployeeCode,
                            e.EmployeeName,
                            e.Gender,
                            e.Title,
                            e.Email,
                            e.Moblie,
                            e.Phone,
                            e.Ext,
                            e.Birthday,
                            d.DepartmentID,
                            d.DepartmentName,
                            c.CompanyID,
                            c.BriefName,
                            c.CompanyName
                        };

            //按搜索条件筛选
            if (queryInfo != null)
            {
                if (!string.IsNullOrEmpty(queryInfo.Name))
                {
                    query = query.Where(m => m.EmployeeName.Contains(queryInfo.Name));
                }
                if (!string.IsNullOrEmpty(queryInfo.Code))
                {
                    query = query.Where(m => m.EmployeeCode.Contains(queryInfo.Code));
                }
                if (!string.IsNullOrEmpty(queryInfo.Company))
                {
                    query = query.Where(m => m.CompanyName.Contains(queryInfo.Company));
                }

            }
            ///分页查询
            model.PagingInfo.TotalCount = query.Count();

            query = query.OrderBy(c => c.CompanyID).Skip(model.PagingInfo.SkipNum).Take(model.PagingInfo.TakeNum);

            var entityList = query.ToList();
            if (entityList == null || entityList.Count == 0)
            {

                model.CompanyInfo.Id = GetCompanyIdByDeptId(deptInfo.DepartmentID);

                return model;
            }
            else
            {
                ///给面包屑导航到部门传值

                model.CompanyInfo.Id = entityList.First().CompanyID;
            }

            entityList.ForEach(entity =>
            {
                var info = new EmployeeInfo();

                info.EmployeeId = entity.EmployeeID;
                info.EmployeeCode = entity.EmployeeCode;
                info.EmployeeName = entity.EmployeeName;
                info.Title = entity.Title;
                info.Email = entity.Email;
                info.Moblie = entity.Moblie;
                info.Phone = entity.Phone;
                info.Ext = entity.Ext;
                info.Birthday = entity.Birthday;
                info.DepartmentId = entity.DepartmentID;
                info.DeptInfo = new DepartmentCommonInfo();
                info.DeptInfo.DepartmentName = entity.DepartmentName;
                info.CompanyId = entity.CompanyID;
                info.CompanyInfo = new CompanyCommonInfo();
                info.CompanyInfo.Name = entity.BriefName;
                info.GenderId = entity.Gender;
                info.GenderType = new GenderTypeInfo();
                info.GenderType.Name = (from d in Context.Dictionary
                                        where d.FieldType == "GenderType" && d.FieldValue == entity.Gender
                                        select d.FieldName).FirstOrDefault();

                model.EmployeeList.Add(info);
            });
            return model;
        }

        /// <summary>
        /// 根据部门id获取公司id
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        public int GetCompanyIdByDeptId(int deptId)
        {
            var compayQuery = from d in Context.Department
                              join c in Context.Company on d.CompanyID equals c.CompanyID
                              where d.DepartmentID == deptId
                              select new
                              {
                                  c.CompanyID
                              };

            return compayQuery.FirstOrDefault().CompanyID;

        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        public MessageBag<bool> AddEmployee(EmployeeDetailModel model)
        {
            var result = new MessageBag<bool>();
            result.Code = 0;
            result.Desc = "新增成功";

            var query = from e in Context.Employee
                        where e.Status == 1 && e.CompanyID == model.CompanyId && e.EmployeeCode == model.EmployeeCode
                        select e.EmployeeCode;

            if (query.Any())
            {
                result.Code = -1;
                result.Key = "EmployeeCode";
                result.Desc = "员工编号不可用";
                return result;
            }

            var instance = CreateBizObject<Employee>();            
            Employee entity = new Employee()
            {
                EmployeeCode = model.EmployeeCode,
                EmployeeName = model.EmployeeName,
                Title = model.Title,
                Tag = model.Tag,
                Email = model.Email,
                Moblie = model.Moblie,
                Phone = model.Phone,
                Ext = model.Ext,
                Birthday = model.Birthday,
                DepartmentID = model.DepartmentId,
                IDNumber = model.IDNumber,
                CompanyID = model.CompanyId,
                Gender = model.GenderId,
                Status = 1,
                InDate = DateTime.Now,
                InUser = 1,
                EditDate = DateTime.Now,
                EditUser = 1//获取当前用户id
            };

            instance.Insert(entity);
            this.SaveChanges();

            //给新员工分配系统账号
            if (model.SysUserOrNot)
            {
                AddSysUser(entity);
            }
            return result;
        }

        /// <summary>
        /// 给新员工初始化一个系统账号
        /// </summary>
        /// <param name="employee"></param>
        public void AddSysUser(Employee employee)
        {

            var instance = CreateBizObject<Sys_User>();
            var roleInstance = CreateBizObject<Sys_User_Role>();
            var entity = new Sys_User()
            {
                EmployeeID = employee.EmployeeID,
                UserType = 1,
                UserName = employee.Email,
                Password = Cryptographer.SHA1.Encrypt(ParamManager.GetStringValue("InitPassword")),
                Status = 1,
                InDate = DateTime.Now,
                InUser = 0,
                EditDate = DateTime.Now,
                EditUser = 0
            };

            instance.Insert(entity);
            this.SaveChanges();

        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="employeeId"></param>
        public void DeleteEmployee(int employeeId)
        {
            var instance = CreateBizObject<Employee>();
            instance.Delete(employeeId);
            this.SaveChanges();
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void UpdateEmployee(EmployeeDetailModel model)
        {
            var instance = CreateBizObject<Employee>();
            var entity = instance.GetBySysNo(model.EmployeeId);
            if (entity != null)
            {
                entity.EmployeeCode = model.EmployeeCode;
                entity.EmployeeName = model.EmployeeName;
                entity.Title = model.Title;
                entity.Tag = model.Tag;
                entity.Email = model.Email;
                entity.Moblie = model.Moblie;
                entity.Phone = model.Phone;
                entity.Ext = model.Ext;
                entity.Birthday = model.Birthday;
                entity.DepartmentID = model.DepartmentId;
                entity.IDNumber = model.IDNumber;
                entity.CompanyID = model.CompanyId;
                entity.Gender = model.GenderId;
                entity.Status = 1;
                entity.EditDate = DateTime.Now;
                entity.EditUser = 1;
                instance.Update(entity);
                this.SaveChanges();
            }
        }
    }
}
