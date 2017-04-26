﻿
using System;
namespace EquipManage.Domain.Entity.SystemBusiness
{

    /// <summary>
    /// OperationalTaskPartsEntryEntity
    /// </summary>    
    public class OperationalTaskPartsEntryEntity : IEntity<OperationalTaskPartsEntryEntity>, ICreationAudited
    {

        public string FId { get; set; }

        public string FItemId { get; set; }

        public int? FEntryId { get; set; }

        public string FPartsId { get; set; }

        public string FUnitId { get; set; }

        public decimal? FQty { get; set; }

        public string FStock { get; set; }

        public string FDescription { get; set; }

        public DateTime? FCreatorTime { get; set; }

        public string FCreatorUserId { get; set; }

    }

}
