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
    }
}