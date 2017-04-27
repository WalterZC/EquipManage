/*******************************************************************************
 * Copyright © 2017 EquipManage.Framework 版权所有
 * Author: 王成元
 * Description: 设备管理系统-匠盟科技
 * Date:2017-02-17
*********************************************************************************/
using EquipManage.Application.SystemDocument;
using EquipManage.Code;
using EquipManage.Domain.Entity.SystemDocument;
using EquipManage.Domain.Entity.SystemManage;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace EquipManage.Web.Controllers
{
    [HandlerLogin]
    public class ClientsDataController : Controller
    {
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetClientsDataJson()
        {
            var data = new
            {
                dataItems = this.GetDataItemList(),
                dataItemsFid = this.GetDataItemFidList(),
                organize = this.GetOrganizeList(),
                role = this.GetRoleList(),
                duty = this.GetDutyList(),
                user = this.GetUserList(),
                authorizeMenu = this.GetMenuList(),
                authorizeButton = this.GetMenuButtonList(),
                equipment = this.GetEquipmentList(),
                parts = this.GetPartsList(),
                position = this.GetPositionList()
            };
            return Content(data.ToJson());
        }
        private object GetUserList()
        {
            UserApp userApp = new UserApp();
            var data = userApp.GetList();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (UserEntity item in data)
            {
                var fieldItem = new
                {
                    encode = item.FId,
                    fullname = item.FRealName
                };
                dictionary.Add(item.FId, fieldItem);
            }
            return dictionary;
        }
        private object GetDataItemList()
        {
            var itemdata = new ItemsDetailApp().GetList();
            Dictionary<string, object> dictionaryItem = new Dictionary<string, object>();
            foreach (var item in new ItemsApp().GetList())
            {
                var dataItemList = itemdata.FindAll(t => t.FItemId.Equals(item.FId));
                Dictionary<string, string> dictionaryItemList = new Dictionary<string, string>();
                foreach (var itemList in dataItemList)
                {
                    dictionaryItemList.Add(itemList.FItemCode, itemList.FItemName);
                }
                dictionaryItem.Add(item.FEnCode, dictionaryItemList);
            }
            return dictionaryItem;
        }
        private object GetDataItemFidList()
        {
            Dictionary<string, string> dictionaryItem = new Dictionary<string, string>();
            foreach (var item in new ItemsApp().GetList())
            {
                dictionaryItem.Add(item.FId, item.FFullName);
            }
            foreach (var itemList in new ItemsDetailApp().GetList())
            {
                dictionaryItem.Add(itemList.FId, itemList.FItemName);
            }
            
            return dictionaryItem;
        }
        private object GetOrganizeList()
        {
            OrganizeApp organizeApp = new OrganizeApp();
            var data = organizeApp.GetList();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (OrganizeEntity item in data)
            {
                var fieldItem = new
                {
                    encode = item.FEnCode,
                    fullname = item.FFullName
                };
                dictionary.Add(item.FId, fieldItem);
            }
            return dictionary;
        }
        private object GetPositionList()
        {
            PositionApp positionApp = new PositionApp();
            var data = positionApp.GetList();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (PositionEntity item in data)
            {
                var fieldItem = new
                {
                    encode = item.FId,
                    fullname = item.FShortName,
                    FPrincipalId = item.FPrincipalID
                };
                dictionary.Add(item.FId, fieldItem);
            }
            return dictionary;
        }
        private object GetRoleList()
        {
            RoleApp roleApp = new RoleApp();
            var data = roleApp.GetList();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (RoleEntity item in data)
            {
                var fieldItem = new
                {
                    encode = item.FEnCode,
                    fullname = item.FFullName
                };
                dictionary.Add(item.FId, fieldItem);
            }
            return dictionary;
        }
        private object GetEquipmentList()
        {
            EquipmentApp equipApp = new EquipmentApp();
            var data = equipApp.GetList();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (EquipmentEntity item in data)
            {
                var fieldItem = new
                {
                    FId = item.FId,
                    FShortName = item.FShortName,
                    FNumber = item.FNumber,
                    FModel = item.FModel,
                    FUnitId = item.FUnit,
                };
                dictionary.Add(item.FId, fieldItem);
            }
            return dictionary;
        }
        private object GetPartsList()
        {
            PartsApp partsApp = new PartsApp();
            var data = partsApp.GetList();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (PartsEntity item in data)
            {
                var fieldItem = new
                {
                    FId = item.FId,
                    FFullName = item.FFullName,
                    FNumber = item.FNumber,
                    FModel = item.FModel,
                    FUnit = item.FUnit,
                    FWarehouse = item.FWarehouse
                };
                dictionary.Add(item.FId, fieldItem);
            }
            return dictionary;
        }
        private object GetDutyList()
        {
            DutyApp dutyApp = new DutyApp();
            var data = dutyApp.GetList();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (RoleEntity item in data)
            {
                var fieldItem = new
                {
                    encode = item.FEnCode,
                    fullname = item.FFullName
                };
                dictionary.Add(item.FId, fieldItem);
            }
            return dictionary;
        }
        private object GetMenuList()
        {
            var roleId = OperatorProvider.Provider.GetCurrent().RoleId;
            return ToMenuJson(new RoleAuthorizeApp().GetMenuList(roleId), "0");
        }
        private string ToMenuJson(List<ModuleEntity> data, string parentId)
        {
            StringBuilder sbJson = new StringBuilder();
            sbJson.Append("[");
            List<ModuleEntity> entitys = data.FindAll(t => t.FParentId == parentId);
            if (entitys.Count > 0)
            {
                foreach (var item in entitys)
                {
                    string strJson = item.ToJson();
                    strJson = strJson.Insert(strJson.Length - 1, ",\"ChildNodes\":" + ToMenuJson(data, item.FId) + "");
                    sbJson.Append(strJson + ",");
                }
                sbJson = sbJson.Remove(sbJson.Length - 1, 1);
            }
            sbJson.Append("]");
            return sbJson.ToString();
        }
        private object GetMenuButtonList()
        {
            var roleId = OperatorProvider.Provider.GetCurrent().RoleId;
            var data = new RoleAuthorizeApp().GetButtonList(roleId);
            var dataModuleId = data.Distinct(new ExtList<ModuleButtonEntity>("FModuleId"));
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (ModuleButtonEntity item in dataModuleId)
            {
                var buttonList = data.Where(t => t.FModuleId.Equals(item.FModuleId));
                dictionary.Add(item.FModuleId, buttonList);
            }
            return dictionary;
        }
    }
}
