/*******************************************************************************
 * Copyright © 2017 EquipManage.Framework 版权所有
 * Author: 王成元
 * Description: 设备管理系统-匠盟科技
 * Date:2017-02-17
*********************************************************************************/
using System;

namespace EquipManage.Domain
{
    public interface ICreationAudited
    {
        string FId { get; set; }
        string FCreatorUserId { get; set; }
        DateTime? FCreatorTime { get; set; }
    }
}