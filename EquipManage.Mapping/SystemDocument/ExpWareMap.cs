using EquipManage.Domain.Entity.SystemDocument;
using System.Data.Entity.ModelConfiguration;

namespace EquipManage.Mapping.SystemDocument
{
    public class ExpWareMap:EntityTypeConfiguration<ExpWareEntity>
    {
        public ExpWareMap()
        {
            this.ToTable("Sys_ExpWare");
            this.HasKey(t => t.FId);
        }
    }
}
