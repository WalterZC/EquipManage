﻿using System;
namespace EquipManage.Domain.Entity.SystemBusiness
{
    /// <summary>
    /// OperationalPlanPartsEntryEntity
    /// </summary>    
    public class OperationalPlanPartsEntryEntity : IEntity<OperationalPlanPartsEntryEntity>
    {
        public string FId { get; set; }
        public string FItemId { get; set; }
        public int? FEntryId { get; set; }
        public string FPartsId { get; set; }
        public string FUnitId { get; set; }
        public decimal? FQty { get; set; }
        public string FStock { get; set; }
        public string FDescription { get; set; }

    }
}
