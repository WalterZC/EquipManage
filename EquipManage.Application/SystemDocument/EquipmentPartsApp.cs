using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EquipManage.Domain.Entity.SystemDocument;
using EquipManage.Domain.IRepository.SystemDocument;
using EquipManage.Repository.SystemDocument;
using EquipManage.Code;

namespace EquipManage.Application.SystemDocument
{    
    /// <summary>
    /// EquipmentPartsApp
    /// </summary>    
    public class EquipmentPartsApp
    {
        private IEquipmentPartsRepository service=new EquipmentPartsRepository();

        public List<EquipmentPartsEntity> GetList(string itemId = "", string keyword = "")
        {
            var expression = ExtLinq.True<EquipmentPartsEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.FName.Contains(keyword));
            }
            expression = expression.And(t => t.FItemId == itemId);
            return service.IQueryable(expression).OrderBy(t => t.FCreatorTime).ToList();
        }

        public EquipmentPartsEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }

        public void DeleteForm(EquipmentPartsEntity entity)
        {
            service.Delete(entity);
        }

        public void SubmitForm(EquipmentPartsEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                service.Update(entity);
            }
            else
            {
                entity.Create();
                service.Insert(entity);
            }
        }

    }
	}
