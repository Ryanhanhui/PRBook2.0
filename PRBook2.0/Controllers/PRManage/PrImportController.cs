using PRBook2._0.Models.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PRBook2._0.Controllers.PRManage
{
    public class PrImportController : Controller
    {
        PublicUtil util = new PublicUtil();
        PRBook2._0.Models.LogicL.PRManage.PrImport primport = new Models.LogicL.PRManage.PrImport();
        //
        // GET: /PrImport/
        [Authorize]
        public ActionResult ImportPage()
        {
            return View();
        }
        [HttpPost]
        public string ImportData()
        {
            string dataurl = Request.Form["dataurl"].ToString();
            string excelpath = Server.MapPath(Request.ApplicationPath + System.Configuration.ConfigurationManager.AppSettings["UserDown"].ToString() + dataurl);
            string result= primport.ImportData(excelpath);
            System.IO.File.Delete(excelpath);
            return result;
        }
        [Authorize]
        [HttpPost]
        public string GetExcelDataCount()
        {
            return primport.GetExcelDataCount().ToString();
        }
        [Authorize]
        [HttpPost]
        public string GetExcelData()
        {
            int currpage = int.Parse(Request.Form["currpage"].ToString());
            int pagesize = int.Parse(Request.Form["pagesize"].ToString());
            return primport.GetExcelData(currpage,pagesize);
        }
        [Authorize]
        [HttpPost]
        public string GetCheckDataCount()
        {
            return primport.GetCheckDataCount().ToString();
        }
        [Authorize]
        [HttpPost]
        public string GetCheckData()
        {
            int currpage = int.Parse(Request.Form["currpage"].ToString());
            int pagesize = int.Parse(Request.Form["pagesize"].ToString());
            return primport.GetCheckData(currpage, pagesize);
        }
        [Authorize]
        [HttpPost]
        public string ImportConfirmData()
        {
            return primport.ImportConfirmData();
        }
	}
}