using EquipManage.Domain.Entity.SystemDocument;
using System.Data.Entity.ModelConfiguration;

namespace EquipManage.Mapping.SystemDocument
{
    public class MaintainMap:EntityTypeConfiguration<MaintainEntity>
    {
        public MaintainMap()
        {
            this.ToTable("Sys_Maintain");
            this.HasKey(t => t.FId);
        }
    }
}
