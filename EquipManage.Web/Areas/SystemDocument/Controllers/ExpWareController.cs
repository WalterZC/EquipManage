using EquipManage.Application.SystemDocument;
using EquipManage.Code;
using EquipManage.Domain.Entity.SystemDocument;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EquipManage.Web.Areas.SystemDocument.Controllers
{
    public class ExpWareController : ControllerBase
    {
        private ExpWareApp expWareApp = new ExpWareApp();
        private EquipmentTypeApp equipmentTypeApp = new EquipmentTypeApp();
        private EquipmentApp equipmentApp = new EquipmentApp();

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetSelectJson(string FOperationTypeId, string FOperationLevelId, string FEquipTypeId, string itemId, string keyword)
        {
            var data = expWareApp.GetList(FOperationTypeId, FOperationLevelId, FEquipTypeId, itemId, keyword);

            return Content(data.ToJson());
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetTreeJson()
        {
            var equipmentTypeList = equipmentTypeApp.GetList();    //获取所有设备类型
            var equipmentList = equipmentApp.GetList();  //获取所有设备信息

            var treeList = new List<TreeViewModel>();
            foreach (EquipmentTypeEntity item in equipmentTypeList)
            {
                TreeViewModel tree = new TreeViewModel();
                bool hasChildren = (equipmentTypeList.Count(t => t.FParentId == item.FId)+ equipmentList.Count(t => t.FEquipmentTypeId == item.FId)) == 0 ? false : true;
                tree.id = item.FId;
                tree.text = item.FShortName;
                tree.value = item.FNumber;
                tree.parentId = item.FParentId;
                tree.img = "fa fa-folder-open";
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = hasChildren;
                treeList.Add(tree);
            }

            foreach (EquipmentEntity item in equipmentList)
            {
                TreeViewModel tree = new TreeViewModel();
                tree.id = item.FId;
                tree.text = item.FShortName;
                tree.value = item.FNumber;
                tree.parentId = item.FEquipmentTypeId;
                tree.img = "fa fa-file-text-o";
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = false;
                treeList.Add(tree);
            }
            return Content(treeList.TreeViewJson());
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(string FOperationTypeId,string FOperationLevelId, string FEquipTypeId, string itemId, string keyword)
        {
            var data = expWareApp.GetList(FOperationTypeId, FOperationLevelId, FEquipTypeId, itemId, keyword);
            //var data = equipmentApp.GetList(itemId, keyword);
            return Content(data.ToJson());
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = expWareApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(ExpWareEntity expWareEntity, string keyValue)
        {
            expWareApp.SubmitForm(expWareEntity, keyValue);
            return Success("操作成功。");
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            expWareApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }
    }
}
