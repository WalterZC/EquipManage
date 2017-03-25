using EquipManage.Application.SystemDocument;
using EquipManage.Code;
using EquipManage.Domain.Entity.SystemDocument;
using EquipManage.Web.FileHelper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Linq;

namespace EquipManage.Web.Areas.SystemDocument.Controllers
{
    public class OperationItemController : ControllerBase
    {
        private OperationProjectApp operationProjectApp = new OperationProjectApp();
        private OperationItemApp operationItemApp = new OperationItemApp();
        String subDir = null;

        #region 设备部位管理
        [HttpPost]
        public ActionResult SubmitPartForm(FormCollection collection)
        {
            subDir = FilesHelper.FormartQueryString(HttpContext.Request.UrlReferrer.Query, "keyValue") + "\\";
            OperationItemEntity operationItemEntity = new OperationItemEntity();

            if (!"".Equals(collection["FId"].ToString()))
            {
                operationItemEntity = operationItemApp.GetForm(collection["FId"].ToString());
            }
            operationItemEntity.FItemId = collection["FItemId"].ToString();
            operationItemEntity.FSystemId = collection["FSystemId"].ToString();
            operationItemEntity.FShortName = collection["FShortName"].ToString();
            operationItemEntity.FMaxVal = Ext.ToDecimal(collection["FMaxVal"].ToString());
            operationItemEntity.FMinVal = Ext.ToDecimal(collection["FMinVal"].ToString());
            operationItemEntity.FValType = collection["FValType"].ToString();
            operationItemEntity.FContent = collection["FContent"].ToString();
            operationItemEntity.FNumber = collection["FNumber"].ToString();
            operationItemEntity.FCheckItems = collection["FCheckItems"].ToString();

            if (!string.IsNullOrEmpty(collection["FImage"]))
            {


                var CurrentContext = HttpContext;
                string PartsImagePath = "~/Files/PartsImg/";
                String fullPath = Path.Combine(HostingEnvironment.MapPath(PartsImagePath));
                Directory.CreateDirectory(fullPath);

                if (!string.IsNullOrEmpty(operationItemEntity.FFileName))
                {
                    System.IO.File.Delete(Path.Combine(HostingEnvironment.MapPath(PartsImagePath), (operationItemEntity.FFileName.ToString() + ".jpg")));
                }

                string base64 = collection["FImage"].Substring(collection["FImage"].IndexOf(',') + 1);
                base64 = base64.Trim('\0');
                byte[] myData = Convert.FromBase64String(base64);

                string saveFileName = DateTime.Now.ToFileTime().ToString();
                MemoryStream ms = new MemoryStream(myData);
                Bitmap bmp = new Bitmap(ms);
                Image returnImage = bmp;
                returnImage.Save(Server.MapPath(PartsImagePath) + saveFileName + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

                string filename = saveFileName;

                operationItemEntity.FContentLength = Ext.ToString(base64.Length);
                operationItemEntity.FContentType = "image/jpg";
                operationItemEntity.FFileName = filename;
            }
            operationItemApp.SubmitForm(operationItemEntity, collection["FId"].ToString());
            return Success("操作成功。");
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetPartFormJson(string keyValue)
        {
            var data = operationItemApp.GetForm(keyValue);
            return Content(data.ToJson());
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetPartGridJson(string itemId, string keyword = "")
        {
            var data = operationItemApp.GetList(itemId, keyword);
            return Content(data.ToJson());
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePartForm(string keyValue)
        {
            subDir = FilesHelper.FormartQueryString(HttpContext.Request.UrlReferrer.Query, "keyValue") + "\\";

            string PartsImagePath = "~/Files/PartsImg/";
            OperationItemEntity operationItemEntity = new OperationItemEntity();
            operationItemEntity = operationItemApp.GetForm(keyValue);
            operationItemApp.DeleteForm(operationItemEntity.FId);
            if (!string.IsNullOrEmpty(operationItemEntity.FFileName))
            {
                System.IO.File.Delete(Path.Combine(HostingEnvironment.MapPath(PartsImagePath) + subDir, (operationItemEntity.FFileName.ToString() + ".jpg")));
            }
            return Success("删除成功。");
        }

        [HttpGet]
        public ActionResult ProjectItemClone()
        {
            return View();
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetCloneProjectItemTreeJson(string FEquipmentTypeId)
        {
            var projectdata = operationProjectApp.GetList(FEquipmentTypeId);
            var itemdata = operationItemApp.GetList();
            var treeList = new List<TreeViewModel>();
            foreach (OperationProjectEntity item in projectdata)
            {
                TreeViewModel tree = new TreeViewModel();
                bool hasChildren = itemdata.Count(t => t.FItemId == item.FId) == 0 ? false : true;
                tree.id = item.FId;
                tree.text = item.FShortName;
                tree.value = item.FId;
                tree.parentId = "0";
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = true;
                treeList.Add(tree);
            }
            foreach (OperationItemEntity item in itemdata)
            {
                TreeViewModel tree = new TreeViewModel();
                bool hasChildren = false;
                tree.id = item.FId;
                tree.text = item.FShortName;
                tree.value = item.FId;
                tree.parentId = item.FItemId;
                tree.isexpand = true;
                tree.complete = true;
                tree.showcheck = true;
                tree.hasChildren = hasChildren;

                treeList.Add(tree);
            }
            return Content(treeList.TreeViewJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult SubmitCloneProjectItem(string FOperationProjectId, string FIds)
        {
            operationItemApp.SubmitCloneProjectItem(FOperationProjectId, FIds);
            return Success("克隆成功。");
        }
        #endregion
    }
}
