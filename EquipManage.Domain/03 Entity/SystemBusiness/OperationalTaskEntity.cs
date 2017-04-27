
using System;
namespace EquipManage.Domain.Entity.SystemBusiness
{

    /// <summary>
    /// OperationalTaskEntity
    /// </summary>    
    public class OperationalTaskEntity : IEntity<OperationalTaskEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {

        public string FId { get; set; }

        public string FBillNo { get; set; }

        public string FName { get; set; }

        public bool FCheckMark { get; set; }

        public int? FSortCode { get; set; }

        public bool? FDeleteMark { get; set; }

        public bool FCanceledMark { get; set; }

        public string FDescription { get; set; }

        public DateTime? FCreatorTime { get; set; }

        public string FCreatorUserId { get; set; }

        public DateTime? FLastModifyTime { get; set; }

        public string FLastModifyUserId { get; set; }

        public DateTime? FDeleteTime { get; set; }

        public string FDeleteUserId { get; set; }

        public string FCancelUserId { get; set; }

        public DateTime? FCancelTime { get; set; }

        public string FOperationTypeId { get; set; }

        public string FOperationLevelId { get; set; }

        public DateTime? FStartDate { get; set; }

        public bool FIsCreateRecord { get; set; }

        public bool FCreateStatus { get; set; }

        public bool FMultiCheckStatus { get; set; }

        public string FOrganizeId { get; set; }

        public DateTime? FEndDate { get; set; }

        public string FCheckerId { get; set; }

        public DateTime? FCheckTime { get; set; }

        public string FObjectTypeId { get; set; }

        public string FObjectId { get; set; }

        public int? FUseDay { get; set; }

        public string FDanger { get; set; }

        public int? FStatus { get; set; }

        public string FTaskAssigner { get; set; }

        public DateTime? FTaskAssignDate { get; set; }

        public string FSourceType { get; set; }

        public string FSourceInterId { get; set; }

        public string FSourceBillNo { get; set; }

        public int? FSourceEntryId { get; set; }

        public string FCreateType { get; set; }

        public string FOnlineMonitorId { get; set; }

        public string FEquipManagerId { get; set; }

        public string FPositionId { get; set; }

        public string FUseDeptId { get; set; }

        public string FUseDeptManagerId { get; set; }

        public string FOperationProjectId { get; set; }

        public int? FPickingMark { get; set; }

        public string FPickingUserId { get; set; }

        public DateTime? FUsePartsDate { get; set; }

        public string FExpWareId { get; set; }

        public string FRunningStatus { get; set; }

    }

}
