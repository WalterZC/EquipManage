using EquipManage.Domain.Entity.SystemBusiness;
using System.Data.Entity.ModelConfiguration;
namespace EquipManage.Mapping.SystemBusiness
{
    /// <summary>
    /// OperationalPlanMap
    /// </summary>    
    public class OperationalPlanMap : EntityTypeConfiguration<OperationalPlanEntity>
    {
        public OperationalPlanMap()
        {
            this.ToTable("Sys_OperationalPlan");
            this.HasKey(t => t.FId);
        }
    }
}
