using System;
namespace EquipManage.Domain.Entity.SystemDocument
{
    /// <summary>
    /// EquipmentPartsEntity
    /// </summary>    
    public class EquipmentPartsEntity : IEntity<EquipmentPartsEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        public string FId { get; set; }
        public string FItemId { get; set; }
        public byte[] FImage { get; set; }
        public string FSystemId { get; set; }
        public string FName { get; set; }
        public bool? FAllowEdit { get; set; }
        public bool? FAllowDelete { get; set; }
        public bool? FEnabledMark { get; set; }
        public bool? FDeleteMark { get; set; }
        public DateTime? FCreatorTime { get; set; }
        public string FCreatorUserId { get; set; }
        public DateTime? FLastModifyTime { get; set; }
        public string FLastModifyUserId { get; set; }
        public DateTime? FDeleteTime { get; set; }
        public string FDeleteUserId { get; set; }
        public string FOrganizeId { get; set; }
        public int? FSortCode { get; set; }
        public string FContentLength { get; set; }
        public string FContentType { get; set; }
        public string FFileName { get; set; }

    }

}