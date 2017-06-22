using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        public string UploadFile()
        {
            if (Request.Files.Count == 0)
            {
                //Request.Files.Count 文件数为0上传不成功
                return "上传失败";
            }

            var file = Request.Files[0];
            if (file.ContentLength == 0)
            {
                //文件大小大（以字节为单位）为0时，做一些操作
                return "文件大小大（以字节为单位）为0时，做一些操作";
            }
            else
            {
                //文件大小不为0
                file = Request.Files[0];
                //保存成自己的文件全路径,newfile就是你上传后保存的文件,
                //服务器上的UpLoadFile文件夹必须有读写权限　　　　　　
                file.SaveAs(Server.MapPath(@""+sysDown + "SysConfig/"+file.FileName));
                return "上传成功";
            }
        }
	}
}