using PRBook2._0.Models.DataL;
using PRBook2._0.Models.Tool;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace PRBook2._0.Models.LogicL.UserManage
{
    public class UserInfo
    {
        public PR_UserInfo GetEditInfo(string userId)
        {
            PR_UserInfo userinfo =DBTool.GetInstance().mdb.PR_UserInfo.Where(u => u.UserId.Equals(userId)).ToList().FirstOrDefault();
            return userinfo;
        }
        public string UpdateData(PR_UserInfo userInfo)
        {
            DbEntityEntry<PR_UserInfo> entry = DBTool.GetInstance().mdb.Entry<PR_UserInfo>(userInfo);
            entry.State = System.Data.Entity.EntityState.Unchanged;
            entry.Property("UserId").IsModified = true;
            entry.Property("Name").IsModified = true;
            entry.Property("NickName").IsModified = true;
            entry.Property("Sex").IsModified = true;
            entry.Property("Age").IsModified = true;
            entry.Property("Remark").IsModified = true;
            int ret = DBTool.GetInstance().SaveChanges(userInfo);
            if (ret != 0)
                return "success";
            else
                return "";
        }
        private bool IsExists(PR_UserInfo userInfo)
        {
            int ncount = DBTool.GetInstance().mdb.PR_UserInfo.Where(u => u.UserId.Equals(userInfo.UserId)).ToList().Count;

            if (ncount > 0)
                return false;
            else
                return true;
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="pid">id</param>
        /// <param name="oldpwd">旧密码</param>
        /// <param name="newpwd">新密码</param>
        /// <param name="newpwdconfirm">确认密码</param>
        /// <returns></returns>
        public string UpdatePwdData(int pid,string oldpwd,string newpwd,string newpwdconfirm)
        {
            PR_UserInfo userinfo = DBTool.GetInstance().mdb.PR_UserInfo.Where(u => u.Id == pid).ToList().FirstOrDefault();
            if (EnDecryptTil.SHA1_Encrypt(oldpwd) != userinfo.Password)
                return "旧密码错误";
            else
            {
                if (newpwd != newpwdconfirm)
                    return "两次密码输入不一致，请修改";
                else
                {
                    newpwd = EnDecryptTil.SHA1_Encrypt(newpwd);
                    userinfo.Password = newpwd;
                    DBTool.GetInstance().SaveChanges(userinfo);
                    LogHandle.GetInstance().WriteLog("更新数据", "info", "用户密码更改");
                    return "success";
                }
            }
        }
    }
}