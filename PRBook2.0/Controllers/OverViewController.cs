using PRBook2._0.Models;
using PRBook2._0.Models.LogicL.OverView;
using PRBook2._0.Models.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PRBook2._0.Controllers
{
    public class OverViewController : Controller
    {
        PublicUtil util = new PublicUtil();
        OverView ov = new OverView();
        //
        // GET: /OverView/
        [Authorize]
        public ActionResult NormalIndex()
        {
            if (!util.CheckLoginState())
                return RedirectToAction("Login", "PRSignIn");
            return View();
        }
        [Authorize]
        public ActionResult UserIndex()
        {
            if (!util.CheckLoginState())
                return RedirectToAction("Login", "PRSignIn");
            return View();
        }
        [HttpPost]
        [Authorize]
        public string GetUserIndexStat()
        {
            v_moneygetgivestat querymodel = new v_moneygetgivestat();
            querymodel.UserId = UserInfo.GetInstance().UserId;
            if(!string.IsNullOrWhiteSpace(Request.Params["MoneyDate"]))
                querymodel.MoneyDate=Request["MoneyDate"].ToString();
            return ov.GetUserIndexStat(querymodel);
        }
	}
}