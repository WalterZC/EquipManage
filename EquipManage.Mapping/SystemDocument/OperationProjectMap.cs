using EquipManage.Domain.Entity.SystemDocument;
using System.Data.Entity.ModelConfiguration;

namespace EquipManage.Mapping.SystemDocument
{
    public class OperationProjectMap: EntityTypeConfiguration<OperationProjectEntity>
    {
        public OperationProjectMap()
        {
            this.ToTable("Sys_OperationProject");
            this.HasKey(t => t.FId);
        }
    }
}
