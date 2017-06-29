using PRBook2._0.Models;
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
            return PartialView();
        }
        [Authorize]
        public ActionResult EditPage()
        {
            if(Request["UserId"]!=null)
            {
                ViewBag.RStatus = "edit";
                PR_UserInfo pr = usi.GetEditInfo(Request["UserId"].ToString());
                ViewBag.Model = pr;
            }
            return View();
        }
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
	}
}