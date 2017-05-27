using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        public string LoginConfirm(string username,string pwd)
        {
            return "用户名或密码错误";
        }
	}
}