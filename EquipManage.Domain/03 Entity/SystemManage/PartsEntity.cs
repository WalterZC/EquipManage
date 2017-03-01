using System;

namespace EquipManage.Domain.Entity.SystemManage
{
    public class PartsEntity:IEntity<PartsEntity>,IDeleteAudited,IModificationAudited,ICreationAudited
    {
        public string FId { get; set; }
        public string FNumber { get; set; }
        public string FFullName { get; set; }
        public string FShortName { get; set; }
        public string FModel { get; set; }
        public string FCost { get; set; }
        public string FCategory { get; set; }
        public string FSupply { get; set; }
        public string FFigure { get; set; }
        public string FManufacturer { get; set; }
        public string FWarehouse { get; set; }
        public string FWarehousePlace { get; set; }
        public string FPlace { get; set; }
        public string FSource { get; set; }
        public string FIsTree { get; set; }
        public int? FLayers { get; set; }
        public bool? FAllowEdit { get; set; }
        public bool? FAllowDelete { get; set; }
        public int FSortCode { get; set; }
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
