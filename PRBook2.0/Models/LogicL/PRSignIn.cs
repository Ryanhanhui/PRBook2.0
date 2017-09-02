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
        public bool LoginConfirm(string username, string pwd)
        {
            pwd = EnDecryptTil.SHA1_Encrypt(pwd);
            PR_UserInfo musers = mdb.PR_UserInfo.Where(u => u.UserId == username && u.Password == pwd).FirstOrDefault();
            UserInfo userinfo = UserInfo.GetInstance();
            if (musers != null)
            {
                userinfo.SetUserInfo(musers);
                return true;
            }
            else
                return false;
        }
        public string GetIpAddress(string ip)
        {
            string backMsg = "";
            try
            {
                System.Net.WebRequest httpRquest = System.Net.HttpWebRequest.Create(@"http://whois.pconline.com.cn/ip.jsp?ip=" + ip);
                httpRquest.Method = "GET";
                //这行代码很关键，不设置ContentType将导致后台参数获取不到值  
                httpRquest.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
                System.Net.WebResponse response = httpRquest.GetResponse();
                System.IO.Stream responseStream = response.GetResponseStream();
                System.IO.StreamReader reader = new System.IO.StreamReader(responseStream, System.Text.Encoding.UTF8);
                backMsg = reader.ReadToEnd();
                reader.Close();
                reader.Dispose();
                responseStream.Close();
                responseStream.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return backMsg;
        }
    }
}