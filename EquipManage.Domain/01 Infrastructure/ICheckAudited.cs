using System;

namespace EquipManage.Domain
{
    public interface ICheckAudited
    {
        string FId { get; set; }
        string FCheckerId { get; set; }
        DateTime? FCheckTime { get; set; }
        bool? FCheckMark { get; set; }
    }
}
