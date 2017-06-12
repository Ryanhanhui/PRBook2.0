using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PRBook2._0.Models.Tool
{
    public class PublicUtil
    {
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
            return json;
        }
        /// <summary>
        /// 权限校验
        /// </summary>
        /// <param name="url">请求的地址</param>
        /// <returns>是否具有请求地址的权限</returns>
        public bool CheckPower(string url)
        {
            return true;
        }
    }
}