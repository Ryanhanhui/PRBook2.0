using PRBook2._0.Models.DataL;
using PRBook2._0.Models.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PRBook2._0.Models.LogicL
{
    public class Home
    {
        PublicUtil putil = new PublicUtil();
        public Home() { }
        public string GetPower()
        {
            string usertype = UserInfo.GetInstance().UserType;
            string roletype = UserInfo.GetInstance().RoleType;
            List<TBFun_GetUserPower_Result> powerlist = DBTool.GetInstance().mdb.TBFun_GetUserPower(usertype, roletype).OrderBy(u => u.NodeNum).ToList();
            return putil.GetJsonData(powerlist);
        }
        public SYS_SystemConfigInfo GetSysConfig()
        {
            SYS_SystemConfigInfo sysconfig = DBTool.GetInstance().mdb.SYS_SystemConfigInfo.ToList().FirstOrDefault();
            return sysconfig;
        }
    }
}