
using System;
namespace EquipManage.Domain.Entity.SystemBusiness
{

    /// <summary>
    /// OperationalTaskFaultEntryEntity
    /// </summary>    
    public class OperationalTaskFaultEntryEntity : IEntity<OperationalTaskFaultEntryEntity>, ICreationAudited
    {

        public string FId { get; set; }

        public string FItemId { get; set; }

        public int? FEntryId { get; set; }

        public string FDefectLevelId { get; set; }

        public string FMalfunctionReasonId { get; set; }

        public string FMalfunctionTypeId { get; set; }

        public string FMalfunctionDetail { get; set; }

        public string FDescription { get; set; }

        public DateTime? FCreatorTime { get; set; }

        public string FCreatorUserId { get; set; }

        public string FFaultImage { get; set; }

        public string FFaultVideo { get; set; }

        public bool FMaintainType { get; set; }

        public string FProposerId { get; set; }

        public string FMaintainerId { get; set; }

        public decimal? FAmount { get; set; }

        public string FOuter { get; set; }

        public string FPhone { get; set; }

        public string FMalfunctionSummary { get; set; }

        public string FMalfunctionReason { get; set; }

    }

}
