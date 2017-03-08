﻿using EquipManage.Code;
using EquipManage.Domain.Entity.SystemDocument;
using EquipManage.Domain.IRepository.SystemDocument;
using EquipManage.Repository.SystemDocument;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EquipManage.Application.SystemDocument
{
    public class MaintainApp
    {
        private IMaintainRepository service = new MaintainRepository();

        public List<MaintainEntity> GetList()
        {
            return service.IQueryable().OrderBy(t => t.FCreatorTime).ToList();
        }
        public MaintainEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public void DeleteForm(string keyValue)
        {
            if (service.IQueryable().Count(t => t.FParentId.Equals(keyValue)) > 0)
            {
                throw new Exception("删除失败！操作的对象包含了下级数据。");
            }
            else
            {
                service.Delete(t => t.FId == keyValue);
            }
        }
        public void SubmitForm(MaintainEntity maintainEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                maintainEntity.Modify(keyValue);
                service.Update(maintainEntity);
            }
            else
            {
                maintainEntity.Create();
                service.Insert(maintainEntity);
            }
        }
    }
}
