using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace PRBook2._0.Controllers
{
    public class FileUploadController : Controller
    {
        string sysDown = System.Configuration.ConfigurationManager.AppSettings["SysDown"].ToString();
        string userDown = System.Configuration.ConfigurationManager.AppSettings["UserDown"].ToString();
        //
        // GET: /FileUpload/
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 单文件上传
        /// </summary>
        /// <returns></returns>
        public string UploadSingleFile()
        {
            string filetype = string.Empty, folder = string.Empty, oldpath = string.Empty;
            string newfilepath = string.Empty, newfile = string.Empty;
            if (Request["type"] != null)
                filetype = Request["type"].ToString();
            if (Request["folder"] != null)
                folder = Request["folder"].ToString();
            if (Request["oldpath"] != null)
                oldpath = Request["oldpath"].ToString();

            if (filetype.Equals("1"))
            {
                oldpath = Request.ApplicationPath + sysDown + oldpath;
                newfilepath = Request.ApplicationPath + sysDown + folder + "/";
            }
            else if (filetype.Equals("0"))
            {
                oldpath = Request.ApplicationPath + userDown + oldpath;
                newfilepath = Request.ApplicationPath + userDown + folder + "/";
            }
            try
            {
                if(!string.IsNullOrWhiteSpace(Request["oldpath"]))
                    System.IO.File.Delete(Server.MapPath(oldpath));
            }
            catch
            { }
            if (!Directory.Exists(Server.MapPath(newfilepath)))
            {
                Directory.CreateDirectory(Server.MapPath(newfilepath));
            }
            if (Request.Files.Count == 0)
            {
                //Request.Files.Count 文件数为0上传不成功
                return "Error";
            }

            var file = Request.Files[0];
            if (file.ContentLength == 0)
            {
                //文件大小大（以字节为单位）为0时，做一些操作
                return "Error0";
            }
            else
            {
                //文件大小不为0
                file = Request.Files[0];
                //保存成自己的文件全路径,newfile就是你上传后保存的文件,
                //服务器上的UpLoadFile文件夹必须有读写权限
                string fileextension = System.IO.Path.GetExtension(file.FileName);
                string filename = Guid.NewGuid().ToString("N") + fileextension;
                newfile = folder + "/" + filename;
                string newfilename = newfilepath + filename;
                file.SaveAs(Server.MapPath(newfilename));
                return newfile;
            }
        }
    }
}