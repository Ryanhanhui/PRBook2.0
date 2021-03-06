﻿using PRBook2._0.Models;
using PRBook2._0.Models.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PRBook2._0.Controllers.UserManage
{
    public class UserInfoController : Controller
    {
        PublicUtil util = new PublicUtil();
        PRBook2._0.Models.LogicL.UserManage.UserInfo usi = new Models.LogicL.UserManage.UserInfo();
        //
        // GET: /UserInfo/
        [Authorize]
        public ActionResult Index()
        {
            if (!util.CheckPower(Request.RawUrl.ToString()))
                return PartialView("WithoutPower");
            ViewBag.PageCount = usi.GetDataCount();
            return PartialView();
        }
        [Authorize]
        public ActionResult EditPage()
        {
            if(Request["UserId"]!=null)
            {
                ViewBag.RStatus = "edit";
                v_userinfo pr = usi.GetEditInfo(Request["UserId"].ToString());
                ViewBag.Model = pr;
            }
            else
            {
                ViewBag.RStatus = "add";
                v_userinfo pr = new v_userinfo();
                ViewBag.Model = pr;
            }
            return View();
        }
        [Authorize]
        [HttpPost]
        public string UpdateData()
        {
            PR_UserInfo pr = new PR_UserInfo();
            pr.Id = int.Parse(Request.Params["Id"].ToString());
            pr.UserId = Request.Params["UserId"].ToString();
            pr.NickName = Request.Params["NickName"].ToString();
            pr.Name = Request.Params["Name"].ToString();
            pr.Sex = Request.Params["Sex"].ToString();
            if (string.IsNullOrWhiteSpace(Request.Params["Age"]))
                pr.Age = null;
            else
                pr.Age = int.Parse(Request.Params["Age"]);
            pr.Remark = Request.Params["Remark"].ToString();
            return usi.UpdateData(pr);
        }
        [Authorize]
        public ActionResult PwdPage()
        {
            if (Request["UserId"] != null)
            {
                ViewBag.RStatus = "edit";
                v_userinfo pr = usi.GetEditInfo(Request["UserId"].ToString());
                ViewBag.Model = pr;
            }
            return View();
        }
        [Authorize]
        [HttpPost]
        public string UpdatePwdData()
        {
            string oldpwd = Request.Form["oldPwdField"].ToString();
            string newpwd = Request.Form["newPwdField"].ToString();
            string newpwdconfirm = Request.Form["newPwd1Field"].ToString();
            int pid = int.Parse(Request.Form["Id"].ToString());
            return usi.UpdatePwdData(pid, oldpwd, newpwd, newpwdconfirm);
        }
        [Authorize]
        [HttpPost]
        public string GetData()
        {
            int currpage=int.Parse(Request.Form["currpage"].ToString());
            int pagesize = int.Parse(Request.Form["pagesize"].ToString());
            v_userinfo prUserinfo = new v_userinfo();
            prUserinfo.UserId = Request.Form["UserId"].ToString();
            prUserinfo.Name = Request.Form["Name"].ToString();
            ViewBag.PageCount = usi.GetDataCount(prUserinfo);
            return usi.GetData(currpage, pagesize,prUserinfo);
        }
        [Authorize]
        [HttpPost]
        public string AddData()
        {
            PR_UserInfo pr = new PR_UserInfo();
            pr.UserId = Request.Form["UserId"].ToString();
            pr.NickName = Request.Form["NickName"].ToString();
            pr.Name = Request.Form["Name"].ToString();
            pr.Sex = Request.Form["Sex"].ToString();
            if (string.IsNullOrWhiteSpace(Request.Form["Age"]))
                pr.Age = null;
            else
                pr.Age = int.Parse(Request.Form["Age"]);
            pr.Remark = Request.Form["Remark"].ToString();
            pr.UserType = "0";
            pr.Password = "10470c3b4b1fed12c3baac014be15fac67c6e815";
            return usi.AddData(pr);
        }
        [Authorize]
        [HttpPost]
        public string DeleteData()
        {
            if (!string.IsNullOrWhiteSpace(Request.Params["Id"]))
                return usi.DeleteData(Request.Params["Id"].ToString());
            else
                return "";
        }
        [Authorize]
        [HttpPost]
        public string GetDataCount()
        {
            v_userinfo prUserinfo = new v_userinfo();
            prUserinfo.UserId = Request.Form["UserId"].ToString();
            prUserinfo.Name = Request.Form["Name"].ToString();
            return usi.GetDataCount(prUserinfo).ToString();
        }
        [Authorize]
        public ActionResult RoleSetPage()
        {
            if (Request["UserId"] != null)
            {
                v_userinfo pr = usi.GetEditInfo(Request["UserId"].ToString());
                ViewBag.Model = pr;
            }
            else
            {
                v_userinfo pr = new v_userinfo();
                ViewBag.Model = pr;
            }
            return View();
        }
        [Authorize]
        [HttpPost]
        public string GetRoleList()
        {
            return usi.GetRoleList();
        }
        [Authorize]
        [HttpPost]
        public string SetUserRole()
        {
            int pid = int.Parse(Request.Form["Id"].ToString());
            string roletype = Request.Form["RoleType"].ToString();
            PR_UserInfo pruserinfo = new PR_UserInfo();
            pruserinfo.Id = pid;
            pruserinfo.RoleType = roletype;
            return usi.SetUserRole(pruserinfo);
        }
        [Authorize]
        [HttpPost]
        public string ResetPassword()
        {
            if (!string.IsNullOrWhiteSpace(Request.Params["Id"]))
                return usi.ResetPassword(Request.Params["Id"].ToString());
            else
                return "";
        }
	}
}