using EquipManage.Domain.Entity.SystemDocument;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipManage.Mapping.SystemDocument
{
    public class PositionMap:EntityTypeConfiguration<PositionEntity>
    {
        public PositionMap()
        {
            this.ToTable("Sys_Position");
            this.HasKey(t => t.FId);
        }
    }
}
