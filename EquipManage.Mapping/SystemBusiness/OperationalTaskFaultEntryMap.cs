
using EquipManage.Domain.Entity.SystemBusiness;
using System.Data.Entity.ModelConfiguration;
namespace EquipManage.Mapping.SystemBusiness
{

    /// <summary>
    /// OperationalTaskFaultEntryMap
    /// </summary>    
    public class OperationalTaskFaultEntryMap : EntityTypeConfiguration<OperationalTaskFaultEntryEntity>
    {
        public OperationalTaskFaultEntryMap()
        {
            this.ToTable("Sys_OperationalTaskFaultEntry");
            this.HasKey(t => t.FId);
        }
    }

}
