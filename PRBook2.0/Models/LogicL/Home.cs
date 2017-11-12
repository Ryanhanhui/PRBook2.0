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
        PRBookEntities mdb = new PRBookEntities();
        public Home() { }
        public string GetPower()
        {
            string usertype = UserInfo.GetInstance().UserType;
            string roletype = UserInfo.GetInstance().RoleType;
            List<v_getuserpower> powerlist = mdb.v_getuserpower.Where(u => u.RoleId.Equals(roletype) && u.NodeType.Equals("0")).ToList();
            if(usertype.Equals("1"))
            {
                List<v_getuserpower> tmplist = mdb.v_getuserpower.Where(u => u.NodeType.Equals("1")).ToList();
                if (powerlist == null)
                    powerlist = tmplist;
                else
                    powerlist.AddRange(tmplist);
            }
            powerlist = powerlist.OrderBy(u => u.NodeNum).ToList();
            return putil.GetJsonData(powerlist);
        }
        public SYS_SystemConfigInfo GetSysConfig()
        {
            SYS_SystemConfigInfo sysconfig = mdb.SYS_SystemConfigInfo.ToList().FirstOrDefault();
            return sysconfig;
        }
    }
}