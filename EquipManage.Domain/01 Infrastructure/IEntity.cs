/*******************************************************************************
 * Copyright © 2017 EquipManage.Framework 版权所有
 * Author: 王成元
 * Description: 设备管理系统-匠盟科技
 * Date:2017-02-17
*********************************************************************************/
using EquipManage.Code;
using System;

namespace EquipManage.Domain
{
    public class IEntity<TEntity>
    {
        public void Create()
        {
            var entity = this as ICreationAudited;
            entity.FId = Common.GuId();
            var LoginInfo = OperatorProvider.Provider.GetCurrent();
            if (LoginInfo != null)
            {
                entity.FCreatorUserId = LoginInfo.UserId;
            }
            entity.FCreatorTime = DateTime.Now;
        }
        public void BillHeadCreate()
        {
            var entity = this as ICreationAudited;
            var LoginInfo = OperatorProvider.Provider.GetCurrent();
            if (LoginInfo != null)
            {
                entity.FCreatorUserId = LoginInfo.UserId;
            }
            entity.FCreatorTime = DateTime.Now;
        }
        public void Modify(string keyValue)
        {
            var entity = this as IModificationAudited;
            entity.FId = keyValue;
            var LoginInfo = OperatorProvider.Provider.GetCurrent();
            if (LoginInfo != null)
            {
                entity.FLastModifyUserId = LoginInfo.UserId;
            }
            entity.FLastModifyTime = DateTime.Now;
        }
        public void Remove()
        {
            var entity = this as IDeleteAudited;
            var LoginInfo = OperatorProvider.Provider.GetCurrent();
            if (LoginInfo != null)
            {
                entity.FDeleteUserId = LoginInfo.UserId;
            }
            entity.FDeleteTime = DateTime.Now;
            entity.FDeleteMark = true;
        }
        public void Check()
        {
            var entity = this as ICheckAudited;
            var LoginInfo = OperatorProvider.Provider.GetCurrent();
            if (LoginInfo != null)
            {
                entity.FCheckerId = LoginInfo.UserId;
            }
            entity.FCheckTime = DateTime.Now;
            entity.FCheckMark = true;
        }
        public void UnCheck()
        {
            var entity = this as ICheckAudited;
            var LoginInfo = OperatorProvider.Provider.GetCurrent();
            if (LoginInfo != null)
            {
                entity.FCheckerId = "";
            }
            entity.FCheckTime = null;
            entity.FCheckMark = false;
        }
        public void Cancel()
        {
            var entity = this as ICancelAudited;
            var LoginInfo = OperatorProvider.Provider.GetCurrent();
            if (LoginInfo != null)
            {
                entity.FCancelUserId = LoginInfo.UserId;
            }
            entity.FCancelTime = DateTime.Now;
            entity.FCanceledMark = true;
        }
        public void UnCancel()
        {
            var entity = this as ICancelAudited;
            var LoginInfo = OperatorProvider.Provider.GetCurrent();
            if (LoginInfo != null)
            {
                entity.FCancelUserId = "";
            }
            entity.FCancelTime = null;
            entity.FCanceledMark = false;
        }
    }
}
