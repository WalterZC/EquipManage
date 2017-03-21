using EquipManage.Data;
using EquipManage.Domain.Entity.SystemDocument;
using System.Collections.Generic;

namespace EquipManage.Domain.IRepository.SystemDocument
{
    public interface IOperationItemRepository:IRepositoryBase<OperationItemEntity>
    {
        List<OperationItemEntity> GetItemList(string FNumber);
    }
}
