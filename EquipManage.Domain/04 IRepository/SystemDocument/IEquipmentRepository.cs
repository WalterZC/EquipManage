using EquipManage.Data;
using EquipManage.Domain.Entity.SystemDocument;
using System.Collections.Generic;

namespace EquipManage.Domain.IRepository.SystemDocument
{
    public interface IEquipmentRepository : IRepositoryBase<EquipmentEntity>
    {
        List<EquipmentEntity> GetPermissionOrgItemList(string OrgId, string UserId);

        List<EquipmentEntity> GetSelectOrgItemList(string OrgId);


        List<EquipmentEntity> GetSelectTypeItemList(string TypeId);
        List<EquipmentEntity> GetPermissionTypeItemList(string TypeId, string UserId);
    }
}
