/*******************************************************************************
 * Copyright © 2017 EquipManage.Framework 版权所有
 * Author: 王成元
 * Description: 设备管理系统-匠盟科技
 * Date:2017-02-17
*********************************************************************************/
using System;

namespace EquipManage.Domain.Entity.SystemDocument
{
    public class UserLogOnEntity
    {
        public string FId { get; set; }
        public string FUserId { get; set; }
        public string FUserPassword { get; set; }
        public string FUserSecretkey { get; set; }
        public DateTime? FAllowStartTime { get; set; }
        public DateTime? FAllowEndTime { get; set; }
        public DateTime? FLockStartDate { get; set; }
        public DateTime? FLockEndDate { get; set; }
        public DateTime? FFirstVisitTime { get; set; }
        public DateTime? FPreviousVisitTime { get; set; }
        public DateTime? FLastVisitTime { get; set; }
        public DateTime? FChangePasswordDate { get; set; }
        public bool? FMultiUserLogin { get; set; }
        public int? FLogOnCount { get; set; }
        public bool? FUserOnLine { get; set; }
        public string FQuestion { get; set; }
        public string FAnswerQuestion { get; set; }
        public bool? FCheckIPAddress { get; set; }
        public string FLanguage { get; set; }
        public string FTheme { get; set; }
    }
}
