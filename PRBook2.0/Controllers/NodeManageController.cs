using PRBook2._0.Models.LogicL;
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
        NodeManage nodeManage = new NodeManage();
        //
        // GET: /NodeManage/
        [Authorize]
        public ActionResult Index()
        {
            if (!util.CheckLoginState())
                return RedirectToAction("Login", "PRSignIn");
            return PartialView();
        }
        [Authorize]
        [HttpPost]
        public string GetNodeTreeData()
        {
            return nodeManage.GetNodeTreeData();
        }
	}
}