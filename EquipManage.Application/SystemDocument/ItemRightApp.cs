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
    /// ItemRightApp
    /// </summary>    
    public class ItemRightApp
    {
        private IItemRightRepository service = new ItemRightRepository();

        public List<ItemRightEntity> GetList(string userId,string objectType)
        {
            var expression = ExtLinq.True<ItemRightEntity>();
            expression = expression.And(t => t.FUserId.Equals(userId));
            expression = expression.And(t => t.FObjectType.Equals(objectType));
            return service.IQueryable(expression).OrderBy(t => t.FCreatorTime).ToList();
        }

        public void Delete(string userId, string objectType)
        {
            var expression = ExtLinq.True<ItemRightEntity>();
            expression = expression.And(t => t.FUserId.Equals(userId));
            expression = expression.And(t => t.FObjectType.Equals(objectType));
            service.Delete(expression);
        }

        public void SubmitForm(string[] FOrgIds, string FUserId, string FObjectType)
        {
            this.Delete(FUserId, FObjectType);
            List<ItemRightEntity> List = new List<ItemRightEntity>();

            foreach (var itemId in FOrgIds)
            {
                ItemRightEntity ItemEntity = new ItemRightEntity();
                ItemEntity.Create();
                ItemEntity.FAccessType = true;
                ItemEntity.FObjectId = itemId;
                ItemEntity.FObjectType = FObjectType;
                ItemEntity.FUserId = FUserId;

                List.Add(ItemEntity);
            }
            service.Insert(List);

        }

    }
}
