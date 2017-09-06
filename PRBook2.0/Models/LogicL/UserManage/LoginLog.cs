using PRBook2._0.Models.Tool;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace PRBook2._0.Models.LogicL.UserManage
{
    public class LoginLog
    {
        PublicUtil putil = new PublicUtil();
        PRBookEntities mdb = new PRBookEntities();
        /// <summary>
        /// 获取数据条数
        /// </summary>
        /// <returns></returns>
        public int GetDataCount()
        {
            return mdb.V_SYS_LoginLog.OrderBy(u => u.LoginTime).ToList().Count;
        }
        public int GetDataCount(V_SYS_LoginLog loginlog, string begintime = null, string endtime = null)
        {
            try
            {
                DbQuery<V_SYS_LoginLog> loginlogquery = mdb.V_SYS_LoginLog as DbQuery<V_SYS_LoginLog>;
                if (!string.IsNullOrWhiteSpace(loginlog.UserId))
                    loginlogquery = loginlogquery.Where(u => u.UserId.Contains(loginlog.UserId)) as DbQuery<V_SYS_LoginLog>;
                if (!string.IsNullOrWhiteSpace(loginlog.NickName))
                    loginlogquery = loginlogquery.Where(u => u.NickName.Contains(loginlog.NickName)) as DbQuery<V_SYS_LoginLog>;
                if (!string.IsNullOrWhiteSpace(loginlog.LoginIP))
                    loginlogquery = loginlogquery.Where(u => u.LoginIP.Contains(loginlog.LoginIP)) as DbQuery<V_SYS_LoginLog>;
                if (!string.IsNullOrWhiteSpace(loginlog.LoginAddress))
                    loginlogquery = loginlogquery.Where(u => u.LoginAddress.Equals(loginlog.LoginAddress)) as DbQuery<V_SYS_LoginLog>;
                if (!string.IsNullOrWhiteSpace(begintime))
                {
                    DateTime _begintime = DateTime.Parse(begintime);
                    loginlogquery = loginlogquery.Where(u => u.LoginTime >= _begintime) as DbQuery<V_SYS_LoginLog>;
                }
                if (!string.IsNullOrWhiteSpace(endtime))
                {
                    DateTime _endtime = DateTime.Parse(endtime);
                    loginlogquery = loginlogquery.Where(u => u.LoginTime <= _endtime) as DbQuery<V_SYS_LoginLog>;
                }

                return loginlogquery.ToList().Count;
            }
            catch (Exception ex)
            {
                LogHandle.GetInstance().Error(ex.Message, GetType().ToString());
                return 0;
            }
        }
        public string GetData(int currpage, int pagesize, V_SYS_LoginLog loginlog, string begintime = null, string endtime = null)
        {
            try
            {
                DbQuery<V_SYS_LoginLog> loginlogquery = mdb.V_SYS_LoginLog as DbQuery<V_SYS_LoginLog>;
                if (!string.IsNullOrWhiteSpace(loginlog.UserId))
                    loginlogquery = loginlogquery.Where(u => u.UserId.Contains(loginlog.UserId)) as DbQuery<V_SYS_LoginLog>;
                if (!string.IsNullOrWhiteSpace(loginlog.NickName))
                    loginlogquery = loginlogquery.Where(u => u.NickName.Contains(loginlog.NickName)) as DbQuery<V_SYS_LoginLog>;
                if (!string.IsNullOrWhiteSpace(loginlog.LoginIP))
                    loginlogquery = loginlogquery.Where(u => u.LoginIP.Contains(loginlog.LoginIP)) as DbQuery<V_SYS_LoginLog>;
                if (!string.IsNullOrWhiteSpace(loginlog.LoginAddress))
                    loginlogquery = loginlogquery.Where(u => u.LoginAddress.Equals(loginlog.LoginAddress)) as DbQuery<V_SYS_LoginLog>;
                if (!string.IsNullOrWhiteSpace(begintime))
                {
                    DateTime _begintime=DateTime.Parse(begintime);
                    loginlogquery = loginlogquery.Where(u => u.LoginTime >= _begintime) as DbQuery<V_SYS_LoginLog>;
                }
                if (!string.IsNullOrWhiteSpace(endtime))
                {
                    DateTime _endtime=DateTime.Parse(endtime);
                    loginlogquery = loginlogquery.Where(u => u.LoginTime <= _endtime) as DbQuery<V_SYS_LoginLog>;
                }

                loginlogquery = loginlogquery.OrderByDescending(u => u.LoginTime).Skip((currpage - 1) * pagesize).Take(pagesize) as DbQuery<V_SYS_LoginLog>;

                List<V_SYS_LoginLog> loginloginfo = loginlogquery.ToList();
                return putil.GetJsonData(loginloginfo);
            }
            catch (Exception ex)
            {
                LogHandle.GetInstance().Error(ex.Message, GetType().ToString());
                return string.Empty;
            }
        }
    }
}