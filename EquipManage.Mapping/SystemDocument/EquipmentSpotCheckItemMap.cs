using EquipManage.Domain.Entity.SystemDocument;
using System.Data.Entity.ModelConfiguration;

namespace EquipManage.Mapping.SystemDocument
{
    public class EquipmentSpotCheckItemMap:EntityTypeConfiguration<EquipmentSpotCheckItemEntity>
    {
        public EquipmentSpotCheckItemMap()
        {
            this.ToTable("Sys_EquipmentSpotCheckItem");
            this.HasKey(t => t.FId);
        }
    }
}
