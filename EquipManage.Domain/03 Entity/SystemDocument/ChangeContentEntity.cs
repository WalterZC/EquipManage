﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipManage.Domain.Entity.SystemDocument
{
    public class ChangeContentEntity : IEntity<ChangeContentEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        public string FId { get; set; }
        public string FNumber { get; set; }
        public string FFullName { get; set; }
        public string FTable { get; set; }
        public string FField { get; set; }
        public string FType { get; set; }
        public int FSortCode { get; set; }
        public bool? FAllowEdit { get; set; }
        public bool? FAllowDelete { get; set; }
        public bool? FDeleteMark { get; set; }
        public bool? FEnabledMark { get; set; }
        public string FDescription { get; set; }
        public DateTime? FCreatorTime { get; set; }
        public string FCreatorUserId { get; set; }
        public DateTime? FLastModifyTime { get; set; }
        public string FLastModifyUserId { get; set; }
        public DateTime? FDeleteTime { get; set; }
        public string FDeleteUserId { get; set; }
        public string FOrganizeId { get; set; }
    }
}
