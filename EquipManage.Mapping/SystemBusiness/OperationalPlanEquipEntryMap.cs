using EquipManage.Domain.Entity.SystemBusiness;
using System.Data.Entity.ModelConfiguration;
namespace EquipManage.Mapping.SystemBusiness
{    
    /// <summary>
    /// OperationalPlanEquipEntryMap
    /// </summary>    
    public class OperationalPlanEquipEntryMap:EntityTypeConfiguration<OperationalPlanEquipEntryEntity>
    {
       public OperationalPlanEquipEntryMap()
       {
          this.ToTable("Sys_OperationalPlanEquipEntry");
          this.HasKey(t=>t.FId);
       }
    }
		}
