using PRBook2._0.Models.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PRBook2._0.Models.LogicL
{
    public class NodeManage
    {
        PRBookEntities mdb = new PRBookEntities();
        PublicUtil putil = new PublicUtil();
        public NodeManage()
        {

        }
        public string GetNodeTreeData()
        {
            List<NodeSetInfo> nodemodel = mdb.NodeSetInfoes.ToList();
            return putil.GetJsonData(nodemodel);
        }
    }
}