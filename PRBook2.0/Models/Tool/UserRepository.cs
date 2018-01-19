using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace PRBook2._0.Models.Tool
{
    public class UserRepository
    {
        private string[] _UserInfo = new string[6];
        public UserRepository()
        {

        }
        public UserRepository(string _UserData)
        {
            _UserInfo = _UserData.Split('|');
        }
        public string UserId { get { return _UserInfo[0]; } set { _UserInfo[0] = value; } }
        public string NickName { get { return _UserInfo[1]; } set { _UserInfo[1] = value; } }
        public string Name { get { return _UserInfo[2]; } set { _UserInfo[2] = value; } }
        public string RoleType { get { return _UserInfo[3]; } set { _UserInfo[3] = value; } }
        public string UserType { get { return _UserInfo[4]; } set { _UserInfo[4] = value; } }
        public string RoleIndexPage { get { return _UserInfo[5]; } set { _UserInfo[5] = value; } }
        public string GetUserString()
        {
            string result = string.Empty;
            StringBuilder strbuilder = new StringBuilder();
            for (int i = 0; i < _UserInfo.Length; i++)
            {
                strbuilder.Append(_UserInfo[i] + @"|");
            }
            result = strbuilder.ToString();
            result = result.Remove(result.LastIndexOf('|'));
            return result;
        }
    }
}