using PRBook2._0.Models.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

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
            if (musers != null)
            {
                try
                {
                    string mguid=Guid.NewGuid().ToString("N");
                    FormsAuthentication.SetAuthCookie(musers.UserId + mguid, false);//创建身份验证票
                    UserRepository userdata = new UserRepository();
                    userdata.UserId = musers.UserId;
                    userdata.NickName = musers.NickName;
                    userdata.Name = musers.Name;
                    userdata.RoleType = musers.RoleType;
                    userdata.UserType = musers.UserType;
                    RoleManage roleMa = new RoleManage();
                    userdata.RoleIndexPage = roleMa.GetDetailObj(musers.RoleType).RoleIndexPage;
                    FormsAuthenticationTicket authenTicket = new FormsAuthenticationTicket(1, musers.UserId + mguid, DateTime.Now, DateTime.Now.AddHours(2), false, userdata.GetUserString());
                    //加密身份验证票中的用户信息
                    string encryptTiket = FormsAuthentication.Encrypt(authenTicket);
                    //保存用户信息
                    HttpCookie httpcookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptTiket);
                    HttpContext.Current.Response.Cookies.Add(httpcookie);

                    //写入登录日志
                    string lipaddress = putil.GetIpAddress(ip);
                    SYS_LoginLog loginlog = new SYS_LoginLog();
                    loginlog.LogId = Guid.NewGuid().ToString("N");
                    loginlog.UserId = musers.UserId;
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