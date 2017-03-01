using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipManage.Domain.Entity.SystemDocument
{
    public class EquipmentSpotCheckItemEntity : IEntity<EquipmentSpotCheckItemEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        public string FId { get; set; }
        public string FItemID { get; set; }
        public string FEquipmentSystemID { get; set; }
        public string FEquipmentSystemItemID { get; set; }
        public string FImage { get; set; }
        public string FCheckContent { get; set; }
        public string FValType { get; set; }
        public decimal FMaxVal { get; set; }
        public decimal FMinVal { get; set; }
        public string FSetVal { get; set; }
        public bool FAllowEdit { get; set; }
        public bool FAllowDelete { get; set; }
        public string FSortCode { get; set; }
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
