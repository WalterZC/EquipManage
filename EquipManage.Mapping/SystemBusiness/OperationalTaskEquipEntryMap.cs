
using EquipManage.Domain.Entity.SystemBusiness;
using System.Data.Entity.ModelConfiguration;
namespace EquipManage.Mapping.SystemBusiness
{

    /// <summary>
    /// OperationalTaskEquipEntryMap
    /// </summary>    
    public class OperationalTaskEquipEntryMap : EntityTypeConfiguration<OperationalTaskEquipEntryEntity>
    {
        public OperationalTaskEquipEntryMap()
        {
            this.ToTable("Sys_OperationalTaskEquipEntry");
            this.HasKey(t => t.FId);
        }
    }

}
