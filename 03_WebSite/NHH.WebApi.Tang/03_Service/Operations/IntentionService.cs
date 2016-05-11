using System;
using System.Collections.Generic;
using NHH.Entities;
using NHH.Framework.Service;
using NHH.Models.Operations;
using NHH.Service.Operations.IService;

namespace NHH.Service.Operations
{
    public class IntentionService :  NHHService<NHHEntities>,IIntentionService
    {
        #region 入驻意向
        /// <summary>
        /// 添加入驻意向
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddIntention(IntentionInfo model)
        {
            var instance = CreateBizObject<Intention>();
            var entity = new Intention()
            {
                IntentionType = model.IntentionType,
                ContactName = model.ContactName,
                ContactPhone = model.ContactPhone,
                ProjectName = model.ProjectName,
                Remark = model.Remark,
                Source = model.Source,
                InDate = DateTime.Now,
                Status = 1
            };
            instance.Insert(entity);
            this.SaveChanges();
            model.IntentionID = entity.IntentionID;
            return model.IntentionID > 0;
        }
        #endregion 入驻意向
    }
}
