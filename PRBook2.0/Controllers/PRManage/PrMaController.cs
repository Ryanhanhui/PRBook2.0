using PRBook2._0.Models.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PRBook2._0.Controllers.PRManage
{
    public class PrMaController : Controller
    {
        PublicUtil util = new PublicUtil();
        //
        // GET: /PrMa/
        [Authorize]
        public ActionResult UserIndex()
        {
            if (!util.CheckPower(Request.RawUrl.ToString()))
                return PartialView("WithoutPower");
            //ViewBag.PageCount = usi.GetDataCount();
            return View();
        }
	}
}