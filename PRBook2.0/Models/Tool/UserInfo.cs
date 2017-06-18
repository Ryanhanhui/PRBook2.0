using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PRBook2._0.Models.Tool
{
    /// <summary>
    /// 保存登录用户的用户信息
    /// </summary>
    public class UserInfo
    {
        private static UserInfo _Instance;//全局单例

        public string UserId { get; set; }
        public string Password { get; set; }
        public string NickName { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public Nullable<int> Age { get; set; }
        public string RoleType { get; set; }
        public string UserType { get; set; }
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
                _Instance = new UserInfo();
            return _Instance;
        }
        /// <summary>
        /// 设置登录用户的信息
        /// </summary>
        /// <param name="pr_userinfo">用户信息</param>
        public void SetUserInfo(PR_UserInfo pr_userinfo)
        {
            _Instance.UserId = pr_userinfo.UserId;
            _Instance.NickName = pr_userinfo.NickName;
            _Instance.Name = pr_userinfo.Name;
            _Instance.Age = pr_userinfo.Age;
            _Instance.Sex = pr_userinfo.Sex;
            _Instance.RoleType = pr_userinfo.RoleType;
            _Instance.Password = pr_userinfo.Password;
            _Instance.UserType = pr_userinfo.UserType;
        }
    }
}