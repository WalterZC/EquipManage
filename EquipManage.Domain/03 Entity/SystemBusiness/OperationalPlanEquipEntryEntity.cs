﻿using System;
namespace EquipManage.Domain.Entity.SystemBusiness
{
    /// <summary>
    /// OperationalPlanEquipEntryEntity
    /// </summary>    
    public class OperationalPlanEquipEntryEntity : IEntity<OperationalPlanEquipEntryEntity>
    {
        public string FId { get; set; }
        public string FItemId { get; set; }
        public int? FEntryId { get; set; }
        public int? FObjectTypeId { get; set; }
        public string FObjectId { get; set; }
        public string FOperationClassId { get; set; }
        public string FOperatorId { get; set; }
        public string FOperationItemId { get; set; }
        public string FDescription { get; set; }
        public string FProjectId { get; set; }

    }
}
