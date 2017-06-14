using PRBook2._0.Models.LogicL;
using PRBook2._0.Models.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PRBook2._0.Controllers
{
    public class NodeManageController : Controller
    {
        PublicUtil util = new PublicUtil();
        NodeManage nodeManage = new NodeManage();
        //
        // GET: /NodeManage/
        [Authorize]
        public ActionResult Index()
        {
            if (!util.CheckPower(Request.RawUrl.ToString()))
                return PartialView("WithoutPower");
            return PartialView();
        }
        [Authorize]
        [HttpPost]
        public string GetNodeTreeData()
        {
            return nodeManage.GetNodeTreeData();
        }
        [Authorize]
        [HttpPost]
        public string GetNodeTreeDataBusiness()
        {
            return nodeManage.GetNodeTreeDataBusiness();
        }
        [Authorize]
        [HttpPost]
        public string GetSingleData(string Id)//获取指定树节点
        {
            return nodeManage.GetSingleData(Id);
        }
        [Authorize]
        [HttpPost]
        public string AddNode()
        {
            PRBook2._0.Models.NodeSetInfo nodesetinfo = new Models.NodeSetInfo();
            nodesetinfo.NodeName = Request.Params["NodeName"].ToString();
            if (string.IsNullOrWhiteSpace(Request.Params["NodeNum"]))
                nodesetinfo.NodeNum = null;
            else
                nodesetinfo.NodeNum = int.Parse(Request.Params["NodeNum"].ToString());
            nodesetinfo.NodeType = Request.Params["NodeType"].ToString();
            nodesetinfo.NodeUrl = Request.Params["NodeUrl"].ToString();
            if (string.IsNullOrWhiteSpace(Request.Params["ParentNode"]))
                nodesetinfo.ParentNode = null;
            else
                nodesetinfo.ParentNode = int.Parse(Request.Params["ParentNode"].ToString());
            nodesetinfo.Status = Request.Params["Status"].ToString();
            return nodeManage.AddNode(nodesetinfo);
        }
        [Authorize]
        [HttpPost]
        public string UpdateData()
        {
            PRBook2._0.Models.NodeSetInfo nodesetinfo = new Models.NodeSetInfo();
            nodesetinfo.Id = int.Parse(Request.Params["Id"].ToString());
            nodesetinfo.NodeName = Request.Params["NodeName"].ToString();
            nodesetinfo.NodeNum = int.Parse(Request.Params["NodeNum"].ToString());
            nodesetinfo.NodeType = Request.Params["NodeType"].ToString();
            nodesetinfo.NodeUrl = Request.Params["NodeUrl"].ToString();
            if (string.IsNullOrWhiteSpace(Request.Params["ParentNode"]))
                nodesetinfo.ParentNode = null;
            else
                nodesetinfo.ParentNode = int.Parse(Request.Params["ParentNode"].ToString());
            nodesetinfo.Status = Request.Params["Status"].ToString();
            return nodeManage.UpdateData(nodesetinfo);
        }
        public string DeleteNode()
        {
            if (!string.IsNullOrWhiteSpace(Request.Params["Id"]))
                return nodeManage.DeleteNode(Request.Params["Id"].ToString());
            else
                return "";
        }
	}
}