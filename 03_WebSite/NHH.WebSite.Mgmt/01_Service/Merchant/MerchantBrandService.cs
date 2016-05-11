using NHH.Entities;
using NHH.Framework.Service;
using NHH.Models.Merchant;
using NHH.Service.Merchant.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Merchant
{
    /// <summary>
    /// 商户品牌服务
    /// </summary>
    public class MerchantBrandService : NHHService<NHHEntities>, IMerchantBrandService
    {
        /// <summary>
        /// 获取商户品牌列表
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public MerchantBrandListModel GetBrandList(int merchantId, int? page)
        {
            var model = new MerchantBrandListModel();
            model.MerchantId = merchantId;
            model.BrandList = new List<MerchantBrandInfo>();
            model.PagingInfo = new Models.Common.PagingInfo
            {
                PageIndex = page.HasValue ? page.Value : 1
            };

            var query = from mb in Context.Merchant_Brand
                        join b in Context.Brand on mb.BrandID equals b.BrandID
                        join d1 in Context.Dictionary on mb.BrandType equals d1.FieldValue
                        join d2 in Context.Dictionary on b.BrandLevel equals d2.FieldValue
                        where mb.MerchantID == merchantId && d1.FieldType == "BrandType" && d2.FieldType == "BrandLevel" && mb.Status == 1
                        select new
                        {
                            mb.MerchantBrandID,
                            mb.MerchantID,
                            mb.BrandType,
                            BrandTypeName = d1.FieldName,
                            b.BrandID,
                            b.BrandName,
                            b.BrandLevel,
                            BrandLevelName = d2.FieldName,
                            b.BizType.BizTypeName,
                            b.BizCategory.BizCategoryName,
                            mb.Authorized,
                            mb.AuthorizedSince,
                            mb.AuthorizedTo,
                            mb.OperationSince,
                            mb.OperationTo,
                        };
            model.PagingInfo.TotalCount = query.Count();

            var list = query.OrderBy(item => item.MerchantBrandID).Skip(model.PagingInfo.SkipNum).Take(model.PagingInfo.TakeNum).ToList();
            if (list == null || list.Count == 0)
                return model;
            list.ForEach(entity =>
            {
                var brand = new MerchantBrandInfo
                {
                    MerchantBrandID = entity.MerchantBrandID,
                    MerchantID = entity.MerchantID,
                    BrandID = entity.BrandID,
                    BrandName = entity.BrandName,
                    BrandTypeId = entity.BrandType,
                    BrandType = entity.BrandTypeName,
                    BrandLevel = entity.BrandLevelName,
                    BizType = entity.BizTypeName,
                    BizCategory = entity.BizCategoryName,
                    Authorized = entity.Authorized,
                    AuthorizedSince = entity.AuthorizedSince,
                    AuthorizedTo = entity.AuthorizedTo,
                };
                model.BrandList.Add(brand);
            });

            return model;
        }

        /// <summary>
        /// 获取商户品牌详情
        /// </summary>
        /// <param name="mbId"></param>
        /// <returns></returns>
        public MerchantBrandDetailModel GetBrandDetail(int mbId)
        {
            var model = new MerchantBrandDetailModel();

            var query = from mb in Context.Merchant_Brand
                        join b in Context.Brand on mb.BrandID equals b.BrandID
                        join d in Context.Dictionary on b.BrandLevel equals d.FieldValue
                        where mb.MerchantBrandID == mbId && d.FieldType == "BrandLevel" && mb.Status == 1
                        select new
                        {
                            mb.MerchantBrandID,
                            mb.BrandType,
                            mb.MerchantID,
                            mb.BrandID,
                            b.BrandName,
                            b.BrandLevel,
                            BrandLevelName = d.FieldName,
                            BizTypeName = b.BizType.BizTypeName,
                            BizCategoryName = b.BizCategory.BizCategoryName,
                            mb.Authorized,
                            mb.AuthorizedFile,
                            mb.AuthorizedSince,
                            mb.AuthorizedTo,
                            mb.OperationSince,
                            mb.OperationTo,
                            mb.Revenue,
                        };
            var entity = query.FirstOrDefault();
            if (entity != null)
            {
                model.MerchantBrandID = entity.MerchantBrandID;
                model.MerchantID = entity.MerchantID;
                model.BrandID = entity.BrandID;
                model.BrandName = entity.BrandName;
                model.BrandTypeId = entity.BrandType;
                model.Authorized = entity.Authorized;
                model.AuthorizedFile = entity.AuthorizedFile;
                model.AuthorizedSince = entity.AuthorizedSince;
                model.AuthorizedTo = entity.AuthorizedTo;
                model.OperationSince = entity.OperationSince;
                model.OperationTo = entity.OperationTo;
                model.Revenue = entity.Revenue;
                model.BrandLevel = entity.BrandLevelName;
            }

            if (model.AuthorizedFile.HasValue && model.AuthorizedFile.Value > 0)
            {
                var attEntity = CreateBizObject<NHH.Entities.Merchant_Attachment>().GetBySysNo(model.AuthorizedFile.Value);
                if (attEntity != null)
                {
                    model.AuthorizedFileName = attEntity.AttachmentName;
                    model.AuthorizedFilePath = attEntity.AttachmentPath;
                }
            }

            return model;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        public void Add(MerchantBrandDetailModel model)
        {
            if (!string.IsNullOrEmpty(model.AuthorizedFilePath))
            {
                var attEntity = new NHH.Entities.Merchant_Attachment
                {
                    MerchantID = model.MerchantID,
                    AttachmentName = model.AuthorizedFileName,
                    AttachmentPath = model.AuthorizedFilePath,
                    AttachmentType = 5,
                    Status = 1,
                    InDate = DateTime.Now,
                    InUser = model.InUser,
                    EditDate = DateTime.Now,
                    EditUser = model.InUser
                };
                var attBll = CreateBizObject<NHH.Entities.Merchant_Attachment>();
                attBll.Insert(attEntity);
                this.SaveChanges();

                model.Authorized = 1;
                model.AuthorizedFile = attEntity.AttachmentID;
            }

            var entity = new NHH.Entities.Merchant_Brand
            {
                MerchantID = model.MerchantID,
                BrandID = model.BrandID,
                BrandType = model.BrandTypeId,
                Authorized = model.Authorized,
                AuthorizedFile = model.AuthorizedFile,
                AuthorizedSince = model.AuthorizedSince,
                AuthorizedTo = model.AuthorizedTo,
                OperationSince = model.OperationSince,
                OperationTo = model.OperationTo,
                Revenue = model.Revenue,
                Status = 1,
                InDate = DateTime.Now,
                InUser = model.InUser,
                EditDate = DateTime.Now,
                EditUser = model.InUser,
            };
            var bll = CreateBizObject<NHH.Entities.Merchant_Brand>();
            bll.Insert(entity);
            this.SaveChanges();
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="model"></param>
        public void Edit(MerchantBrandDetailModel model)
        {
            var bll = CreateBizObject<NHH.Entities.Merchant_Brand>();

            var entity = bll.GetBySysNo(model.MerchantBrandID);
            if (entity == null)
            {
                return;
            }

            //重新上传附件
            if (entity.AuthorizedFile != model.AuthorizedFile)
            {
                //老附件删除
                var attBll = CreateBizObject<NHH.Entities.Merchant_Attachment>();
                if (entity.AuthorizedFile.HasValue)
                {
                    attBll.Delete(entity.AuthorizedFile.Value);
                }
                //新附件
                var attEntity = new NHH.Entities.Merchant_Attachment
                {
                    MerchantID = model.MerchantID,
                    AttachmentName = model.AuthorizedFileName,
                    AttachmentPath = model.AuthorizedFilePath,
                    AttachmentType = 5,
                    Status = 1,
                    InDate = DateTime.Now,
                    InUser = 1,
                    EditDate = DateTime.Now,
                    EditUser = 0
                };
                attBll.Insert(attEntity);
                this.SaveChanges();

                model.Authorized = 1;
                model.AuthorizedFile = attEntity.AttachmentID;

            }

            entity.BrandID = model.BrandID;
            entity.BrandType = model.BrandTypeId;
            entity.Authorized = model.Authorized;
            entity.AuthorizedFile = model.AuthorizedFile;
            entity.AuthorizedSince = model.AuthorizedSince;
            entity.AuthorizedTo = model.AuthorizedTo;
            entity.OperationSince = model.OperationSince;
            entity.OperationTo = model.OperationTo;
            entity.Revenue = model.Revenue;
            entity.EditDate = DateTime.Now;
            entity.EditUser = 1;
            bll.Update(entity);
            this.SaveChanges();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="brandId"></param>
        public MessageBag<bool> DelelteMerchantBrand(int merchantBrandId)
        {
            var message = new MessageBag<bool>();

            var contractId = (from mb in Context.Merchant_Brand
                              join cb in Context.Contract_Brand on mb.MerchantBrandID equals cb.MerchantBrandID
                              join c in Context.Contract on cb.ContractID equals c.ContractID
                              where (new int[] { 1, 2, 3 }).Contains(c.ContractStatus) && mb.MerchantBrandID == merchantBrandId
                              select c.ContractID).FirstOrDefault();
            if (contractId > 0)
            {
                message.Code = -1;
                message.Desc = "此品牌存在合约不可作废";
                return message;
            }


            var bll = CreateBizObject<NHH.Entities.Merchant_Brand>();
            bll.Delete(merchantBrandId);
            this.SaveChanges();

            message.Desc = "作废成功";

            return message;
        }

        /// <summary>
        /// 根据商户id返回所有其经营品牌信息
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        public List<MerchantBrandInfo> GetBrandListAll(int merchantId)
        {

            List<MerchantBrandInfo> BrandList = new List<MerchantBrandInfo>();

            var query = from mb in Context.Merchant_Brand
                        join b in Context.Brand on mb.BrandID equals b.BrandID
                        join m in Context.Merchant on mb.MerchantID equals m.MerchantID
                        join d1 in Context.Dictionary on mb.BrandType equals d1.FieldValue
                        join d2 in Context.Dictionary on b.BrandLevel equals d2.FieldValue
                        where mb.MerchantID == merchantId && d1.FieldType == "BrandType" && d2.FieldType == "BrandLevel" && mb.Status == 1
                        select new
                        {
                            mb.MerchantBrandID,
                            mb.BrandType,
                            BrandTypeName = d1.FieldName,
                            b.BrandID,
                            b.BrandName,
                            b.BrandLevel,
                            BrandLevelName = d2.FieldName,
                            b.BizType.BizTypeName,
                            b.BizCategory.BizCategoryName,
                            mb.MerchantID,
                            m.MerchantName,
                            mb.Authorized,
                            mb.AuthorizedSince,
                            mb.AuthorizedTo,
                            mb.OperationSince,
                            mb.OperationTo,
                        };

            var entityList = query.ToList();
            if (entityList == null || entityList.Count == 0)
            {
                return BrandList;
            }

            entityList.ForEach(entity =>
            {
                var brand = new MerchantBrandInfo
                {
                    MerchantBrandID = entity.MerchantBrandID,
                    BrandID = entity.BrandID,
                    BrandName = entity.BrandName,
                    BrandTypeId = entity.BrandType,
                    BrandType = entity.BrandTypeName,
                    BrandLevel = entity.BrandLevelName,
                    BizType = entity.BizTypeName,
                    BizCategory = entity.BizCategoryName,
                    MerchantID = entity.MerchantID,
                    MerchantName = entity.MerchantName,
                    Authorized = entity.Authorized,
                    AuthorizedSince = entity.AuthorizedSince,
                    AuthorizedTo = entity.AuthorizedTo,
                };
                BrandList.Add(brand);
            });

            return BrandList;

        }
    }
}
