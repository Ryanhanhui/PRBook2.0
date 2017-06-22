using PRBook2._0.Models;
using PRBook2._0.Models.LogicL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PRBook2._0.Controllers
{
    public class SysConfigController : Controller
    {
        SysConfig sysConfig = new SysConfig();
        //
        // GET: /SysConfig/
        public ActionResult Index()
        {
            SYS_SystemConfigInfo sysmodel = sysConfig.GetBindData();
            ViewBag.Id = "";
            ViewBag.System_Name = "";
            ViewBag.LoginFooter = "";
            ViewBag.MainFooter = "";
            ViewBag.PhoneAddress = "";
            ViewBag.PhoneQR = "";
            if(sysmodel!=null)
            {
                ViewBag.Id = sysmodel.Id;
                ViewBag.System_Name = sysmodel.System_Name;
                ViewBag.LoginFooter = sysmodel.LoginFooter;
                ViewBag.MainFooter = sysmodel.MainFooter;
                ViewBag.PhoneAddress = sysmodel.PhoneAddress;
                ViewBag.PhoneQR = sysmodel.PhoneQR;
            }
            return PartialView();
        }
	}
}