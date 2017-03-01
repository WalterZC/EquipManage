using System;

namespace EquipManage.Domain.Entity.SystemManage
{
    public class OperationItemEntity : IEntity<EquipmentEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        public string FId { get; set; }
        public string FNumber { get; set; }
        public string FFullName { get; set; }
        public string FShortName { get; set; }
        public string FOperationTypeID { get; set; }
        public string FSortCode { get; set; }
        public bool? FAllowEdit { get; set; }
        public bool? FAllowDelete { get; set; }
        public bool? FDeleteMark { get; set; }
        public bool? FEnabledMark { get; set; }
        public string FDescription { get; set; }
        public DateTime? FCreatorTime { get; set; }
        public string FCreatorUserId { get; set; }
        public DateTime? FLastModifyTime { get; set; }
        public string FLastModifyUserId { get; set; }
        public DateTime? FDeleteTime { get; set; }
        public string FDeleteUserId { get; set; }
        public string FOrganizeId { get; set; }
    }
}
