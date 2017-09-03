using PRBook2._0.Models.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PRBook2._0.Models.LogicL
{
    public class PRSignIn
    {
        PRBookEntities mdb = new PRBookEntities();
        PublicUtil putil = new PublicUtil();
        public PRSignIn()
        {

        }
        /// <summary>
        /// 获取登录页页脚
        /// </summary>
        /// <returns></returns>
        public SYS_SystemConfigInfo GetFooter()
        {
            SYS_SystemConfigInfo sysconfig = mdb.SYS_SystemConfigInfo.ToList().FirstOrDefault();
            return sysconfig;
        }
        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        public bool LoginConfirm(string username, string pwd,string ip)
        {
            pwd = EnDecryptTil.SHA1_Encrypt(pwd);
            PR_UserInfo musers = mdb.PR_UserInfo.Where(u => u.UserId == username && u.Password == pwd).FirstOrDefault();
            UserInfo userinfo = UserInfo.GetInstance();
            if (musers != null)
            {
                userinfo.SetUserInfo(musers);
                try
                {
                    string lipaddress = putil.GetIpAddress(ip);
                    SYS_LoginLog loginlog = new SYS_LoginLog();
                    loginlog.LogId = Guid.NewGuid().ToString("N");
                    loginlog.UserId = userinfo.UserId;
                    loginlog.LoginIP = ip;
                    loginlog.LoginAddress = lipaddress;
                    loginlog.LoginTime = DateTime.Now;
                    mdb.SYS_LoginLog.Add(loginlog);
                    mdb.SaveChanges();
                }
                catch (Exception ex)
                {
                    Tool.LogHandle.GetInstance().Error(ex.Message, GetType().ToString());
                }
                return true;
            }
            else
                return false;
        }
    }
}