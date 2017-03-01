using System;

namespace EquipManage.Domain.Entity.SystemDocument
{
    public class MaintainEntity : IEntity<MaintainEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        public string FId { get; set; }
        public string FNumber { get; set; }
        public string FFullName { get; set; }
        public string FShortName { get; set; }
        public string FLayers { get; set; }
        public string FAddress { get; set; }
        public string FRate { get; set; }
        public string FLinkMan { get; set; }
        public string FTelePhone { get; set; }
        public string FMobilePhone { get; set; }
        public string FWeChat { get; set; }
        public string FFax { get; set; }
        public string FEmail { get; set; }
        public string FSource { get; set; }
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
        public string FOrganizeId { get; set; }
    }
}
