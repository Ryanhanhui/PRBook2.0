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
    public class RoleManageController : Controller
    {
        PublicUtil util = new PublicUtil();
        RoleManage roleManage = new RoleManage();
        //
        // GET: /RoleManage/
        [Authorize]
        public ActionResult Index()
        {
            if (!util.CheckPower(Request.RawUrl.ToString()))
                return PartialView("WithoutPower");
            ViewBag.PageCount = roleManage.GetDataCount();
            return PartialView();
        }
        [Authorize]
        [HttpPost]
        public string GetData(int currpage, int pagesize)
        {
            return roleManage.GetData(currpage, pagesize);
        }
        [Authorize]
        public ActionResult DetialPage()
        {
            ViewBag.RStatus = Request["status"].ToString();
            if(!Request["status"].ToString().Equals("add"))
            {
                
                ViewBag.RId = Request["id"].ToString();
            }
            return PartialView();
        }
        [Authorize]
        [HttpPost]
        public string GetDetail(string Id)
        {
            return roleManage.GetDetail(Id);
        }
        [Authorize]
        [HttpPost]
        public string AddData()
        {
            SYS_RoleInfo sysRoleInfo = new SYS_RoleInfo();
            sysRoleInfo.RoleName = Request.Params["RoleName"].ToString();
            sysRoleInfo.RoleDesc = Request.Params["RoleDesc"].ToString();
            return roleManage.AddData(sysRoleInfo);
        }
        [Authorize]
        [HttpPost]
        public string UpdateData()
        {
            SYS_RoleInfo sysRoleInfo = new SYS_RoleInfo();
            sysRoleInfo.Id = int.Parse(Request.Params["Id"].ToString());
            sysRoleInfo.RoleName = Request.Params["RoleName"].ToString();
            sysRoleInfo.RoleDesc = Request.Params["RoleDesc"].ToString();
            return roleManage.UpdateData(sysRoleInfo);
        }
        [Authorize]
        [HttpPost]
        public string DeleteData()
        {
            if (!string.IsNullOrWhiteSpace(Request.Params["Id"]))
                return roleManage.DeleteData(Request.Params["Id"].ToString());
            else
                return "";
        }
        [Authorize]
        public ActionResult PowerPage()
        {
            return PartialView();
        }
	}
}