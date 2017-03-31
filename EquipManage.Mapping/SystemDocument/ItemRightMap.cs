using EquipManage.Domain.Entity.SystemDocument;
using System.Data.Entity.ModelConfiguration;
namespace EquipManage.Mapping.SystemDocument
{
    /// <summary>
    /// ItemRightMap
    /// </summary>    
    public class ItemRightMap : EntityTypeConfiguration<ItemRightEntity>
    {
        public ItemRightMap()
        {
            this.ToTable("Sys_ItemRight");
            this.HasKey(t => t.FId);
        }
    }
}
