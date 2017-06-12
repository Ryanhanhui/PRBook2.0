﻿using PRBook2._0.Models.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PRBook2._0.Models.LogicL
{
    public class RoleManage
    {
        PRBookEntities mdb = new PRBookEntities();
        PublicUtil putil = new PublicUtil();
        public RoleManage()
        {

        }
        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <param name="currpage">当前页</param>
        /// <param name="pagesize">每一页个数</param>
        /// <returns>数据列表json</returns>
        public string GetData(int currpage, int pagesize)
        {
            List<SYS_RoleInfo> sysRoleInfo =
                mdb.SYS_RoleInfo.OrderBy(u => u.Id).Skip((currpage - 1) * pagesize).Take(pagesize).ToList();
            return putil.GetJsonData(sysRoleInfo);
        }
        /// <summary>
        /// 获取信息条数（无条件）
        /// </summary>
        /// <returns></returns>
        public int GetDataCount()
        {
            return mdb.SYS_RoleInfo.OrderBy(u => u.Id).ToList().Count;
        }
        /// <summary>
        /// 获取编辑页的数据
        /// </summary>
        /// <param name="Id">数据ID，为空则为新增状态</param>
        /// <returns>单条记录json</returns>
        public string GetDetail(string Id)
        {
            int id = int.Parse(Id);
            SYS_RoleInfo sysRoleInfo = mdb.SYS_RoleInfo.Where(u => u.Id == id).ToList().SingleOrDefault();
            return putil.GetJsonData(sysRoleInfo);
        }
        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="sysRoleInfo"></param>
        /// <returns></returns>
        public string AddData(SYS_RoleInfo sysRoleInfo)
        {
            if (!IsExists(sysRoleInfo))
                return "exists";
            mdb.SYS_RoleInfo.Add(sysRoleInfo);
            int ret = mdb.SaveChanges();
            if (ret != 0)
                return "success";
            else
                return "";
        }
        private bool IsExists(SYS_RoleInfo sysRoleInfo)
        {
            int ncount = mdb.SYS_RoleInfo.Where(u => u.RoleName.Equals(sysRoleInfo.RoleName)).ToList().Count;

            if (ncount > 0)
                return false;
            else
                return true;
        }
    }
}