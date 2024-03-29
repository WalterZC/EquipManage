﻿using EquipManage.Domain.Entity.SystemDocument;
using System.Data.Entity.ModelConfiguration;

namespace EquipManage.Mapping.SystemDocument
{
    public class ChangeContentMap: EntityTypeConfiguration<ChangeContentEntity>
    {
        public ChangeContentMap()
        {
            this.ToTable("Sys_ChangeContent");
            this.HasKey(t => t.FId);
        }
    }
}
