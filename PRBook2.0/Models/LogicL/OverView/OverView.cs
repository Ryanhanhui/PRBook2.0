using PRBook2._0.Models.Tool;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace PRBook2._0.Models.LogicL.OverView
{
    public class OverView
    {
        PublicUtil putil = new PublicUtil();
        PRBookEntities mdb = new PRBookEntities();
        public string GetUserIndexStat(v_moneygetgivestat queryobj)
        {
            try
            {
                DbQuery<v_moneygetgivestat> query = mdb.v_moneygetgivestat as DbQuery<v_moneygetgivestat>;
                if (!string.IsNullOrWhiteSpace(queryobj.UserId))
                    query = query.Where(u => u.UserId.Equals(queryobj.UserId)) as DbQuery<v_moneygetgivestat>;
                //if (!string.IsNullOrWhiteSpace(queryobj.MoneyDate))
                //    query = query.Where(u => u.NickName.Contains(userinfo.NickName)) as DbQuery<v_userinfo>;

                query = query.OrderBy(u => u.Id) as DbQuery<v_moneygetgivestat>;

                List<v_moneygetgivestat> pruserinfo = query.ToList();
                return putil.GetJsonData(pruserinfo);
            }
            catch (Exception ex)
            {
                LogHandle.GetInstance().Error(ex.Message, GetType().ToString());
                return string.Empty;
            }
        }
    }
}