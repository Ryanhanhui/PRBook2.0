using PRBook2._0.Models.DataL;
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
        public PRSignIn()
        {

        }
        /// <summary>
        /// 获取登录页页脚
        /// </summary>
        /// <returns></returns>
        public SYS_SystemConfigInfo GetFooter()
        {
            SYS_SystemConfigInfo sysconfig = DBTool.GetInstance().mdb.SYS_SystemConfigInfo.ToList().FirstOrDefault();
            return sysconfig;
        }
        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        public bool LoginConfirm(string username,string pwd)
        {
            pwd = EnDecryptTil.SHA1_Encrypt(pwd);
            PR_UserInfo musers = DBTool.GetInstance().mdb.PR_UserInfo.Where(u => u.UserId == username && u.Password == pwd).FirstOrDefault();
            UserInfo userinfo=UserInfo.GetInstance();
            if (musers != null)
            {
                userinfo.SetUserInfo(musers);
                return true;
            }
            else
                return false;
        }
    }
}