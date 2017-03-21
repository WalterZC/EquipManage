using EquipManage.Code;
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
            var expression = ExtLinq.True<ChangeContentEntity>();

            expression = expression.And(t => t.FEnabledMark==true);
            return service.IQueryable(expression).OrderBy(t => t.FSortCode).ToList();
        }
    }
}
