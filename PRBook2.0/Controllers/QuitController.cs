using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PRBook2._0.Controllers
{
    public class QuitController : Controller
    {
        //
        // GET: /Quit/
        public ActionResult SignOut()
        {
            return View();
        }
	}
}