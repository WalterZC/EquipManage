using System;
namespace EquipManage.Domain.Entity.SystemDocument
{
    /// <summary>
    /// ItemRightEntity
    /// </summary>    
    public class ItemRightEntity : IEntity<ItemRightEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        public string FId { get; set; }
        public string FObjectType { get; set; }
        public string FObjectId { get; set; }
        public string FUserId { get; set; }
        public bool? FAccessType { get; set; }
        public DateTime? FCreatorTime { get; set; }
        public string FCreatorUserId { get; set; }
        public DateTime? FLastModifyTime { get; set; }
        public string FLastModifyUserId { get; set; }
        public DateTime? FDeleteTime { get; set; }
        public string FDeleteUserId { get; set; }
        public bool? FDeleteMark { get; set; }

    }
}

