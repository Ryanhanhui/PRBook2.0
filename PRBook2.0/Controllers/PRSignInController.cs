using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PRBook2._0.Models.LogicL;
using System.Web.Security;

namespace PRBook2._0.Controllers
{
    public class PRSignInController : Controller
    {
        //
        // GET: /PRSignIn/
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public string LoginConfirm(string username,string pwd)
        {
            PRSignIn signin = new PRSignIn();
            bool result = signin.LoginConfirm(username, pwd);
            if (result)
            {
                FormsAuthentication.SetAuthCookie(username, false);
                return "";
            }
            else
                return "用户名或密码错误";
        }
	}
}