using EquipManage.Application.SystemDocument;
using EquipManage.Code;
using EquipManage.Domain.Entity.SystemDocument;
using EquipManage.Web.FileHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace EquipManage.Web.Areas.SystemDocument.Controllers
{
    public class EquipmentController : ControllerBase
    {
        private EquipmentApp equipmentApp = new EquipmentApp();

        FilesHelper filesHelper;
        String tempPath = "~/somefiles/";
        String serverMapPath = "~/Files/somefiles/";
        private string StorageRoot
        {
            get { return Path.Combine(HostingEnvironment.MapPath(serverMapPath)); }
        }
        private string UrlBase = "/Files/somefiles/";
        String DeleteURL = "/FileUpload/DeleteFile/?file=";
        String DeleteType = "GET";
        public EquipmentController()
        {
            filesHelper = new FilesHelper(DeleteURL, DeleteType, StorageRoot, UrlBase, tempPath, serverMapPath);
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(string itemId, string keyword)
        {
            var data = equipmentApp.GetList(itemId, keyword);
            return Content(data.ToJson());
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetSelectJson(string enCode)
        {
            var data = equipmentApp.GetItemList(enCode);
            List<object> list = new List<object>();
            foreach (EquipmentEntity item in data)
            {
                list.Add(new { id = item.FNumber, text = item.FFullName });
            }
            return Content(list.ToJson());
        }
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
        public ActionResult Files()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SaveUploadedFile()
        {
            var resultList = new List<ViewDataUploadFilesResult>();

            var CurrentContext = HttpContext;

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
            //bool isSavedSuccessfully = true;
            //string fName = "";
            //try
            //{
            //    foreach (string fileName in Request.Files)
            //    {
            //        HttpPostedFileBase file = Request.Files[fileName];
            //        //Save file content goes here
            //        fName = file.FileName;
            //        if (file != null && file.ContentLength > 0)
            //        {

            //            var originalDirectory = new DirectoryInfo(string.Format("{0}Images\\EquipmentFiles", Server.MapPath(@"\")));

            //            string pathString = System.IO.Path.Combine(originalDirectory.ToString(), "imagepath");

            //            var fileName1 = Path.GetFileName(file.FileName);

            //            bool isExists = System.IO.Directory.Exists(pathString);

            //            if (!isExists)
            //                System.IO.Directory.CreateDirectory(pathString);

            //            var path = string.Format("{0}\\{1}", pathString, file.FileName);
            //            file.SaveAs(path);

            //        }

            //    }

            //}
            //catch (Exception ex)
            //{
            //    isSavedSuccessfully = false;
            //}


            //if (isSavedSuccessfully)
            //{
            //    return Json(new { Message = fName, JsonRequestBehavior.AllowGet });
            //}
            //else
            //{
            //    return Json(new { Message = "Error in saving file", JsonRequestBehavior.AllowGet });
            //}
        }
        public JsonResult GetFileList()
        {
            var list = filesHelper.GetFileList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult DeleteFile(string file)
        {
            filesHelper.DeleteFile(file);
            return Json("OK", JsonRequestBehavior.AllowGet);
        }
    }
}
