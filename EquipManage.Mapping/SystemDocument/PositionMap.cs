using EquipManage.Domain.Entity.SystemDocument;
using System.Data.Entity.ModelConfiguration;

namespace EquipManage.Mapping.SystemDocument
{
    public class PositionMap:EntityTypeConfiguration<PositionEntity>
    {
        public PositionMap()
        {
            this.ToTable("Sys_Position");
            this.HasKey(t => t.FId);
        }
    }
}
