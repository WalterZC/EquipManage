/*******************************************************************************
 * Copyright © 2017 EquipManage.Framework 版权所有
 * Author: 王成元
 * Description: 设备管理系统-匠盟科技
 * Date:2017-02-17
*********************************************************************************/
using System;

namespace EquipManage.Domain.Entity.SystemSecurity
{
    public class LogEntity : IEntity<LogEntity>, ICreationAudited
    {
        public string FId { get; set; }
        public DateTime? FDate { get; set; }
        public string FAccount { get; set; }
        public string FNickName { get; set; }
        public string FType { get; set; }
        public string FIPAddress { get; set; }
        public string FIPAddressName { get; set; }
        public string FModuleId { get; set; }
        public string FModuleName { get; set; }
        public bool? FResult { get; set; }
        public string FDescription { get; set; }
        public DateTime? FCreatorTime { get; set; }
        public string FCreatorUserId { get; set; }
    }
}
