using EquipManage.Domain.Entity.SystemDocument;
using System.Data.Entity.ModelConfiguration;

namespace EquipManage.Mapping.SystemDocument
{
    public class OperationClassMemberMap:EntityTypeConfiguration<OperationClassMemberEntity>
    {
        public OperationClassMemberMap()
        {
            this.ToTable("Sys_OperationClassMember");
            this.HasKey(t => t.FId);
        }
    }
}
