
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

    }

}
