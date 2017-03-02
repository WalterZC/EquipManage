using EquipManage.Domain.Entity.SystemDocument;
using System.Data.Entity.ModelConfiguration;

namespace EquipManage.Mapping.SystemDocument
{
    public class OperationItemMap:EntityTypeConfiguration<OperationItemEntity>
    {
        public OperationItemMap()
        {
            this.ToTable("Sys_OperationItem");
            this.HasKey(t => t.FId);
        }
    }
}
