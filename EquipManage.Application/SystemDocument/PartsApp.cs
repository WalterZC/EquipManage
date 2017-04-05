using EquipManage.Code;
using EquipManage.Domain.Entity.SystemDocument;
using EquipManage.Domain.IRepository.SystemDocument;
using EquipManage.Repository.SystemDocument;
using System.Collections.Generic;
using System.Linq;

namespace EquipManage.Application.SystemDocument
{
    public class PartsApp
    {
        private IPartsRepository service = new PartsRepository();
        private OrganizeApp organizeApp = new OrganizeApp();

        public List<PartsEntity> GetList(string keyword = "")
        {
            var expression = ExtLinq.True<PartsEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.FFullName.Contains(keyword));
                expression = expression.Or(t => t.FNumber.Contains(keyword));
            }
            return service.IQueryable(expression).OrderBy(t => t.FSortCode).ToList();
        }
        public PartsEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public List<PartsEntity> GetPermissionGridList(string OrgId = "", string keyword = "")
        {

            List<PartsEntity> datalist = new List<PartsEntity>();
            List<PartsEntity> rolelist = new List<PartsEntity>();
            List<OrganizeEntity> orgList = new List<OrganizeEntity>();
            orgList = organizeApp.GetPermissionGridList(OrgId);
            rolelist = this.GetList();

            datalist = (from c in rolelist
                        join o in orgList on c.FOrganizeId equals o.FId
                        select c).ToList();

            if (!string.IsNullOrEmpty(keyword))
            {
                datalist = datalist.Where(t => t.FFullName.Contains(keyword) || t.FNumber.Contains(keyword)).ToList();
            }
            return datalist;
        }
        public void DeleteForm(string keyValue)
        {
            service.Delete(t => t.FId == keyValue);
        }
        public void SubmitForm(PartsEntity partsEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                partsEntity.Modify(keyValue);
                service.Update(partsEntity);
            }
            else
            {
                partsEntity.Create();
                service.Insert(partsEntity);
            }
        }
    }
}
