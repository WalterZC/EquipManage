using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipManage.Domain.Entity.SystemDocument
{
    public class EquipmentTypeEntity : IEntity<EquipmentTypeEntity>, ICreationAudited, IModificationAudited, IDeleteAudited
    {
        public string FId { get; set; }
        public string FNumber { get; set; }
        public string FFullName { get; set; }
        public string FShortName { get; set; }
        public string FStandard { get; set; }
        public string FModel { get; set; }
        public string FParentId { get; set; }
        public decimal FMachineCoefficient { get; set; }
        public decimal FElectricCoefficient { get; set; }
        public bool FIfUseChange { get; set; }
        public bool FIfPrecision { get; set; }
        public bool FIfRare { get; set; }
        public bool FIfLarge { get; set; }
        public string FSpecialType { get; set; }
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
        public bool? FIsTree { get; set; }
        public int FLayers { get; set; }
    }
}
