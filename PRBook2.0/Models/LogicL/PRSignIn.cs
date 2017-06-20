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
        public PRSignIn()
        {

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
            PR_UserInfo musers = mdb.PR_UserInfo.Where(u => u.UserId == username && u.Password == pwd).FirstOrDefault();
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