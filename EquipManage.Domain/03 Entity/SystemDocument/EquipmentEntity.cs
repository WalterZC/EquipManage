/*******************************************************************************
 * Copyright © 2017 EquipManage.Framework 版权所有
 * Author: 王成元
 * Description: 设备管理系统-匠盟科技
 * Date:2017-03-01
*********************************************************************************/

using System;

namespace EquipManage.Domain.Entity.SystemDocument
{
    public class EquipmentEntity : IEntity<EquipmentEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        public string FId { get; set; }
        public string FNumber { get; set; }
        public string FFullName { get; set; }
        public string FShortName { get; set; }
        public string FModel { get; set; }
        public string FGood { get; set; }
        public string FCode { get; set; }
        public string FSerialNo { get; set; }
        public string FManufacturer { get; set; }
        public string FSupply { get; set; }
        public string FCategory { get; set; }
        public DateTime? FUseDate { get; set; }
        public DateTime? FBuyDate { get; set; }
        public DateTime? FReleaseDate { get; set; }
        public decimal FOrgVal { get; set; }
        public int FYearLife { get; set; }
        public string FAccuracy { get; set; }
        public decimal FSalvageRate { get; set; }
        public string FOrganizeId { get; set; }
        public string FBelongOrgID { get; set; }
        public string FUseOrgID { get; set; }
        public string FPositionID { get; set; }
        public string FRemark { get; set; }
        public bool? FDetail { get; set; }
        public int? FLayers { get; set; }
        public string FParentID { get; set; }
        public string FManuOrgID { get; set; }
        public string FManuClassID { get; set; }
        public string FManuPrincipalID { get; set; }
        public string FOperatorID { get; set; }
        public string FABC { get; set; }
        public DateTime? FGuaranteeTime { get; set; }
        public string FFigure { get; set; }
        public string FFiles { get; set; }
        public string FPictures { get; set; }
        public int FPower { get; set; }
        public int FMachineCount { get; set; }
        public string FQRCode { get; set; }
        public string FEnergys { get; set; }
        public string FSource { get; set; }
        public string FOperationProjectID { get; set; }
        public string FFixedAssets { get; set; }
        public string FUnit { get; set; }
        public string FSpotCheckProjectID { get; set; }
        public bool? FIsSingleMeasurement { get; set; }
        public bool? FAllowEdit { get; set; }
        public bool? FAllowDelete { get; set; }
        public int? FSortCode { get; set; }
        public bool? FDeleteMark { get; set; }
        public bool? FEnabledMark { get; set; }
        public string FDescription { get; set; }
        public DateTime? FCreatorTime { get; set; }
        public string FCreatorUserId { get; set; }
        public DateTime? FLastModifyTime { get; set; }
        public string FLastModifyUserId { get; set; }
        public DateTime? FDeleteTime { get; set; }
        public string FStatus { get; set; }
        public string FDeleteUserId { get; set; }
        public string FEquipmentTypeId { get; set; }
        public string FStandard { get; set;}
        public string FPrincipalId { get; set; }
    }
}
