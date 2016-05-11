using NHH.Models.Merchant;
using NHH.Service.Merchant.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;
using NHH.Framework.Service;
using NHH.Entities;
using NHH.Framework.Utility;
using NHH.Models.Common;
using System.Linq.Expressions;
using NHH.Framework.Security.Cryptography;
namespace NHH.Service.Merchant
{
    /// <summary>
    /// 商户服务
    /// </summary>
    public class MerchantService : NHHService<NHHEntities>, IMerchantService
    {

        /// <summary>
        /// 获取商户详细信息
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        public MerchantDetailModel GetMerchantDetail(int merchantId)
        {
            var model = new MerchantDetailModel();
            var query = from m in Context.Merchant
                        join p in Context.Province on m.ProvinceID equals p.ProvinceID into MerchantProvince
                        from p in MerchantProvince.DefaultIfEmpty()
                        join ct in Context.City on m.CityID equals ct.CityID into MerchantCity
                        from ct in MerchantCity.DefaultIfEmpty()
                        join rp in Context.Province on m.RegisterProvinceID equals rp.ProvinceID into MerchantRProvince
                        from rp in MerchantRProvince.DefaultIfEmpty()
                        join rct in Context.City on m.RegisterCityID equals rct.CityID into MerchantRCity
                        from rct in MerchantRCity.DefaultIfEmpty()
                        where m.MerchantID == merchantId
                        select new
                        {
                            m.MerchantID,
                            m.MerchantType,
                            m.MerchantCode,
                            m.BriefName,
                            m.MerchantName,
                            m.ProvinceID,
                            p.ProvinceName,
                            m.CityID,
                            ct.CityName,
                            m.AddressLine,
                            m.Zipcode,
                            m.LicenseID,
                            m.RegisterProvinceID,
                            RegisterProvinceName = rp.ProvinceName,
                            m.RegisterCityID,
                            RegisterCityName = rct.CityName,
                            m.RegisterAddressLine,
                            m.OwnerName,
                            m.ContactName,
                            m.ContactIDNumber,
                            m.ContactPhone,
                            m.ContactEmail
                        };
            var entity = query.FirstOrDefault();

            if (entity != null)
            {
                model.MerchantId = entity.MerchantID;
                model.MerchantType = entity.MerchantType;
                model.MerchantTypeInfo = new MerchantTypeInfo();
                model.MerchantTypeInfo.Name = entity.MerchantType == 1 ? "公司" : "自然人";
                model.MerchantCode = entity.MerchantCode;
                model.BriefName = entity.BriefName;
                model.MerchantName = entity.MerchantName;
                model.ProvinceId = entity.ProvinceID.HasValue ? entity.ProvinceID.Value : 0;
                model.ProvinceInfo = new ProvinceInfo();
                model.ProvinceInfo.Name = entity.ProvinceName;
                model.CityId = entity.CityID.HasValue ? entity.CityID.Value : 0;
                model.CityInfo = new CityInfo();
                model.CityInfo.Name = entity.CityName;
                model.AddressLine = entity.AddressLine;
                model.Zipcode = entity.Zipcode;
                model.LicenseId = entity.LicenseID;
                model.RegisterProvinceId = entity.RegisterProvinceID.HasValue ? entity.RegisterProvinceID.Value : 0;
                model.RegisterProvinceInfo = new ProvinceInfo();
                model.RegisterProvinceInfo.Name = entity.RegisterProvinceName;
                model.RegisterCityId = entity.RegisterCityID.HasValue ? entity.RegisterCityID.Value : 0;
                model.RegisterCityInfo = new CityInfo();
                model.RegisterCityInfo.Name = entity.RegisterCityName;
                model.RegisterAddressLine = entity.RegisterAddressLine;
                model.OwnerName = entity.OwnerName;
                model.ContactName = entity.ContactName;
                model.ContactIDNumber = entity.ContactIDNumber;
                model.ContactPhone = entity.ContactPhone;
                model.ContactEmail = entity.ContactEmail;
                //获取商户财务信息
                model.FinanceInfo = GetFinance(entity.MerchantID);
            }
            return model;
        }

        /// <summary>
        /// 根据公司id查询商户财务信息
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        public MerchantFinanceModel GetFinance(int merchantId)
        {

            Expression<Func<Merchant_Finance, bool>> where = com => com.MerchantID == merchantId;

            var entity = CreateBizObject<Merchant_Finance>().TopOne(where);
            var model = new MerchantFinanceModel();
            if (entity != null)
            {
                model.BankAccount = entity.BankAccount;
                model.BankName = entity.BankName;
                model.AccountName = entity.AccountName;
                model.SubBranch = entity.SubBranch;
                model.AlipayAccount = entity.AlipayAccount;
                model.WechatAccount = entity.WechatAccount;
                model.FinanceContact = entity.FinanceContact;
                model.FinancePhone = entity.FinancePhone;
            }
            return model;
        }

        /// <summary>
        /// 获取商户列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public BaseListModel<MerchantInfo> GetMerchantList(MerchantListQueryInfo queryInfo)
        {
            var model = new BaseListModel<MerchantInfo>();
            model.QueryInfo = queryInfo;
            model.List = new List<MerchantInfo>();
            model.PagingInfo = new PagingInfo
            {
                PageIndex = queryInfo.Page.HasValue ? queryInfo.Page.Value : 1
            };

            #region 查询消息
            var query = from m in Context.Merchant
                        join p in Context.Province on m.ProvinceID equals p.ProvinceID into MerchantProvince
                        from p in MerchantProvince.DefaultIfEmpty()
                        join c in Context.City on m.CityID equals c.CityID into MerchantCity
                        from c in MerchantCity.DefaultIfEmpty()
                        where m.Status == 1
                        select new
                        {
                            m.MerchantID,
                            m.MerchantType,
                            m.MerchantName,
                            m.OwnerName,
                            m.ContactName,
                            p.ProvinceName,
                            c.CityName
                        };

            if (queryInfo.MerchantType.HasValue)
            {
                query = query.Where(item => item.MerchantType == queryInfo.MerchantType.Value);
            }
            if (!string.IsNullOrEmpty(queryInfo.MerchantName) && queryInfo.MerchantName.Length > 0)
            {
                query = query.Where(item => item.MerchantName.Contains(queryInfo.MerchantName));
            }
            #endregion

            model.PagingInfo.TotalCount = query.Count();
            query = query.OrderBy(queryInfo.OrderExpression).Skip(model.PagingInfo.SkipNum).Take(model.PagingInfo.TakeNum);

            var entityList = query.ToList();
            if (entityList != null)
            {
                entityList.ForEach(entity =>
                {
                    var info = new MerchantInfo();
                    info.MerchantId = entity.MerchantID;
                    info.MerchantType = entity.MerchantType;
                    info.MerchantTypeName = entity.MerchantType == 1 ? "公司" : "自然人";
                    info.MerchantName = entity.MerchantName;
                    info.OwnerName = entity.OwnerName;
                    info.ContactName = entity.ContactName;
                    info.MerchantAddress = string.Format("{0}，{1}", entity.ProvinceName, entity.CityName);
                    model.List.Add(info);
                });
            }
            return model;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddMerchant(MerchantDetailModel model)
        {

            var instance = CreateBizObject<NHH.Entities.Merchant>();

            //商户名不重复时，则往下执行
            NHH.Entities.Merchant merchant = new NHH.Entities.Merchant()
            {
                MerchantType = model.MerchantType,
                MerchantName = model.MerchantName,
                MerchantCode = model.MerchantCode,
                BriefName = model.BriefName,
                ProvinceID = model.ProvinceId,
                CityID = model.CityId,
                AddressLine = model.AddressLine,
                Zipcode = model.Zipcode,
                LicenseID = model.LicenseId,
                RegisterProvinceID = model.RegisterProvinceId,
                RegisterCityID = model.RegisterCityId,
                RegisterAddressLine = model.RegisterAddressLine,
                OwnerName = model.OwnerName,
                ContactName = model.ContactName,
                ContactIDNumber = model.ContactIDNumber,
                ContactEmail = model.ContactEmail,
                ContactPhone = model.ContactPhone,
                Status = 1,
                InDate = DateTime.Now,
                InUser = model.UserId,
                EditDate = DateTime.Now,
                EditUser = model.UserId,
            };
            instance.Insert(merchant);
            this.SaveChanges();

            //增加商户财务信息
            model.FinanceInfo.MerchantId = merchant.MerchantID;
            AddMerchantFinance(model.FinanceInfo);

            //创建商户时为商户用户增加一个账号
            AddMerchantUser(merchant);

            return true;
        }

        /// <summary>
        /// //创建商户时为商户用户增加一个账号
        /// </summary>
        /// <param name="merchantId"></param>
        public void AddMerchantUser(NHH.Entities.Merchant merchant)
        {
            var instance = CreateBizObject<Merchant_User>();
            var entity = new Merchant_User()
            {
                MerchantID = merchant.MerchantID,
                UserName = string.Format("Admin{0}",merchant.MerchantID),
                Password = Cryptographer.SHA1.Encrypt(ParamManager.GetStringValue("InitPassword")),//"123456"),
                RoleID = 1,
                Status = 1,
                InDate = DateTime.Now,
                InUser = merchant.InUser,
                EditDate = DateTime.Now,
                EditUser = merchant.EditUser
            };
            instance.Insert(entity);
            this.SaveChanges();
        }

        /// <summary>
        /// 增加商户财务信息
        /// </summary>
        /// <param name="finance"></param>
        /// <param name="merchantId"></param>
        public void AddMerchantFinance(MerchantFinanceModel finance)
        {
            var instance = CreateBizObject<Merchant_Finance>();
            Merchant_Finance mf = new Merchant_Finance()
            {
                BankAccount = finance.BankAccount,
                BankName = finance.BankName,
                SubBranch = finance.SubBranch,
                AccountName = finance.AccountName,
                AlipayAccount = finance.AlipayAccount,
                WechatAccount = finance.WechatAccount,
                FinanceContact = finance.FinanceContact,
                FinancePhone = finance.FinancePhone,
                MerchantID = finance.MerchantId,
                InDate = DateTime.Now,
                InUser = finance.UserId,
                EditDate = DateTime.Now,
                EditUser = finance.UserId,
            };
            instance.Insert(mf);
            this.SaveChanges();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="merchantId"></param>
        public void DeleteMerchant(int merchantId)
        {
            var instance = CreateBizObject<NHH.Entities.Merchant>();
            instance.Delete(merchantId);
            this.SaveChanges();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="detailModel"></param>
        /// <param name="financeModel"></param>
        public void UpdateMerchant(MerchantDetailModel detailModel, MerchantFinanceModel financeModel)
        {
            var conInstance = CreateBizObject<NHH.Entities.Merchant>();
            var finInstance = CreateBizObject<Merchant_Finance>();

            //增加商户信息
            var entity = conInstance.GetBySysNo(detailModel.MerchantId);
            if (entity != null)
            {
                entity.MerchantType = detailModel.MerchantType;
                entity.MerchantCode = detailModel.MerchantCode;
                entity.BriefName = detailModel.BriefName;
                entity.ProvinceID = detailModel.ProvinceId;
                entity.CityID = detailModel.CityId;
                entity.AddressLine = detailModel.AddressLine;
                entity.Zipcode = detailModel.Zipcode;
                entity.LicenseID = detailModel.LicenseId;
                entity.RegisterProvinceID = detailModel.RegisterProvinceId;
                entity.RegisterCityID = detailModel.RegisterCityId;
                entity.RegisterAddressLine = detailModel.RegisterAddressLine;
                entity.OwnerName = detailModel.OwnerName;
                entity.ContactName = detailModel.ContactName;
                entity.ContactIDNumber = detailModel.ContactIDNumber;
                entity.ContactEmail = detailModel.ContactEmail;
                entity.ContactPhone = detailModel.ContactPhone;
                entity.EditDate = DateTime.Now;
                entity.EditUser = detailModel.UserId;
                conInstance.Update(entity);
                this.SaveChanges();
            }
            //更新商户财务信息
            Expression<Func<Merchant_Finance, bool>> where = f => f.MerchantID == detailModel.MerchantId;
            var FinEntity = finInstance.TopOne(where);
            if (FinEntity != null)
            {
                FinEntity.BankAccount = financeModel.BankAccount;
                FinEntity.BankName = financeModel.BankName;
                FinEntity.AccountName = financeModel.AccountName;
                FinEntity.SubBranch = financeModel.SubBranch;
                FinEntity.AlipayAccount = financeModel.AlipayAccount;
                FinEntity.WechatAccount = financeModel.WechatAccount;
                FinEntity.FinanceContact = financeModel.FinanceContact;
                FinEntity.FinancePhone = financeModel.FinancePhone;
                FinEntity.EditDate = DateTime.Now;
                FinEntity.EditUser = detailModel.UserId;

                finInstance.Update(FinEntity);
                this.SaveChanges();
            }
            else
            {
                ///当更新商户信息，没有相关联的财务信息时增加财务信息
                financeModel.MerchantId = detailModel.MerchantId;
                AddMerchantFinance(financeModel);
            }
        }

        /// <summary>
        /// 获取商户类型
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        public int GetMerchantType(int merchantId)
        {
            return (from m in Context.Merchant
                    where m.MerchantID == merchantId
                    select m.MerchantType).FirstOrDefault();
        }
    }
}
