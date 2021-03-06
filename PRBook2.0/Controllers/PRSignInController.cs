﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PRBook2._0.Models.LogicL;
using System.Web.Security;
using PRBook2._0.Models;
using PRBook2._0.Models.Tool;

namespace PRBook2._0.Controllers
{
    public class PRSignInController : Controller
    {

        PRSignIn signin = new PRSignIn();
        //
        // GET: /PRSignIn/
        public ActionResult Login()
        {
            SYS_SystemConfigInfo sysconfig=signin.GetFooter();
            ViewBag.LoginFooter = sysconfig.LoginFooter;
            ViewBag.System_Name = sysconfig.System_Name;
            LoginImg imgfunc = new LoginImg();
            ViewBag.BgImgs = imgfunc.GetAllData();
            return View();
        }
        [HttpPost]
        public string LoginConfirm(string username,string pwd)
        {
            bool result = signin.LoginConfirm(username, pwd,Request.UserHostAddress);
            if (result)
            {
                return "";
            }
            else
                return "用户名或密码错误";
        }
	}
}