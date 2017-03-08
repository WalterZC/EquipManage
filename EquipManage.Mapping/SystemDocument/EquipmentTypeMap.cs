using EquipManage.Domain.Entity.SystemDocument;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipManage.Mapping.SystemDocument
{
    public class EquipmentTypeMap: EntityTypeConfiguration<EquipmentTypeEntity>
    {
        public EquipmentTypeMap()
        {
            this.ToTable("Sys_EquipmentType");
            this.HasKey(t => t.FId);
        }
    }
}
