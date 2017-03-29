using EquipManage.Data;
using EquipManage.Domain.Entity.SystemDocument;
using System.Collections.Generic;

namespace EquipManage.Domain.IRepository.SystemDocument
{
    public interface IOperationClassRepository:IRepositoryBase<OperationClassEntity>
    {
        List<OperationClassEntity> GetItemList(string FParentId);
    }
}
