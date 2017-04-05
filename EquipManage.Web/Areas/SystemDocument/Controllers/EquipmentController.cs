using EquipManage.Application.SystemDocument;
using EquipManage.Code;
using EquipManage.Domain.Entity.SystemDocument;
using EquipManage.Web.FileHelper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.UI.HtmlControls;

namespace EquipManage.Web.Areas.SystemDocument.Controllers
{
    public class EquipmentController : ControllerBase
    {
        private EquipmentApp equipmentApp = new EquipmentApp();
        private EquipmentPartsApp equipmentPartsApp = new EquipmentPartsApp();

        FilesHelper filesHelper;
        String tempPath = "~/equipment/";
        String serverMapPath = "~/Files/equipment/";
        private string StorageRoot
        {
            get { return Path.Combine(HostingEnvironment.MapPath(serverMapPath)); }
        }
        private string UrlBase = "/Files/equipment/";
        String DeleteURL = "/Equipment/DeleteFile/?file=";
        String DeleteType = "GET";
        String subDir = null;
        public EquipmentController()
        {
            filesHelper = new FilesHelper(DeleteURL, DeleteType, StorageRoot, UrlBase, tempPath, serverMapPath,subDir);
        }

        /// <summary>
        /// 选择部门后，根据权限显示相应的设备列表
        /// </summary>
        /// <param name="itemId">部门Id</param>
        /// <param name="keyword">检索关键字</param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetPermissionGridJson(string FObjectType,string itemId, string keyword)
        {
            var data = equipmentApp.GetPermissionGridList(FObjectType, itemId, keyword);
            return Content(data.ToJson());
        }

        //[HttpGet]
        //[HandlerAjaxOnly]
        //public ActionResult GetSelectJson()
        //{
        //    var data = equipmentApp.GetItemList(OperatorProvider.Provider.GetCurrent().DepartmentId, OperatorProvider.Provider.GetCurrent().UserId);
        //    List<object> list = new List<object>();
        //    foreach (EquipmentEntity item in data)
        //    {
        //        list.Add(new { id = item.FNumber, text = item.FFullName });
        //    }
        //    return Content(list.ToJson());
        //}
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = equipmentApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(EquipmentEntity equipmentEntity, string keyValue)
        {
            equipmentApp.SubmitForm(equipmentEntity, keyValue);
            return Success("操作成功。");
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            equipmentApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }
        
        [HttpGet]
        [HandlerAuthorize]
        public ActionResult PartForm()
        {
            return View();
        }
        [HttpGet]
        [HandlerAuthorize]
        public ActionResult PartList()
        {
            return View();
        }

        #region 设备文档

        [HttpGet]
        [HandlerAuthorize]
        public ActionResult Files()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SaveUploadedFile()
        {
            var resultList = new List<ViewDataUploadFilesResult>();

            var CurrentContext = HttpContext;

            filesHelper.subDir = FilesHelper.FormartQueryString(CurrentContext.Request.UrlReferrer.Query, "keyValue") + "\\";

            filesHelper.UploadAndShowResults(CurrentContext, resultList);
            JsonFiles files = new JsonFiles(resultList);

            bool isEmpty = !resultList.Any();
            if (isEmpty)
            {
                return Json("Error ");
            }
            else
            {
                return Json(files);
            }
        }
        public JsonResult GetFileList()
        {
            var CurrentContext = HttpContext;
            filesHelper.subDir = FilesHelper.FormartQueryString(CurrentContext.Request.UrlReferrer.Query, "keyValue") + "\\";
            var list = filesHelper.GetFileList(HttpContext);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult DeleteFile(string file)
        {
            var CurrentContext = HttpContext;
            filesHelper.subDir = FilesHelper.FormartQueryString(CurrentContext.Request.UrlReferrer.Query, "keyValue") + "\\";
            filesHelper.DeleteFile(CurrentContext, file);
            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        public ActionResult DownFile(string filePath, string fileName)
        {
            filePath = Server.MapPath(filePath);
            FileStream fs = new FileStream(filePath, FileMode.Open);
            byte[] bytes = new byte[(int)fs.Length];
            fs.Read(bytes, 0, bytes.Length);
            fs.Close();
            Response.Charset = "UTF-8";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
            Response.ContentType = "application/octet-stream";


            Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(fileName));
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
            return new EmptyResult();
        }
        #endregion

        #region 设备部位管理
        [HttpPost]
        public ActionResult SubmitPartForm(FormCollection collection)
        {

            EquipmentPartsEntity equipmentPartsEntity = new EquipmentPartsEntity();

            if (!"".Equals(collection["FId"].ToString()))
            {
                equipmentPartsEntity = equipmentPartsApp.GetForm(collection["FId"].ToString());
            }
            equipmentPartsEntity.FItemId = collection["FItemId"].ToString();
            equipmentPartsEntity.FSystemId = collection["FSystemId"].ToString();
            equipmentPartsEntity.FName = collection["FName"].ToString();

            if (!string.IsNullOrEmpty(collection["FImage"]))
            {
                

                var CurrentContext = HttpContext;
                string PartsImagePath = "~/Files/PartsImg/";
                String fullPath = Path.Combine(HostingEnvironment.MapPath(PartsImagePath));
                Directory.CreateDirectory(fullPath);

                if (!string.IsNullOrEmpty(equipmentPartsEntity.FFileName))
                {
                    System.IO.File.Delete(Path.Combine(HostingEnvironment.MapPath(PartsImagePath) + subDir, (equipmentPartsEntity.FFileName.ToString()+".jpg")));
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

                equipmentPartsEntity.FContentLength =Ext.ToString(base64.Length);
                equipmentPartsEntity.FContentType = "image/jpg";
                equipmentPartsEntity.FFileName = filename;
            }
            equipmentPartsApp.SubmitForm(equipmentPartsEntity, collection["FId"].ToString());
            return Success("操作成功。");
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetPartFormJson(string keyValue)
        {
            var data = equipmentPartsApp.GetForm(keyValue);
            return Content(data.ToJson());
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetPartGridJson(string itemId, string keyword = "")
        {
            var data = equipmentPartsApp.GetList(itemId, keyword);
            return Content(data.ToJson());
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePartForm(string keyValue)
        {
            string PartsImagePath = "~/Files/PartsImg/";
            EquipmentPartsEntity equipmentPartsEntity = new EquipmentPartsEntity();
            equipmentPartsEntity = equipmentPartsApp.GetForm(keyValue);
            equipmentPartsApp.DeleteForm(equipmentPartsEntity);
            if (!string.IsNullOrEmpty(equipmentPartsEntity.FFileName))
            {
                System.IO.File.Delete(Path.Combine(HostingEnvironment.MapPath(PartsImagePath) + subDir, (equipmentPartsEntity.FFileName.ToString() + ".jpg")));
            }
            return Success("删除成功。");
        }

        #endregion
    }
}
