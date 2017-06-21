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
            ViewBag.Model = sysConfig.GetBindData();
            return PartialView();
        }
	}
}