using PRBook2._0.Models;
using PRBook2._0.Models.LogicL;
using PRBook2._0.Models.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PRBook2._0.Controllers
{
    public class HomeController : Controller
    {
        PublicUtil util = new PublicUtil();
        Home home = new Home();
        //
        // GET: /Home/
        public ActionResult Index()
        {
            if (!util.CheckLoginState())
                return RedirectToAction("Login", "PRSignIn");
            SYS_SystemConfigInfo sysconfig = home.GetSysConfig();
            ViewBag.System_Name = sysconfig.System_Name;
            ViewBag.MainFooter = sysconfig.MainFooter;
            ViewBag.PhoneQR = sysconfig.PhoneQR;
            return View("Main");
        }
        public ActionResult WithoutPower()
        {
            return PartialView();
        }
        public string GetPower()
        {
            return home.GetPower();
        }
	}
}