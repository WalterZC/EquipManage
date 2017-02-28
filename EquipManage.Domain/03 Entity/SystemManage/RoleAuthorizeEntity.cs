/*******************************************************************************
 * Copyright © 2017 EquipManage.Framework 版权所有
 * Author: 王成元
 * Description: 设备管理系统-匠盟科技
 * Date:2017-02-17
*********************************************************************************/
using System;

namespace EquipManage.Domain.Entity.SystemManage
{
    public class RoleAuthorizeEntity : IEntity<RoleAuthorizeEntity>, ICreationAudited
    {
        public string FId { get; set; }
        public int? FItemType { get; set; }
        public string FItemId { get; set; }
        public int? FObjectType { get; set; }
        public string FObjectId { get; set; }
        public int? FSortCode { get; set; }
        public DateTime? FCreatorTime { get; set; }
        public string FCreatorUserId { get; set; }
    }
}
