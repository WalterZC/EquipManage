using EquipManage.Domain.Entity.SystemDocument;
using System.Data.Entity.ModelConfiguration;
namespace EquipManage.Mapping.SystemDocument
{    
    /// <summary>
    /// EquipmentPartsMap
    /// </summary>    
    public class EquipmentPartsMap:EntityTypeConfiguration<EquipmentPartsEntity>
    {
       public EquipmentPartsMap()
       {
          this.ToTable("Sys_EquipmentParts");
          this.HasKey(t=>t.FId);
       }
    }
}
