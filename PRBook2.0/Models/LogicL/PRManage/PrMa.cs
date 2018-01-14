using PRBook2._0.Models.Tool;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace PRBook2._0.Models.LogicL.PRManage
{
    public class PrMa
    {
        PublicUtil putil = new PublicUtil();
        PRBookEntities mdb = new PRBookEntities();
        /// <summary>
        /// 返回数据条数
        /// </summary>
        /// <returns></returns>
        public int GetDataCount()
        {
            return mdb.v_pr_peopleinfo.ToList().Count;
        }
        public int GetDataCount(v_pr_peopleinfo userinfo)
        {
            try
            {
                DbQuery<v_pr_peopleinfo> pr_peopleinfoquery = mdb.v_pr_peopleinfo as DbQuery<v_pr_peopleinfo>;
                if (!string.IsNullOrWhiteSpace(userinfo.Name))
                    pr_peopleinfoquery = pr_peopleinfoquery.Where(u => u.Name.Contains(userinfo.Name)) as DbQuery<v_pr_peopleinfo>;
                if (!string.IsNullOrWhiteSpace(userinfo.Remarks))
                    pr_peopleinfoquery = pr_peopleinfoquery.Where(u => u.Remarks.Contains(userinfo.Remarks)) as DbQuery<v_pr_peopleinfo>;
                if (!string.IsNullOrWhiteSpace(userinfo.UserId))
                    pr_peopleinfoquery = pr_peopleinfoquery.Where(u => u.UserId.Equals(userinfo.UserId)) as DbQuery<v_pr_peopleinfo>;
                return pr_peopleinfoquery.ToList().Count;
            }
            catch (Exception ex)
            {
                LogHandle.GetInstance().Error(ex.Message, GetType().ToString());
                return 0;
            }
        }
        public string GetData(int currpage, int pagesize)
        {
            string cuserid = UserInfo.GetInstance().UserId;
            List<v_pr_peopleinfo> pruserinfo =
                mdb.v_pr_peopleinfo.Where(u => u.UserId.Equals(cuserid)).OrderBy(u => u.InputDate).Skip((currpage - 1) * pagesize).Take(pagesize).ToList();
            return putil.GetJsonData(pruserinfo);
        }
        public string GetData(int currpage, int pagesize, v_pr_peopleinfo userinfo)
        {
            try
            {
                DbQuery<v_pr_peopleinfo> pr_peopleinfoquery = mdb.v_pr_peopleinfo as DbQuery<v_pr_peopleinfo>;
                if (!string.IsNullOrWhiteSpace(userinfo.Name))
                    pr_peopleinfoquery = pr_peopleinfoquery.Where(u => u.Name.Contains(userinfo.Name)) as DbQuery<v_pr_peopleinfo>;
                if (!string.IsNullOrWhiteSpace(userinfo.Remarks))
                    pr_peopleinfoquery = pr_peopleinfoquery.Where(u => u.Remarks.Contains(userinfo.Remarks)) as DbQuery<v_pr_peopleinfo>;

                pr_peopleinfoquery = pr_peopleinfoquery.Where(u=>u.UserId.Equals(userinfo.UserId)).OrderBy(u => u.Id).Skip((currpage - 1) * pagesize).Take(pagesize) as DbQuery<v_pr_peopleinfo>;

                List<v_pr_peopleinfo> pruserinfo = pr_peopleinfoquery.ToList();
                return putil.GetJsonData(pruserinfo);
            }
            catch (Exception ex)
            {
                LogHandle.GetInstance().Error(ex.Message, GetType().ToString());
                return string.Empty;
            }
        }
        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public string AddData(PR_PeopleInfo peopleInfo)
        {
            try
            {
                if (!IsExists(peopleInfo))
                    return "exists";
                mdb.PR_PeopleInfo.Add(peopleInfo);
                int ret = mdb.SaveChanges();
                if (ret != 0)
                {
                    return "success";
                }
                else
                    return "";
            }
            catch (Exception ex)
            {
                LogHandle.GetInstance().Error(ex.Message, GetType().ToString());
                return string.Empty;
            }
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="peopleInfo"></param>
        /// <returns></returns>
        public string UpdateData(PR_PeopleInfo peopleInfo)
        {
            try
            {
                DbEntityEntry<PR_PeopleInfo> entry = mdb.Entry<PR_PeopleInfo>(peopleInfo);
                entry.State = System.Data.Entity.EntityState.Unchanged;
                entry.Property("Name").IsModified = true;
                entry.Property("Remarks").IsModified = true;
                entry.Property("InputDate").IsModified = true;
                int ret = mdb.SaveChanges();
                if (ret != 0)
                {
                    return "success";
                }
                else
                    return "";
            }
            catch (Exception ex)
            {
                LogHandle.GetInstance().Error(ex.Message, GetType().ToString());
                return string.Empty;
            }
        }
        //获取编辑数据
        public v_pr_peopleinfo GetEditInfo(string peopleId)
        {
            v_pr_peopleinfo peopleinfo = mdb.v_pr_peopleinfo.Where(u => u.Id.Equals(peopleId)).ToList().FirstOrDefault();
            return peopleinfo;
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="Id">Id</param>
        /// <returns>标志,成功 success,不成功为空</returns>
        public string DeleteData(string Id)
        {
            try
            {
                List<PR_MoneyInfo> moneylist = mdb.PR_MoneyInfo.Where(u => u.PeopleId.Equals(Id)).ToList();
                PR_PeopleInfo peopleInfo = mdb.PR_PeopleInfo.Where(u => u.Id.Equals(Id)).FirstOrDefault();
                mdb.PR_MoneyInfo.RemoveRange(moneylist);
                mdb.PR_PeopleInfo.Remove(peopleInfo);//删除实体
                int ret = mdb.SaveChanges();
                if (ret != 0)
                {
                    return "success";
                }
                else
                    return "";
            }
            catch (Exception ex)
            {
                LogHandle.GetInstance().Error(ex.Message, GetType().ToString());
                return string.Empty;
            }
        }
        public string GetPrMoeny(string PeopleId,string Mtype)
        {
            try
            {
                int moneytype = int.Parse(Mtype);
                List<PR_MoneyInfo> moneyInfoList = mdb.PR_MoneyInfo.Where(u => u.PeopleId.Equals(PeopleId) && u.MoneyType == moneytype).OrderByDescending(u => u.InputDate).ToList();
                return putil.GetJsonData(moneyInfoList);
            }
            catch (Exception ex)
            {
                LogHandle.GetInstance().Error(ex.Message, GetType().ToString());
                return string.Empty;
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Id">Id</param>
        /// <returns>标志,成功 success,不成功为空</returns>
        public string DeleteMoneyInfo(string Id)
        {
            try
            {
                PR_MoneyInfo moneyInfo = mdb.PR_MoneyInfo.Where(u => u.Id.Equals(Id)).FirstOrDefault();
                mdb.PR_MoneyInfo.Remove(moneyInfo);//删除实体
                int ret = mdb.SaveChanges();
                if (ret != 0)
                {
                    return "success";
                }
                else
                    return "";
            }
            catch (Exception ex)
            {
                LogHandle.GetInstance().Error(ex.Message, GetType().ToString());
                return string.Empty;
            }
        }
        /// <summary>
        /// 新增人情来往金额数据
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public string AddMoneyData(PR_MoneyInfo moneyInfo)
        {
            try
            {
                mdb.PR_MoneyInfo.Add(moneyInfo);
                int ret = mdb.SaveChanges();
                if (ret != 0)
                {
                    return "success";
                }
                else
                    return "";
            }
            catch (Exception ex)
            {
                LogHandle.GetInstance().Error(ex.Message, GetType().ToString());
                return string.Empty;
            }
        }
        private bool IsExists(PR_PeopleInfo peopleinfo)
        {
            string cuserid = UserInfo.GetInstance().UserId;
            PR_PeopleInfo ppi = mdb.PR_PeopleInfo.Where(u => u.UserId.Equals(cuserid) && u.Name.Equals(peopleinfo.Name) && u.Remarks.Equals(peopleinfo.Remarks)).FirstOrDefault();

            if (ppi==null)
                return true;
            else
                return false;
        }
    }

}