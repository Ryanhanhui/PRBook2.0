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
                if (!string.IsNullOrWhiteSpace(queryobj.MoneyDate))
                {
                    string datestr = "1900-1";
                    if(!queryobj.MoneyDate.Equals("-1"))
                    {
                        DateTime tmpdt = DateTime.Now.Date.AddMonths(-int.Parse(queryobj.MoneyDate));
                        datestr = tmpdt.Year + "-" + (tmpdt.Month < 10 ? ("0" + tmpdt.Month) : (""+tmpdt.Month));
                        query = query.Where(u => u.MoneyDate.CompareTo(datestr) >= 0) as DbQuery<v_moneygetgivestat>;
                    }
                    
                }
                query = query.OrderBy(u => u.MoneyDate) as DbQuery<v_moneygetgivestat>;

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