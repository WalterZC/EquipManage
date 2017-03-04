using EquipManage.Data;
using EquipManage.Domain.Entity.SystemDocument;
using System.Collections.Generic;

namespace EquipManage.Domain.IRepository.SystemDocument
{
    public interface IOperationClassMemberRepository:IRepositoryBase<OperationClassMemberEntity>
    {
        List<OperationClassMemberEntity> GetItemList(string FNumber);
    }
}
