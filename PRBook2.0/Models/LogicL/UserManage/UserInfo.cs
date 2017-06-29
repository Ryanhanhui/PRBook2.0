using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace PRBook2._0.Models.LogicL.UserManage
{
    public class UserInfo
    {
        PRBookEntities mdb = new PRBookEntities();
        public PR_UserInfo GetEditInfo(string userId)
        {
            PR_UserInfo userinfo = mdb.PR_UserInfo.Where(u => u.UserId.Equals(userId)).ToList().FirstOrDefault();
            return userinfo;
        }
        public string UpdateData(PR_UserInfo userInfo)
        {
            DbEntityEntry<PR_UserInfo> entry = mdb.Entry<PR_UserInfo>(userInfo);
            entry.State = System.Data.Entity.EntityState.Unchanged;
            entry.Property("UserId").IsModified = true;
            entry.Property("Name").IsModified = true;
            entry.Property("NickName").IsModified = true;
            entry.Property("Sex").IsModified = true;
            entry.Property("Age").IsModified = true;
            entry.Property("Remark").IsModified = true;
            int ret = mdb.SaveChanges();
            if (ret != 0)
                return "success";
            else
                return "";
        }
        private bool IsExists(PR_UserInfo userInfo)
        {
            int ncount = mdb.PR_UserInfo.Where(u => u.UserId.Equals(userInfo.UserId)).ToList().Count;

            if (ncount > 0)
                return false;
            else
                return true;
        }
    }
}