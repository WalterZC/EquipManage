﻿using EquipManage.Code;
using EquipManage.Domain.Entity.SystemDocument;
using EquipManage.Domain.IRepository.SystemDocument;
using EquipManage.Repository.SystemDocument;
using System.Collections.Generic;
using System.Linq;

namespace EquipManage.Application.SystemDocument
{
    public class ExpWareApp
    {
        private IExpWareRepository service = new ExpWareRepository();

        public List<ExpWareEntity> GetList(string FEquipTypeId,string itemId="", string keyword="")
        {
            var expression = ExtLinq.True<ExpWareEntity>();

            expression = expression.And(t => t.FEquipTypeId == FEquipTypeId);

            if (!string.IsNullOrEmpty(itemId))
            {
                expression = expression.Or(t => t.FItemId == itemId);
            }

            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.FShortName.Contains(keyword));
                expression = expression.Or(t => t.FNumber.Contains(keyword));
            }
            return service.IQueryable(expression).OrderBy(t => t.FSortCode).ToList();
        }
        public ExpWareEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public void DeleteForm(string keyValue)
        {
            service.Delete(t => t.FId == keyValue);
        }
        public void SubmitForm(ExpWareEntity expWareEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                expWareEntity.Modify(keyValue);
                service.Update(expWareEntity);
            }
            else
            {
                expWareEntity.Create();
                service.Insert(expWareEntity);
            }
        }
    }
}
