using PRBook2._0.Models.DataL;
using PRBook2._0.Models.Tool;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace PRBook2._0.Models.LogicL
{
    public class NodeManage
    {
        PublicUtil putil = new PublicUtil();
        public NodeManage()
        {

        }
        /// <summary>
        /// 获取树节点所有数据
        /// </summary>
        /// <returns>节点数据集合</returns>
        public string GetNodeTreeData()
        {
            List<NodeSetInfo> nodemodel = DBTool.GetInstance().mdb.NodeSetInfoes.OrderBy(u => u.NodeNum).ToList();
            return putil.GetJsonData(nodemodel);
        }
        public string GetNodeTreeDataBusiness()
        {
            List<NodeSetInfo> nodemodel = DBTool.GetInstance().mdb.NodeSetInfoes.Where(u => u.NodeType.Equals("0") && u.Status.Equals("1")).ToList();
            return putil.GetJsonData(nodemodel);
        }
        /// <summary>
        /// 根据id获取对应树节点数据
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>节点数据</returns>
        public string GetSingleData(string Id)
        {
            if (string.IsNullOrWhiteSpace(Id))
                return "null";
            int id = int.Parse(Id);
            NodeSetInfo nodemodel = DBTool.GetInstance().mdb.NodeSetInfoes.Where(u => u.Id == id).ToList().FirstOrDefault();
            return putil.GetJsonData(nodemodel);
        }
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="nodesetinfo">树节点</param>
        /// <returns>标志,成功 success,不成功为空</returns>
        public string AddNode(NodeSetInfo nodesetinfo)
        {
            DBTool.GetInstance().mdb.NodeSetInfoes.Add(nodesetinfo);
            int ret = DBTool.GetInstance().SaveChanges(nodesetinfo);
            if (ret != 0)
            {
                return "success";
            }
            else
                return "";
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="nodesetinfo">树节点</param>
        /// <returns>标志,成功 success,不成功为空</returns>
        public string UpdateData(NodeSetInfo nodesetinfo)
        {
            DbEntityEntry<NodeSetInfo> entry = DBTool.GetInstance().mdb.Entry<NodeSetInfo>(nodesetinfo);
            entry.State = System.Data.Entity.EntityState.Unchanged;
            entry.Property("NodeName").IsModified = true;
            entry.Property("NodeUrl").IsModified = true;
            entry.Property("NodeType").IsModified = true;
            entry.Property("NodeNum").IsModified = true;
            entry.Property("Status").IsModified = true;
            int ret = DBTool.GetInstance().SaveChanges(nodesetinfo);
            if (ret != 0)
                return "success";
            else
                return "";
        }
        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="Id">Id</param>
        /// <returns>标志,成功 success,不成功为空</returns>
        public string DeleteNode(string Id)
        {
            int id = int.Parse(Id);
            NodeSetInfo nodesetinfo = DBTool.GetInstance().mdb.NodeSetInfoes.Where(u => u.Id == id).FirstOrDefault();
            DBTool.GetInstance().mdb.NodeSetInfoes.Remove(nodesetinfo);//删除实体
            int ret = DBTool.GetInstance().SaveChanges(nodesetinfo);
            if (ret != 0)
                return "success";
            else
                return "";
        }
    }
}