using EquipManage.Domain.Entity.SystemDocument;
using System.Data.Entity.ModelConfiguration;

namespace EquipManage.Mapping.SystemDocument
{
    public class ChangeTypeMap:EntityTypeConfiguration<ChangeTypeEntity>
    {
        public ChangeTypeMap()
        {
            this.ToTable("Sys_ChangeType");
            this.HasKey(t => t.FId);
        }
    }
}
