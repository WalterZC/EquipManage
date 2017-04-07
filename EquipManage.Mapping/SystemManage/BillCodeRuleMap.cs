using EquipManage.Domain.Entity.SystemManage;
using System.Data.Entity.ModelConfiguration;
namespace EquipManage.Mapping.SystemManage
{
    /// <summary>
    /// BillCodeRuleMap
    /// </summary>    
    public class BillCodeRuleMap : EntityTypeConfiguration<BillCodeRuleEntity>
    {
        public BillCodeRuleMap()
        {
            this.ToTable("Sys_BillCodeRule");
            this.HasKey(t => t.FId);
        }
    }
}
