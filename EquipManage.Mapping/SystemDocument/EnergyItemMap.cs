using EquipManage.Domain.Entity.SystemDocument;
using System.Data.Entity.ModelConfiguration;

namespace EquipManage.Mapping.SystemDocument
{
    public class EnergyItemMap:EntityTypeConfiguration<EnergyItemEntity>
    {
        public EnergyItemMap()
        {
            this.ToTable("Sys_EnergyItem");
            this.HasKey(t => t.FId);
        }
    }
}
