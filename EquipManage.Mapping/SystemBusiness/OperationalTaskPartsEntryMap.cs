
using EquipManage.Domain.Entity.SystemBusiness;
using System.Data.Entity.ModelConfiguration;
namespace EquipManage.Mapping.SystemBusiness
{

    /// <summary>
    /// OperationalTaskPartsEntryMap
    /// </summary>    
    public class OperationalTaskPartsEntryMap : EntityTypeConfiguration<OperationalTaskPartsEntryEntity>
    {
        public OperationalTaskPartsEntryMap()
        {
            this.ToTable("Sys_OperationalTaskPartsEntry");
            this.HasKey(t => t.FId);
        }
    }

}
