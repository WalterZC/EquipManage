using EquipManage.Domain.Entity.SystemBusiness;
using System.Data.Entity.ModelConfiguration;
namespace EquipManage.Mapping.SystemBusiness
{
    /// <summary>
    /// OperationalPlanPartsEntryMap
    /// </summary>    
    public class OperationalPlanPartsEntryMap : EntityTypeConfiguration<OperationalPlanPartsEntryEntity>
    {
        public OperationalPlanPartsEntryMap()
        {
            this.ToTable("Sys_OperationalPlanPartsEntry");
            this.HasKey(t => t.FId);
        }
    }
}
