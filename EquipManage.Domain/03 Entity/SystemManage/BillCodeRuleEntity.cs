using System;
namespace EquipManage.Domain.Entity.SystemManage
{
    /// <summary>
    /// BillCodeRuleEntity
    /// </summary>    
    public class BillCodeRuleEntity : IEntity<BillCodeRuleEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        public string FId { get; set; }
        public string FTableName { get; set; }
        public int FMaxInterId { get; set; }
        public string FPreLetter { get; set; }
        public string FFormat { get; set; }
        public bool FROB { get; set; }
        public string FDescription { get; set; }
        public DateTime? FCreatorTime { get; set; }
        public string FCreatorUserId { get; set; }
        public DateTime? FLastModifyTime { get; set; }
        public string FLastModifyUserId { get; set; }
        public DateTime? FDeleteTime { get; set; }
        public string FDeleteUserId { get; set; }
        public bool? FDeleteMark { get; set; }
        public int FLength { get; set; }
    }
}

