using EquipManage.Domain.Entity.SystemDocument;
using EquipManage.Domain.IRepository.SystemDocument;
using EquipManage.Repository.SystemDocument;
using System.Collections.Generic;
using System.Linq;

namespace EquipManage.Application.SystemDocument
{
    public class ChangeContentApp
    {
        private IChangeContentRepository service = new ChangeContentRepository();

        public List<ChangeContentEntity> GetList()
        {
            return service.IQueryable().OrderBy(t => t.FSortCode).ToList();
        }
    }
}
