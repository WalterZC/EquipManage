using EquipManage.Domain.Entity.SystemDocument;
using System.Data.Entity.ModelConfiguration;

namespace EquipManage.Mapping.SystemDocument
{
    public class PartsMap:EntityTypeConfiguration<PartsEntity>
    {
        public PartsMap()
        {
            this.ToTable("Sys_Parts");
            this.HasKey(t => t.FId);
        }
    }
}
