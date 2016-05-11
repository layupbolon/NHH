using NHH.Entities;
using NHH.Framework.Service;
using NHH.Models.Common;
using NHH.Models.Common.Company;
using NHH.Service.Common.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Common
{
    /// <summary>
    /// 公司服务
    /// </summary>
    public class CompanyService : NHHService<NHHEntities>, ICompanyService
    {
        /// <summary>
        /// 关联公司详情
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public CompanyDetailModel GetCompanyDetail(int companyId)
        {
            var model = new CompanyDetailModel();
            model.CompanyTypeInfo = new Models.Common.CompanyTypeInfo();
            model.FinanceInfo = new FinanceModel();
            var query = from c in Context.Company
                        join p in Context.Province on c.ProvinceID equals p.ProvinceID into CompanyProvince
                        from p in CompanyProvince.DefaultIfEmpty()
                        join ct in Context.City on c.CityID equals ct.CityID into CompanyCity
                        from ct in CompanyCity.DefaultIfEmpty()
                        join rp in Context.Province on c.RegisterProvinceID equals rp.ProvinceID into CompanyRProvince
                        from rp in CompanyRProvince.DefaultIfEmpty()
                        join rct in Context.City on c.RegisterCityID equals rct.CityID into CompanyRCity
                        from rct in CompanyRCity.DefaultIfEmpty()
                        join vc in Context.View_CompanyType on c.CompanyType equals vc.CompanyTypeID

                        where c.CompanyID == companyId
                        select new
                        {
                            c.CompanyID,
                            c.CompanyName,
                            c.BriefName,
                            c.CompanyCode,
                            c.ProvinceID,
                            p.ProvinceName,
                            c.CityID,
                            ct.CityName,
                            c.AddressLine,
                            c.Zipcode,
                            c.LicenseID,
                            c.RegisterProvinceID,
                            RegisterProvinceName = rp.ProvinceName,
                            c.RegisterCityID,
                            RegisterCityName = rct.CityName,
                            c.RegisterAddressLine,
                            c.OwnerName,
                            c.ContactName,
                            c.ContactIDNumber,
                            c.ContactPhone,
                            c.ContactEmail,
                            c.OwnerCompanyID,
                            c.CompanyType,
                            vc.CompanyTypeName
                        };
            var entity = query.FirstOrDefault();

            if (entity != null)
            {
                model.CompanyID = entity.CompanyID;
                model.CompanyName = entity.CompanyName;
                model.CompanyCode = entity.CompanyCode;
                model.BriefName = entity.BriefName;
                model.ProvinceID = entity.ProvinceID.HasValue ? entity.ProvinceID.Value : 0;
                model.ProvinceName = entity.ProvinceName;
                model.CityID = entity.CityID.HasValue ? entity.CityID.Value : 0;
                model.CityName = entity.CityName;
                model.AddressLine = entity.AddressLine;
                model.CompanyAddress = entity.ProvinceName + (entity.CityName != null ? "," + entity.CityName : "") + (entity.AddressLine != null ? "," + entity.AddressLine : "");
                model.ZipCode = entity.Zipcode;
                model.LicenseID = entity.LicenseID;
                model.RegisterProvinceID = entity.RegisterProvinceID.HasValue ? entity.RegisterProvinceID.Value : 0;
                model.RegisterProvinceName = entity.RegisterProvinceName;
                model.RegisterCityID = entity.RegisterCityID.HasValue ? entity.RegisterCityID.Value : 0;
                model.RegisterCityName = entity.RegisterCityName;
                model.RegisterAddressLine = entity.RegisterAddressLine;
                model.CompanyRegisterAddress = entity.RegisterProvinceName + (entity.RegisterCityName != null ? "," + entity.RegisterCityName : "") + (entity.RegisterAddressLine != null ? "," + entity.RegisterAddressLine : "");
                model.OwnerName = entity.OwnerName;
                model.ContactName = entity.ContactName;
                model.ContactIDNumber = entity.ContactIDNumber;
                model.ContactPhone = entity.ContactPhone;
                model.ContactEmail = entity.ContactEmail;
                model.OwnerCompanyID = entity.OwnerCompanyID;
                model.FinanceInfo = GetFinanceModel(entity.CompanyID);

                //绑定公司类型
                model.CompanyType = entity.CompanyType;
                model.CompanyTypeInfo.Name = entity.CompanyTypeName;
            }
            return model;
        }

        /// <summary>
        /// 根据公司id查询公司财务信息
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public FinanceModel GetFinanceModel(int companyId)
        {
            var model = new FinanceModel();

            Expression<Func<Company_Finance, bool>> where = com => com.CompanyID == companyId;
            var entity = CreateBizObject<Company_Finance>().TopOne(where);

            if (entity != null)
            {
                model.BankAccount = entity.BankAccount;
                model.OpeningBank = entity.BankName;
                model.SubBranch = entity.SubBranch;
                model.OpeninBankName = entity.AccountName;
                model.AlipayAccount = entity.AlipayAccount;
                model.WechatAccount = entity.WechatAccount;
                model.FinanceContact = entity.FinanceContact;
                model.FinancePhone = entity.FinancePhone;
            }
            return model;
        }

        /// <summary>
        /// 关联公司查询首页
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public CompanyListModel GetCompanyList(CompanyListQueryInfo queryInfo)
        {
            var model = new CompanyListModel();
            model.QueryInfo = queryInfo;
            model.CompanyList = new List<CompanyInfo>();
            model.PagingInfo = new PagingInfo
            {
                PageIndex = queryInfo.Page.HasValue ? queryInfo.Page.Value : 1,
            };

            var query = from c in Context.Company
                        join p in Context.Province on c.ProvinceID equals p.ProvinceID into CompanyProvince
                        from p in CompanyProvince.DefaultIfEmpty()
                        join ct in Context.City on c.CityID equals ct.CityID into CompanyCity
                        from ct in CompanyCity.DefaultIfEmpty()
                        join vc in Context.View_CompanyType on c.CompanyType equals vc.CompanyTypeID
                        where c.Status == 1
                        select new
                        {
                            c.CompanyType,
                            vc.CompanyTypeName,
                            c.CompanyName,
                            c.CompanyID,
                            p.ProvinceName,
                            ct.CityName
                        };

            if (queryInfo.CompanyType.HasValue)
            {
                query = query.Where(c => c.CompanyType == queryInfo.CompanyType.Value);
            }

            model.PagingInfo.TotalCount = query.Count();

            query = query.OrderBy(queryInfo.OrderExpression).Skip(model.PagingInfo.SkipNum).Take(model.PagingInfo.TakeNum);

            var entityList = query.ToList();
            entityList.ForEach(entity =>
            {
                var companyInfo = new CompanyInfo();
                companyInfo.CompanyType = entity.CompanyType;
                companyInfo.CompanyTypeInfo = new CompanyTypeInfo();
                companyInfo.CompanyTypeInfo.Name = entity.CompanyTypeName;
                companyInfo.CompanyID = entity.CompanyID;
                companyInfo.CompanyName = entity.CompanyName;
                companyInfo.CompanyAddress = entity.ProvinceName + "," + entity.CityName;

                model.CompanyList.Add(companyInfo);

            });
            return model;
        }

        /// <summary>
        /// 关联公司新增
        /// </summary>
        /// <param name="detailModel"></param>
        public bool AddCompany(CompanyDetailModel detailModel, FinanceModel finance)
        {
            var instance = CreateBizObject<Company>();

            bool exist = instance.Exists(m => m.CompanyName == detailModel.CompanyName && m.Status == 1);

            if (!exist)
            {
                Company entity = new Company()
                {
                    CompanyType = detailModel.CompanyType,
                    CompanyName = detailModel.CompanyName,
                    CompanyCode = detailModel.CompanyCode,
                    BriefName = detailModel.BriefName,
                    ProvinceID = detailModel.ProvinceID,
                    CityID = detailModel.CityID,
                    AddressLine = detailModel.AddressLine,
                    Zipcode = detailModel.ZipCode,
                    LicenseID = detailModel.LicenseID,
                    RegisterProvinceID = detailModel.RegisterProvinceID,
                    RegisterCityID = detailModel.RegisterCityID,
                    RegisterAddressLine = detailModel.RegisterAddressLine,
                    OwnerName = detailModel.OwnerName,
                    ContactName = detailModel.ContactName,
                    ContactIDNumber = detailModel.ContactIDNumber,
                    ContactEmail = detailModel.ContactEmail,
                    ContactPhone = detailModel.ContactPhone,
                    OwnerCompanyID = detailModel.OwnerCompanyID,
                    Status = 1,
                    InDate = DateTime.Now,
                    InUser = detailModel.InUser,
                    EditDate = DateTime.Now,
                    EditUser = detailModel.InUser,
                };
                instance.Insert(entity);
                this.SaveChanges();


                AddCompanyFinance(finance, entity.CompanyID);
                return true;
            }
            return false;

        }

        /// <summary>
        /// 关联公司删除
        /// </summary>
        /// <param name="companyId"></param>
        public void DeleteCompany(int companyId)
        {
            var instance = CreateBizObject<Company>();
            instance.Delete(companyId);
            this.SaveChanges();
        }

        /// <summary>
        /// 关联公司更新
        /// </summary>
        /// <param name="DetailModel"></param>
        /// <returns></returns>
        public void UpdateCompany(CompanyDetailModel DetailModel, FinanceModel finance)
        {
            var conInstance = CreateBizObject<Company>();
            var finInstance = CreateBizObject<Company_Finance>();

            var entity = conInstance.GetBySysNo(DetailModel.CompanyID);

            if (entity != null)
            {
                entity.CompanyType = DetailModel.CompanyType;
                //  entity.CompanyName = DetailModel.CompanyName;
                entity.CompanyCode = DetailModel.CompanyCode;
                entity.BriefName = DetailModel.BriefName;
                entity.ProvinceID = DetailModel.ProvinceID;
                entity.CityID = DetailModel.CityID;
                entity.AddressLine = DetailModel.AddressLine;
                entity.Zipcode = DetailModel.ZipCode;
                entity.LicenseID = DetailModel.LicenseID;
                entity.RegisterProvinceID = DetailModel.RegisterProvinceID;
                entity.RegisterCityID = DetailModel.RegisterCityID;
                entity.RegisterAddressLine = DetailModel.RegisterAddressLine;
                entity.OwnerName = DetailModel.OwnerName;
                entity.ContactName = DetailModel.ContactName;
                entity.ContactIDNumber = DetailModel.ContactIDNumber;
                entity.ContactEmail = DetailModel.ContactEmail;
                entity.ContactPhone = DetailModel.ContactPhone;
                entity.OwnerCompanyID = DetailModel.OwnerCompanyID;
                entity.EditDate = DateTime.Now;
                entity.EditUser = DetailModel.InUser;
                conInstance.Update(entity);
                this.SaveChanges();
            }

            Expression<Func<Company_Finance, bool>> where = f => f.CompanyID == DetailModel.CompanyID;
            var FinEntity = finInstance.TopOne(where);
            if (FinEntity != null)
            {
                FinEntity.BankAccount = finance.BankAccount;
                FinEntity.BankName = finance.OpeningBank;
                FinEntity.SubBranch = finance.SubBranch;
                FinEntity.AccountName = finance.OpeninBankName;
                FinEntity.AlipayAccount = finance.AlipayAccount;
                FinEntity.WechatAccount = finance.WechatAccount;
                FinEntity.FinanceContact = finance.FinanceContact;
                FinEntity.FinancePhone = finance.FinancePhone;
                FinEntity.EditDate = DateTime.Now;
                FinEntity.EditUser = DetailModel.InUser;

                finInstance.Update(FinEntity);
                this.SaveChanges();
            }
            else
            {
                AddCompanyFinance(finance, DetailModel.CompanyID);
            }
        }
        /// <summary>
        /// 增加公司财务信息
        /// </summary>
        /// <param name="finance"></param>
        /// <param name="cid"></param>
        public void AddCompanyFinance(FinanceModel finance, int cid)
        {
            var instance = CreateBizObject<Company_Finance>();
            Company_Finance entity = new Company_Finance()
            {
                BankAccount = finance.BankAccount,
                BankName = finance.OpeningBank,
                SubBranch = finance.SubBranch,
                AccountName = finance.OpeninBankName,
                AlipayAccount = finance.AlipayAccount,
                WechatAccount = finance.WechatAccount,
                FinanceContact = finance.FinanceContact,
                FinancePhone = finance.FinancePhone,
                CompanyID = cid,
                InDate = DateTime.Now,
                InUser = 1,
                EditDate = DateTime.Now,
                EditUser = 1,
            };
            instance.Insert(entity);
            this.SaveChanges();
        }
    }
}
