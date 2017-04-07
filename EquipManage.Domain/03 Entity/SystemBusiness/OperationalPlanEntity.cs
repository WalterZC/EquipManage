using System;
namespace EquipManage.Domain.Entity.SystemBusiness
{
    /// <summary>
    /// OperationalPlanEntity
    /// </summary>    
    public class OperationalPlanEntity : IEntity<OperationalPlanEntity>, ICreationAudited, IDeleteAudited, IModificationAudited, ICheckAudited, ICancelAudited
    {
        public string FId { get; set; }
        public string FNumber { get; set; }
        public string FName { get; set; }
        public bool? FCheckMark { get; set; }
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
        public int? FCyclicTypeID { get; set; }
        public string FCyclicTypeName { get; set; }
        public string FOperationLevelId { get; set; }
        public DateTime? FLastOperaDate { get; set; }
        public int? FInterval { get; set; }
        public DateTime? FStartDate { get; set; }
        public bool FIsCreateTask { get; set; }
        public bool FCreateStatus { get; set; }
        public bool FMultiCheckStatus { get; set; }
        public string FOrganizeId { get; set; }
        public int? FRunTimes { get; set; }
        public DateTime? FEndDate { get; set; }
        public string FOperationProjectId { get; set; }
        public string FCheckerId { get; set; }
        public DateTime? FCheckTime { get; set; }
    }
}

