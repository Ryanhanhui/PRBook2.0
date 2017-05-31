using PRBook2._0.Models.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PRBook2._0.Controllers
{
    public class NodeManageController : Controller
    {
        PublicUtil util = new PublicUtil();
        //
        // GET: /NodeManage/
        public ActionResult Index()
        {
            if (!util.CheckLoginState())
                return RedirectToAction("Login", "PRSignIn");
            return PartialView();
        }
	}
}