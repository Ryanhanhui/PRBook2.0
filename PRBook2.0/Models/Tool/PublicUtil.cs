using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace PRBook2._0.Models.Tool
{
    public class PublicUtil
    {
        PRBookEntities mdb = new PRBookEntities();
        public PublicUtil()
        {

        }
        /// <summary>
        /// 检查登录状态
        /// </summary>
        /// <returns>是否存在</returns>
        public bool CheckLoginState()
        {
            if (string.IsNullOrEmpty(UserInfo.GetInstance().UserId))
                return false;
            else
                return true;
        }
        /// <summary>
        /// json序列化
        /// </summary>
        /// <param name="obj">将要序列化的对象</param>
        /// <returns>序列化后的json字符串</returns>
        public string GetJsonData(Object obj)
        {
            System.Web.Script.Serialization.JavaScriptSerializer jsS = new System.Web.Script.Serialization.JavaScriptSerializer();
            string json = "";
            json = jsS.Serialize(obj);
            json = Regex.Replace(json, @"\\/Date\((\d+)\)\\/", match =>
            {
                DateTime dt = new DateTime(1970, 1, 1);
                dt = dt.AddMilliseconds(long.Parse(match.Groups[1].Value));
                dt = dt.ToLocalTime();
                return dt.ToString("yyyy-MM-dd HH:mm:ss.fff");
            });
            return json;
        }
        /// <summary>
        /// 权限校验
        /// </summary>
        /// <param name="url">请求的地址</param>
        /// <returns>是否具有请求地址的权限</returns>
        public bool CheckPower(string url)
        {
            string usertype = UserInfo.GetInstance().UserType;
            string roletype = UserInfo.GetInstance().RoleType;
            List<TBFun_GetUserPower_Result> powerlist = mdb.TBFun_GetUserPower(usertype, roletype).OrderBy(u => u.NodeNum).ToList();
            foreach (TBFun_GetUserPower_Result item in powerlist)
            {
                if (url.Contains(item.NodeUrl) && !string.IsNullOrWhiteSpace(url) && !string.IsNullOrWhiteSpace(item.NodeUrl))
                    return true;
            }
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
                //httpRquest.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
                System.Net.WebResponse response = httpRquest.GetResponse();
                System.IO.Stream responseStream = response.GetResponseStream();
                System.IO.StreamReader reader = new System.IO.StreamReader(responseStream, System.Text.Encoding.GetEncoding("GB2312"));
                backMsg = reader.ReadToEnd().Trim();
                reader.Close();
                reader.Dispose();
                responseStream.Close();
                responseStream.Dispose();
            }
            catch (Exception ex)
            {
                Tool.LogHandle.GetInstance().Error(ex.Message, GetType().ToString());
            }
            return backMsg;
        }
    }
}