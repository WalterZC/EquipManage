using EquipManage.Code;
using EquipManage.Domain.Entity.SystemDocument;
using EquipManage.Domain.IRepository.SystemDocument;
using EquipManage.Repository.SystemDocument;
using System.Collections.Generic;
using System.Linq;

namespace EquipManage.Application.SystemDocument
{
    public class EnergyItemApp
    {
        private IEnergyItemRepository service = new EnergyItemRepository();
        private OrganizeApp organizeApp = new OrganizeApp();

        public List<EnergyItemEntity> GetList(string keyword = "")
        {
            var expression = ExtLinq.True<EnergyItemEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.FFullName.Contains(keyword));
                expression = expression.Or(t => t.FNumber.Contains(keyword));
            }
            return service.IQueryable(expression).OrderBy(t => t.FSortCode).ToList();
        }
        public EnergyItemEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }

        public List<EnergyItemEntity> GetPermissionGridList(string OrgId = "", string keyword = "")
        {

            List<EnergyItemEntity> datalist = new List<EnergyItemEntity>();
            List<EnergyItemEntity> energylist = new List<EnergyItemEntity>();
            List<OrganizeEntity> orgList = new List<OrganizeEntity>();
            orgList = organizeApp.GetPermissionGridList(OrgId);
            energylist = this.GetList();

            datalist = (from c in energylist
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
        public void SubmitForm(EnergyItemEntity energyItemEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                energyItemEntity.Modify(keyValue);
                service.Update(energyItemEntity);
            }
            else
            {
                energyItemEntity.Create();
                service.Insert(energyItemEntity);
            }
        }
    }
}
