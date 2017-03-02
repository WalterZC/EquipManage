/*******************************************************************************
 * Copyright © 2017 EquipManage.Framework 版权所有
 * Author: 王成元
 * Description: 设备管理系统-匠盟科技
 * Date:2017-03-02
*********************************************************************************/
using EquipManage.Domain.Entity.SystemDocument;
using EquipManage.Domain.IRepository.SystemDocument;
using EquipManage.Repository.SystemDocument;
using System.Collections.Generic;
using System.Linq;

namespace EquipManage.Application.SystemDocument
{
    public class OperationClassApp
    {
        private IOperationClassRepository service = new OperationClassRepository();
        public List<OperationClassEntity> GetList()
        {
            return service.IQueryable().OrderBy(t => t.FCreatorTime).ToList();
        }
        public OperationClassEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public void SubmitForm(OperationClassEntity operationClassEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                operationClassEntity.Modify(keyValue);
                service.Update(operationClassEntity);
            }
            else
            {
                operationClassEntity.Create();
                service.Insert(operationClassEntity);
            }
        }
        public void DeleteForm(string keyValue)
        {
                service.Delete(t => t.FId == keyValue);
        }
        public void UpdateForm(OperationClassEntity operationClassEntity)
        {
            service.Update(operationClassEntity);
        }
    }
}
