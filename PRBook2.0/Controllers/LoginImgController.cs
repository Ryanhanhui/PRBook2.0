using PRBook2._0.Models;
using PRBook2._0.Models.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PRBook2._0.Controllers
{
    public class LoginImgController : Controller
    {
        PublicUtil util = new PublicUtil();
        PRBook2._0.Models.LogicL.LoginImg lgimg = new Models.LogicL.LoginImg();
        //
        // GET: /LoginImg/
        [Authorize]
        public ActionResult LoginImgIndex()
        {
            if (!util.CheckPower(Request.RawUrl.ToString()))
                return PartialView("WithoutPower");
            ViewBag.Model = lgimg.GetAllData();
            return View();
        }
        [Authorize]
        [HttpPost]
        public string AddData()
        {
            string imgurl = Request.Form["ImgUrl"].ToString();
            return lgimg.AddData(imgurl);
        }
        [Authorize]
        [HttpPost]
        public string DeleteData()
        {
            if (!string.IsNullOrWhiteSpace(Request.Form["ImgId"]) && !string.IsNullOrWhiteSpace(Request.Form["ImgUrl"]))
            {
                try
                {
                    string sysDown = System.Configuration.ConfigurationManager.AppSettings["SysDown"].ToString();
                    string oldpath = Request.ApplicationPath + sysDown + Request.Form["ImgUrl"].ToString();
                    System.IO.File.Delete(Server.MapPath(oldpath));
                }
                catch (Exception ex)
                {
                    LogHandle.GetInstance().Error(ex.Message, GetType().ToString());
                    return string.Empty;
                }
                return lgimg.DeleteData(Request.Form["ImgId"].ToString());
            }
            return "";
        }
	}
}