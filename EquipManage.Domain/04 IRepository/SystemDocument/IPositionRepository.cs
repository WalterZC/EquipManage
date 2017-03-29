using EquipManage.Data;
using EquipManage.Domain.Entity.SystemDocument;
using System.Collections.Generic;

namespace EquipManage.Domain.IRepository.SystemDocument
{
    public interface IPositionRepository:IRepositoryBase<PositionEntity>
    {
        List<PositionEntity> GetItemList(string FParentId);
    }
}
