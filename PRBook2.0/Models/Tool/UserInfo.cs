using PRBook2._0.Models.LogicL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace PRBook2._0.Models.Tool
{
    /// <summary>
    /// 保存登录用户的用户信息
    /// </summary>
    public class UserInfo
    {
        private static UserInfo _Instance;//全局单例
        private static readonly object locker = new object();

        public string UserId { get { return GetUserInfo() == null ? "" : GetUserInfo().UserId; } set { GetUserInfo().UserId = value; } }
        public string NickName { get { return GetUserInfo().NickName; } set { GetUserInfo().NickName = value; } }
        public string Name { get { return GetUserInfo().Name; } set { GetUserInfo().Name = value; } }
        public string RoleType { get { return GetUserInfo().RoleType; } set { GetUserInfo().RoleType = value; } }
        public string UserType { get { return GetUserInfo().UserType; } set { GetUserInfo().UserType = value; } }
        public string RoleIndexPage { get { return GetUserInfo().RoleIndexPage; } set { GetUserInfo().RoleIndexPage = value; } }
        private UserInfo()
        {

        }
        /// <summary>
        /// 获取用户信息的全局单例对象
        /// </summary>
        /// <returns></returns>
        public static UserInfo GetInstance()
        {
            if (_Instance == null)
            {
                lock (locker)
                {
                    if (_Instance == null)
                        _Instance = new UserInfo();
                }
            }
            return _Instance;
        }
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns>用户信息</returns>
        private UserRepository GetUserInfo()
        {
            try
            {
                string UserData = string.Empty;
                UserData = GetUserFromCookie(HttpContext.Current.Request);
                UserRepository userrepository = new UserRepository(UserData);
                return userrepository;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 从加密的cookie中获取加密的用户信息
        /// </summary>
        /// <param name="request">请求的request</param>
        /// <returns>用户信息串</returns>
        private string GetUserFromCookie(HttpRequest request)
        {
            HttpCookie httpcookie = request.Cookies[FormsAuthentication.FormsCookieName.ToString()];
            FormsAuthenticationTicket authenTiket = FormsAuthentication.Decrypt(httpcookie.Value.ToString());
            return authenTiket.UserData.ToString();
        }
    }
}