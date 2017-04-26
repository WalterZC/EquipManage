using EquipManage.Domain.Entity.SystemBusiness;
using System.Data.Entity.ModelConfiguration;
namespace EquipManage.Mapping.SystemBusiness
{

    /// <summary>
    /// OperationalTaskMap
    /// </summary>    
    public class OperationalTaskMap : EntityTypeConfiguration<OperationalTaskEntity>
    {
        public OperationalTaskMap()
        {
            this.ToTable("Sys_OperationalTask");
            this.HasKey(t => t.FId);
        }
    }

}
