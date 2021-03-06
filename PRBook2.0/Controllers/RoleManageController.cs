﻿using PRBook2._0.Models;
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
            sysRoleInfo.RoleIndexPage = Request.Params["RoleIndexPage"].ToString();
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
            sysRoleInfo.RoleIndexPage = Request.Params["RoleIndexPage"].ToString();
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
            ViewBag.RoleId = Request["Id"].ToString();
            return PartialView();
        }
        [Authorize]
        [HttpPost]
        public string GetRolePower()
        {
            string roleId = Request.Params["RoleId"].ToString();
            return roleManage.GetRolePower(roleId);
        }
        [Authorize]
        [HttpPost]
        public string UpdateRolePower()
        {
            string roleId = Request.Params["roleId"].ToString();
            List<SYS_RolePower> sysRolePowers = new List<SYS_RolePower>();
            for (int i = 0; i < Request.Form.Count-1; i++)
            {
                string[] nddid = Request.Form[i].ToString().Split('|');
                int parentnode=int.Parse(nddid[1]);
                SYS_RolePower sysRolePower = new SYS_RolePower();
                sysRolePower.RoleId = int.Parse(roleId);
                sysRolePower.NodeId = int.Parse(nddid[0]);
                sysRolePowers.Add(sysRolePower);
                if(!sysRolePowers.Any(u=>u.NodeId==parentnode))
                {
                    SYS_RolePower parentpower = new SYS_RolePower();
                    parentpower.RoleId = int.Parse(roleId);
                    parentpower.NodeId = parentnode;
                    sysRolePowers.Add(parentpower);
                }
            }
            return roleManage.UpdateRolePower(sysRolePowers,roleId);
        }
	}
}