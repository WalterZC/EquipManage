using EquipManage.Data;
using EquipManage.Domain.Entity.SystemDocument;
using System.Collections.Generic;

namespace EquipManage.Domain.IRepository.SystemDocument
{
    public interface IEquipmentRepository : IRepositoryBase<EquipmentEntity>
    {
        List<EquipmentEntity> GetEquipmentList(string FTypeID);
    }
}
