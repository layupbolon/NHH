using NHH.Entities;
using NHH.Framework.Service;
using NHH.Models.Contract;
using NHH.Service.Contract.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Contract
{
    /// <summary>
    /// 合同管理服务
    /// </summary>
    public class ContractMgmtService : NHHService<NHHEntities>, IContractMgmtService
    {
        /// <summary>
        /// 更新铺位信息
        /// </summary>
        /// <param name="info"></param>
        public void UpdateUnitInfo(ContractUnitInfo info)
        {
            var bll = CreateBizObject<NHH.Entities.Contract>();
            var entity = bll.GetBySysNo(info.ContractID);
            if (entity == null)
                return;

            entity.ContractArea = info.ContractArea;
            entity.EditDate = DateTime.Now;
            bll.Update(entity);
            this.SaveChanges();
        }

        /// <summary>
        /// 更新合同信息
        /// </summary>
        /// <param name="info"></param>
        public void UpdateContractInfo(ContractInfo info)
        {
            var bll = CreateBizObject<NHH.Entities.Contract>();
            var entity = bll.GetBySysNo(info.ContractID);
            if (entity == null)
                return;
            int days = (info.ContractEndDate - info.ContractStartDate).Days;
            entity.ContractLength = days;
            entity.ContractStartDate = info.ContractStartDate;
            entity.ContractEndDate = info.ContractEndDate;
            days = (info.RentFreeEndDate - info.RentFreeStartDate).Days;
            entity.RentFreeLength = days == 0 ? 0 : days / 30;
            entity.RentFreeStartDate = info.RentFreeStartDate;
            entity.RentFreeEndDate = info.RentFreeEndDate;
            entity.AccessDate = info.AccessDate;
            entity.BidBond = info.BidBond;
            entity.ManageBond = info.ManageBond;
            entity.DecorationLength = info.DecorationLength;
            entity.DecorationStartDate = info.DecorationStartDate;
            entity.DecorationEndDate = info.DecorationEndDate;
            entity.DecorationMgmtFee = info.DecorationMgmtFee;
            entity.DecorationBond = info.DecorationBond;
            entity.QualityBond = info.QualityBond;
            entity.ParkingLotNum = info.ParkingLotNum;
            entity.ParkingLotMemo = info.ParkingLotMemo;
            entity.AdPointNum = info.AdPointNum;
            entity.AdPointMemo = info.AdPointMemo;
            entity.SignerName = info.SignerName;
            entity.SignerPhone = info.SignerPhone;
            entity.SignerIDNumber = info.SignerIDNumber;
            entity.OperatorName = info.OperatorName;
            entity.OperatorPhone = info.OperatorPhone;
            entity.EditDate = DateTime.Now;
            bll.Update(entity);
            this.SaveChanges();
        }

        /// <summary>
        /// 更新租金
        /// </summary>
        /// <param name="info"></param>
        public void UpdateRentPaymentInfo(PaymentTermInfo info)
        {
            var bll = CreateBizObject<NHH.Entities.Contract_PaymentTerms>();

            var entity = bll.TopOne(item => item.ContractID == info.ContractID && item.PaymentItemID == 1);
            if (entity == null)
                return;

            entity.PaymentMonthlyAmount = info.PaymentMonthlyAmount;
            entity.PaymentDailyAmount = info.PaymentMonthlyAmount / 30;
            entity.DepositMonthly = info.DepositMonthly;
            entity.PaymentPeriod = info.PaymentPeriod;
            entity.PaymentTermsType = info.PaymentTermsType;
            entity.EditDate = DateTime.Now;

            bll.Update(entity);
            this.SaveChanges();

            var contractBll = CreateCacheBizObject<NHH.Entities.Contract>();
            var contractEntity = contractBll.GetBySysNo(info.ContractID);
            if (contractEntity != null)
            {
                //租金单价
                contractEntity.ContractUnitRent = entity.PaymentDailyAmount.Value / contractEntity.ContractArea;
                contractEntity.EditDate = DateTime.Now;
                contractBll.Update(contractEntity);
                this.SaveChanges();
            }
        }

        /// <summary>
        /// 更新物业信息
        /// </summary>
        /// <param name="info"></param>
        public void UpdateMgmtPaymentInfo(PaymentTermInfo info)
        {
            var bll = CreateBizObject<NHH.Entities.Contract_PaymentTerms>();

            var entity = bll.TopOne(item => item.ContractID == info.ContractID && item.PaymentItemID == 2);
            if (entity == null)
                return;
            //月物业费
            entity.PaymentMonthlyAmount = info.PaymentMonthlyAmount;
            //日物业费
            entity.PaymentDailyAmount = entity.PaymentMonthlyAmount / 30;
            entity.EditDate = DateTime.Now;

            bll.Update(entity);
            this.SaveChanges();

            var contractBll = CreateCacheBizObject<NHH.Entities.Contract>();
            var contractEntity = contractBll.GetBySysNo(info.ContractID);
            if (contractEntity != null)
            {
                //物业费单价
                contractEntity.ContractMgmtFee = entity.PaymentDailyAmount.Value / contractEntity.ContractArea;
                contractEntity.EditDate = DateTime.Now;
                contractBll.Update(contractEntity);
                this.SaveChanges();
            }
        }

        /// <summary>
        /// 更新合同的交付标准和商户责任
        /// </summary>
        /// <param name="info"></param>
        public void UpdateUnitSpec(ContractUnitSpecInfo info)
        {
            var bll = CreateBizObject<NHH.Entities.Contract_UnitSpec>();
            var entity = bll.TopOne(item => item.ContractID == info.ContractID && item.SpecType == info.SpecType);
            if (entity == null)
                return;

            entity.Floor = info.Floor;
            entity.Ceiling = info.Ceiling;
            entity.Wall = info.Wall;
            entity.Pillar = info.Pillar;
            entity.FloorBearing = info.FloorBearing;
            entity.WaterSupply = info.WaterSupply;
            entity.WaterDrain = info.WaterDrain;
            entity.Door = info.Door;
            entity.Logo = info.Logo;
            entity.ElectricityUsage = info.ElectricityUsage;
            entity.FireProtection = info.FireProtection;
            entity.Broadcasting = info.Broadcasting;
            entity.AirCondition = info.AirCondition;
            entity.Smoke = info.Smoke;
            entity.Security = info.Security;
            entity.Wiring = info.Wiring;
            entity.Gas = info.Gas;
            entity.EditDate = DateTime.Now;

            bll.Update(entity);
            this.SaveChanges();
        }
    }
}
