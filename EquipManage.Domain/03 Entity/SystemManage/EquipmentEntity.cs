/*******************************************************************************
 * Copyright © 2017 EquipManage.Framework 版权所有
 * Author: 王成元
 * Description: 设备管理系统-匠盟科技
 * Date:2017-03-01
*********************************************************************************/

using System;

namespace EquipManage.Domain.Entity.SystemManage
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
        public string FUseDate { get; set; }
        public string FBuyDate { get; set; }
        public string FReleaseDate { get; set; }
        public string FOrgVal { get; set; }
        public string FYearLife { get; set; }
        public string FAccuracy { get; set; }
        public string FSalvageRate { get; set; }
        public string FOrganizeId { get; set; }
        public string FBelongOrgID { get; set; }
        public string FUseOrgID { get; set; }
        public string FPositionID { get; set; }
        public string FRemark { get; set; }
        public string FDetail { get; set; }
        public string FLayers { get; set; }
        public string FParentID { get; set; }
        public string FManuOrgID { get; set; }
        public string FManuClassID { get; set; }
        public string FManuPrincipalID { get; set; }
        public string FOperatorID { get; set; }
        public string FABC { get; set; }
        public string FGuaranteeTime { get; set; }
        public string FFigure { get; set; }
        public string FFiles { get; set; }
        public string FPictures { get; set; }
        public string FPower { get; set; }
        public string FMachineCount { get; set; }
        public string FQRCode { get; set; }
        public string FEnergys { get; set; }
        public string FSource { get; set; }
        public string FOperationProjectID { get; set; }
        public string FFixedAssets { get; set; }
        public string FUnit { get; set; }
        public string FSpotCheckProjectID { get; set; }
        public string FIsSingleMeasurement { get; set; }
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
        public string FDeleteUserId { get; set; }
    }
}
