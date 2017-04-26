
using System;
namespace EquipManage.Domain.Entity.SystemBusiness
{

    /// <summary>
    /// OperationalTaskEquipEntryEntity
    /// </summary>    
    public class OperationalTaskEquipEntryEntity : IEntity<OperationalTaskEquipEntryEntity>, ICreationAudited
    {

        public string FId { get; set; }

        public string FItemId { get; set; }

        public int? FEntryId { get; set; }

        public string FOperationClassId { get; set; }

        public string FOperatorId { get; set; }

        public string FDescription { get; set; }

        public string FProjectId { get; set; }

        public string FCreatorUserId { get; set; }

        public DateTime? FCreatorTime { get; set; }

        public string FEquipmentPartsId { get; set; }

    }

}
