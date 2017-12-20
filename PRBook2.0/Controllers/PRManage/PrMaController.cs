﻿using PRBook2._0.Models;
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
        PRBook2._0.Models.LogicL.PRManage.PrMa prma = new Models.LogicL.PRManage.PrMa();
        //
        // GET: /PrMa/
        [Authorize]
        public ActionResult UserIndex()
        {
            if (!util.CheckPower(Request.RawUrl.ToString()))
                return PartialView("WithoutPower");
            v_pr_peopleinfo prUserinfo = new v_pr_peopleinfo();
            prUserinfo.UserId = UserInfo.GetInstance().UserId;
            ViewBag.PageCount = prma.GetDataCount(prUserinfo);
            return View();
        }
        [Authorize]
        [HttpPost]
        public string GetDataCount()
        {
            v_pr_peopleinfo prUserinfo = new v_pr_peopleinfo();
            prUserinfo.Name = Request.Form["Name"].ToString();
            prUserinfo.UserId = UserInfo.GetInstance().UserId;
            return prma.GetDataCount(prUserinfo).ToString();
        }
        [Authorize]
        [HttpPost]
        public string GetData()
        {
            int currpage = int.Parse(Request.Form["currpage"].ToString());
            int pagesize = int.Parse(Request.Form["pagesize"].ToString());
            v_pr_peopleinfo prUserinfo = new v_pr_peopleinfo();
            prUserinfo.Name = Request.Form["Name"].ToString();
            prUserinfo.UserId = UserInfo.GetInstance().UserId;
            ViewBag.PageCount = prma.GetDataCount(prUserinfo);
            return prma.GetData(currpage, pagesize, prUserinfo);
        }
        [Authorize]
        public ActionResult EditPage()
        {
            if (Request["Id"] != null)
            {
                ViewBag.RStatus = "edit";
                v_pr_peopleinfo pr = prma.GetEditInfo(Request["Id"].ToString());
                ViewBag.Model = pr;
            }
            else
            {
                ViewBag.RStatus = "add";
                v_pr_peopleinfo pr = new v_pr_peopleinfo();
                ViewBag.Model = pr;
            }
            return View();
        }
        [Authorize]
        [HttpPost]
        public string AddData()
        {
            PR_PeopleInfo pr = new PR_PeopleInfo();
            pr.Id = Guid.NewGuid().ToString("N");
            pr.Name = Request.Form["Name"].ToString();
            pr.Remarks = Request.Form["Remarks"].ToString();
            pr.InputDate = DateTime.Now;
            pr.UserId = UserInfo.GetInstance().UserId;
            return prma.AddData(pr);
        }
        [Authorize]
        [HttpPost]
        public string UpdateData()
        {
            PR_PeopleInfo pr = new PR_PeopleInfo();
            pr.Id = Request.Params["Id"].ToString();
            pr.Name = Request.Params["Name"].ToString();
            pr.Remarks = Request.Params["Remarks"].ToString();
            pr.InputDate = DateTime.Now;
            return prma.UpdateData(pr);
        }
        [Authorize]
        [HttpPost]
        public string DeleteData()
        {
            if (!string.IsNullOrWhiteSpace(Request.Params["Id"]))
                return prma.DeleteData(Request.Params["Id"].ToString());
            else
                return "";
        }
	}
}