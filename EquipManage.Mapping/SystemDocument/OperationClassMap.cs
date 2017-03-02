using EquipManage.Domain.Entity.SystemDocument;
using System.Data.Entity.ModelConfiguration;

namespace EquipManage.Mapping.SystemDocument
{
    public class OperationClassMap:EntityTypeConfiguration<OperationClassEntity>
    {
        public OperationClassMap()
        {
            this.ToTable("Sys_OperationClass");
            this.HasKey(t => t.FId);
        }
    }
}
