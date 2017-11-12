using PRBook2._0.Models;
using PRBook2._0.Models.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PRBook2._0.Controllers.UserManage
{
    public class LoginLogController : Controller
    {
        PublicUtil util = new PublicUtil();
        PRBook2._0.Models.LogicL.UserManage.LoginLog loginlg = new Models.LogicL.UserManage.LoginLog();
        //
        // GET: /LoginLog/
        [Authorize]
        public ActionResult LoginLogIndex()
        {
            if (!util.CheckPower(Request.RawUrl.ToString()))
                return PartialView("WithoutPower");
            ViewBag.PageCount = loginlg.GetDataCount();
            return View();
        }
        public string GetData()
        {
            int currpage = int.Parse(Request.Form["currpage"].ToString());
            int pagesize = int.Parse(Request.Form["pagesize"].ToString());
            string begintime = Request.Form["BeginTime"].ToString();
            string endtime = Request.Form["EndTime"].ToString();
            v_sys_loginlog sysLoginLog = new v_sys_loginlog();
            sysLoginLog.UserId = Request.Form["UserId"].ToString();
            sysLoginLog.NickName = Request.Form["NickName"].ToString();
            ViewBag.PageCount = loginlg.GetDataCount(sysLoginLog,begintime,endtime);
            return loginlg.GetData(currpage, pagesize, sysLoginLog,begintime,endtime);
        }
        [Authorize]
        [HttpPost]
        public string GetDataCount()
        {
            v_sys_loginlog sysLoginLog = new v_sys_loginlog();
            sysLoginLog.UserId = Request.Form["UserId"].ToString();
            sysLoginLog.NickName = Request.Form["NickName"].ToString();
            string begintime = Request.Form["BeginTime"].ToString();
            string endtime = Request.Form["EndTime"].ToString();
            return loginlg.GetDataCount(sysLoginLog,begintime,endtime).ToString();
        }
	}
}