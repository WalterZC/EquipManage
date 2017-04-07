using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipManage.Domain
{
    public interface ICancelAudited
    {
        string FId { get; set; }
        string FCancelUserId { get; set; }
        DateTime? FCancelTime { get; set; }
        bool FCanceledMark { get; set; }
    }
}
