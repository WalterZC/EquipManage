using System;

namespace EquipManage.Domain.Entity.SystemDocument
{
    public class ExpWareEntity : IEntity<ExpWareEntity>, ICreationAudited, IModificationAudited, IDeleteAudited
    {
        public string FId { get; set; }
        public string FNumber { get; set; }
        public string FFullName { get; set; }
        public string FShortName { get; set; }
        public string FOperationSummary { get; set; }
        public string FOperationDetail { get; set; }
        public string FOperationTypeID { get; set; }
        public string FItemID { get; set; }
        public string FEquipTypeID { get; set; }
        public string FOrganizeId { get; set; }
        public int FSortCode { get; set; }
        public bool FAllowEdit { get; set; }
        public bool FAllowDelete { get; set; }
        public bool? FDeleteMark { get; set; }
        public bool? FEnabledMark { get; set; }
        public string FDescription { get; set; }
        public DateTime? FCreatorTime { get; set; }
        public string FCreatorUserId { get; set; }
        public DateTime? FLastModifyTime { get; set; }
        public string FLastModifyUserId { get; set; }
        public DateTime? FDeleteTime { get; set; }
        public string FDeleteUserId { get; set; }
        public string FMalfunctionType { get; set; }
        public string FOperationLevelId { get; set; }
        public string FMalfunctionSummary { get; set; }
        public string FMalfunctionDetail { get; set; }
        public string FMalfunctionReasonId { get; set; }
        public string FMalfunctionReason { get; set; }
        public string FMalfunctionImages { get; set; }
        public string FOperationImages { get; set; }
    }
}
