using System.Resources;
using NHH.Framework.Utility;
using NHH.Models.Merchant;
using NHH.Service.Merchant.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHH.Framework.Service;
using NHH.Entities;

using NHH.Models.Common;
using System.Linq.Expressions;
namespace NHH.Service.Merchant
{
    /// <summary>
    /// 商户服务
    /// </summary>
    public class MerchantService : NHHService<NHHEntities>, IMerchantService
    {
        /// <summary>
        /// 根据店铺编号获取店铺所在的铺位列表
        /// </summary>
        /// <param name="storeId"></param>
        /// <returns></returns>
        public List<SelectListItemInfo> GetUnitListByStoreId(int storeId)
        {
            var entityList = new List<SelectListItemInfo>();
            var query = from ms in Context.Merchant_Store
                join cu in Context.Contract_Unit on ms.RentContractID equals cu.ContractID
                join pu in Context.Project_Unit on cu.UnitID equals pu.UnitID
                join pb in Context.Project_Building on pu.BuildingID equals pb.BuildingID
                join pf in Context.Project_Floor on pu.FloorID equals pf.FloorID
                where ms.Status == 1 && cu.Status == 1 && ms.StoreID == storeId
                select new
                {
                    cu.UnitID,
                    UnitNumber = pb.BuildingName + " " + pf.FloorName + " " + pu.UnitNumber
                };
            var unitList = query.ToList();

            unitList.ForEach(item =>
            {
                var listModel = new SelectListItemInfo();
                listModel.Text = item.UnitNumber;
                listModel.Value = item.UnitID.ToString();
                entityList.Add(listModel);
            });
            return entityList;
        }

        /// <summary>
        /// 根据商户ID获取项目ID
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        public int GetProjectByMerchantId(int merchantId)
        {
            var projectId = (from ms in Context.Merchant_Store
                join ct in Context.Contract
                    on ms.RentContractID equals ct.ContractID
                where ms.RentContractID == ct.ContractID && ms.Status == 1 && ct.Status == 1
                select ct.ProjectID).FirstOrDefault();
            return projectId;
        }

        /// <summary>
        /// 获得精简的商家内容
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        public MerchantInfo GetSimpleMerchantDetail(int merchantId)
        {
            var model = new MerchantInfo();
            var entity = CreateBizObject<Entities.Merchant>().GetBySysNo(merchantId);
            if (entity != null)
            {
                model.MerchantID = entity.MerchantID;
                model.MerchantType = entity.MerchantType;
                model.BriefName = entity.BriefName;
                model.MerchantName = entity.MerchantName;
            }
            return model;
        }

        /// <summary>
        /// 获取商户详细信息
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        public MerchantDetailModel GetMerchantDetail(int merchantId)
        {
            var model = new MerchantDetailModel();
            //  var entity = CreateBizObject<NHH.Entities.Merchant>().GetBySysNo(merchantId);
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
                            m.ContactEmail,
                            AttachmentList = (from ma in Context.Merchant_Attachment where ma.MerchantID == m.MerchantID && ma.Status == 1
                                                  select new
                                                  {
                                                      ma.AttachmentID,
                                                      ma.AttachmentType,
                                                      ma.AttachmentName,
                                                      ma.AttachmentPath
                                                  }) 
                        };
            var entity = query.FirstOrDefault();

            if (entity != null)
            {
                model.MerchantID = entity.MerchantID;
                model.MerchantType = entity.MerchantType;
                model.MerchantTypeInfo = entity.MerchantType == 1 ? "公司" : "自然人";
                model.MerchantCode = entity.MerchantCode;
                model.BriefName = entity.BriefName;
                model.MerchantName = entity.MerchantName;
                model.ProvinceId = entity.ProvinceID.HasValue ? entity.ProvinceID.Value : 0;
                model.ProvinceName = entity.ProvinceName;
                model.CityId = entity.CityID.HasValue ? entity.CityID.Value : 0;
                model.CityName = entity.CityName;
                model.AddressLine = entity.ProvinceName + "-" + entity.CityName + "-" + entity.AddressLine;
                model.Zipcode = entity.Zipcode;
                model.LicenseId = entity.LicenseID;
                model.RegisterProvinceId = entity.RegisterProvinceID.HasValue ? entity.RegisterProvinceID.Value : 0;
                model.RegisterProvinceName = entity.RegisterProvinceName;
                model.RegisterCityId = entity.RegisterCityID.HasValue ? entity.RegisterCityID.Value : 0;
                model.RegisterCityName = entity.RegisterCityName;
                model.RegisterAddressLine = entity.RegisterAddressLine;
                model.OwnerName = entity.OwnerName;
                model.ContactName = entity.ContactName;
                model.ContactIDNumber = entity.ContactIDNumber;
                model.ContactPhone = entity.ContactPhone;
                model.ContactEmail = entity.ContactEmail;
                //获取商户财务信息
                model.FinanceInfo = GetFinance(entity.MerchantID);
                //获取附件信息
                //附件列表
                if (entity.AttachmentList != null)
                {
                    model.AttachmentList = new List<AttachmentInfo>();
                    entity.AttachmentList.ToList()
                        .ForEach(item => model.AttachmentList.Add(new AttachmentInfo
                        {
                            Id = item.AttachmentID,
                            Name = item.AttachmentName,
                            Path = UrlHelper.GetImageUrl(item.AttachmentPath, 100),
                            Type = item.AttachmentType,
                            OriginalPicPath = UrlHelper.GetImageUrl(item.AttachmentPath)
                        }));
                }
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
        /// <param name="merchantType"></param>
        /// <returns></returns>
        public MerchantListModel GetMerchantList(int? merchantType, PagingInfo pagingInfo)
        {
            var model = new MerchantListModel();
            model.MerchantList = new List<MerchantInfo>();

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

                            p.ProvinceName,

                            c.CityName
                        };

            if (merchantType.HasValue)
            {
                query = query.Where(item => item.MerchantType == merchantType.Value);
            }
            //分页信息
            pagingInfo.TotalCount = query.Count();
            query = query.OrderBy(m => m.MerchantID).Skip(pagingInfo.SkipNum).Take(pagingInfo.TakeNum);

            var merchantList = query.ToList();

            merchantList.ForEach(item =>
            {
                var listModel = new MerchantInfo();
                listModel.MerchantID = item.MerchantID;
                listModel.MerchantType = item.MerchantType;
                listModel.MerchantTypeInfo = item.MerchantType == 1 ? "公司" : "自然人";
                listModel.MerchantName = item.MerchantName;
                listModel.MerchantAddress = string.Format("{0}，{1}", item.ProvinceName, item.CityName);
                model.MerchantList.Add(listModel);
            });
            model.PagingInfo = pagingInfo;
            return model;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="detailModel"></param>
        /// <param name="financeModel"></param>
        /// <returns></returns>
        public bool AddMerchant(MerchantDetailModel detailModel)
        {
            var instance = CreateBizObject<NHH.Entities.Merchant>();
            NHH.Entities.Merchant merchant = new NHH.Entities.Merchant()
            {
                MerchantType = detailModel.MerchantType,
                MerchantName = detailModel.MerchantName,
                MerchantCode = detailModel.MerchantCode,
                BriefName = detailModel.BriefName,
                ProvinceID = detailModel.ProvinceId,
                CityID = detailModel.CityId,
                AddressLine = detailModel.AddressLine,
                Zipcode = detailModel.Zipcode,
                LicenseID = detailModel.LicenseId,
                RegisterProvinceID = detailModel.RegisterProvinceId,
                RegisterCityID = detailModel.RegisterCityId,
                RegisterAddressLine = detailModel.RegisterAddressLine,
                OwnerName = detailModel.OwnerName,
                ContactName = detailModel.ContactName,
                ContactIDNumber = detailModel.ContactIDNumber,
                ContactEmail = detailModel.ContactEmail,
                ContactPhone = detailModel.ContactPhone,
                Status = 1,
                InDate = DateTime.Now,
                InUser = 1,
                EditDate = DateTime.Now,
                EditUser = 1,
            };
            instance.Insert(merchant);
            this.SaveChanges();

            //增加商户财务信息
            detailModel.FinanceInfo.MerchantId = merchant.MerchantID;
            AddMerchantFinance(detailModel.FinanceInfo);

            return true;
        }

        
        /// <summary>
        /// 增加商户财务信息
        /// </summary>
        /// <param name="finance"></param>
        public bool AddMerchantFinance(MerchantFinanceModel finance)
        {
            var instance = CreateBizObject<Merchant_Finance>();
            Merchant_Finance mf = new Merchant_Finance()
            {
                BankAccount = finance.BankAccount,
                BankName = finance.BankName,
                SubBranch = finance.SubBranch,
                AccountName = finance.AccountName,
                //AlipayAccount = finance.AlipayAccount,
                //WechatAccount = finance.WechatAccount,
                FinanceContact = finance.FinanceContact,
                FinancePhone = finance.FinancePhone,
                MerchantID = finance.MerchantId,
                InDate = DateTime.Now,
                InUser = finance.InUser,
                EditDate = DateTime.Now,
                EditUser = finance.InUser,
            };
            instance.Insert(mf);
            this.SaveChanges();
            return true;
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
        public bool UpdateMerchant(MerchantDetailModel detailModel, MerchantFinanceModel financeModel)
        {
            bool result = false;
            var conInstance = CreateBizObject<NHH.Entities.Merchant>();
            var finInstance = CreateBizObject<Merchant_Finance>();

            //更新商户信息
            var entity = conInstance.GetBySysNo(detailModel.MerchantID);
            if (entity != null)
            {
                entity.MerchantType = detailModel.MerchantType;
                //entity.MerchantCode = detailModel.MerchantCode;
                entity.BriefName = detailModel.BriefName;
                entity.MerchantName = detailModel.MerchantName;
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
                //entity.ContactIDNumber = detailModel.ContactIDNumber;
                entity.ContactEmail = detailModel.ContactEmail;
                entity.ContactPhone = detailModel.ContactPhone;
                entity.EditDate = DateTime.Now;
                entity.EditUser = detailModel.EditUser;
                conInstance.Update(entity);
                this.SaveChanges();
                result =  true;
            }
            //商户信息更新成功后再更新财务信息
            if (result && financeModel != null)
            {
                financeModel.EditUser = detailModel.EditUser;
                //更新商户财务信息
                Expression<Func<Merchant_Finance, bool>> where = f => f.MerchantID == detailModel.MerchantID;
                var FinEntity = finInstance.TopOne(where);
                if (FinEntity != null)
                {
                    FinEntity.BankAccount = financeModel.BankAccount;
                    FinEntity.BankName = financeModel.BankName;
                    FinEntity.SubBranch = financeModel.SubBranch;
                    FinEntity.AccountName = financeModel.AccountName;
                    FinEntity.FinanceContact = financeModel.FinanceContact;
                    FinEntity.FinancePhone = financeModel.FinancePhone;

                    //FinEntity.AlipayAccount = financeModel.AlipayAccount;
                    //FinEntity.WechatAccount = financeModel.WechatAccount;
                    FinEntity.EditDate = DateTime.Now;
                    FinEntity.EditUser = financeModel.EditUser;

                    finInstance.Update(FinEntity);
                    this.SaveChanges();
                }
                else
                {
                    //当更新商户信息，没有相关联的财务信息时增加财务信息
                    financeModel.MerchantId = detailModel.MerchantID;
                    financeModel.InUser = detailModel.EditUser;
                    AddMerchantFinance(financeModel);
                }
            }
            return result;
        }

       
    }
}
