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
    public class SysConfigController : Controller
    {
        SysConfig sysConfig = new SysConfig();
        PublicUtil util = new PublicUtil();
        //
        // GET: /SysConfig/
        [Authorize]
        public ActionResult Index()
        {
            if (!util.CheckPower(Request.RawUrl.ToString()))
                return PartialView("WithoutPower");
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
        [Authorize]
        [HttpPost]
        public string UpdateData()
        {
            SYS_SystemConfigInfo sysconfig = new SYS_SystemConfigInfo();
            if(!string.IsNullOrWhiteSpace(Request.Params["Id"]))
                sysconfig.Id = int.Parse(Request.Params["Id"].ToString());
            sysconfig.System_Name = Request.Params["System_Name"].ToString();
            sysconfig.LoginFooter = Request.Params["LoginFooter"].ToString();
            sysconfig.MainFooter = Request.Params["MainFooter"].ToString();
            sysconfig.PhoneAddress = Request.Params["PhoneAddress"].ToString();
            sysconfig.PhoneQR = Request.Params["PhoneQR"].ToString();
            return sysConfig.UpdateData(sysconfig);
        }
	}
}