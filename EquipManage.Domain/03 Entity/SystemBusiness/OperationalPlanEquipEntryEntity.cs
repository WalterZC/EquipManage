using System;
namespace EquipManage.Domain.Entity.SystemBusiness
{
    /// <summary>
    /// OperationalPlanEquipEntryEntity
    /// </summary>    
    public class OperationalPlanEquipEntryEntity : IEntity<OperationalPlanEquipEntryEntity>,ICreationAudited
    {
        public string FId { get; set; }
        public string FItemId { get; set; }
        public int? FEntryId { get; set; }
        public string FObjectTypeId { get; set; }
        public string FObjectId { get; set; }
        public string FOperationClassId { get; set; }
        public string FOperatorId { get; set; }
        public string FOperationProjectId { get; set; }
        public string FDescription { get; set; }
        public string FProjectId { get; set; }
        public DateTime? FCreatorTime { get; set; }
        public string FCreatorUserId { get; set; }

        //public virtual OperationalPlanEntity OperationalPlanEntity { get; set; }
    }
}
