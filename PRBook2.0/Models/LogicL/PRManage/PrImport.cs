using PRBook2._0.Models.Tool;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace PRBook2._0.Models.LogicL.PRManage
{
    public class PrImport
    {
        PublicUtil putil = new PublicUtil();
        PRBookEntities mdb = new PRBookEntities();
        public string ImportData(string dataurl)
        {
            try
            {
                string cuserid = UserInfo.GetInstance().UserId;
                mdb.PR_EnterPeopleMoney.RemoveRange(mdb.PR_EnterPeopleMoney.Where(u => u.UserId.Equals(cuserid)).ToList());//清空导入表

                string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + dataurl + ";" + "Extended Properties=Excel 8.0;";
                OleDbConnection conn = new OleDbConnection(strConn);
                conn.Open();
                string strExcel = "";
                OleDbDataAdapter myCommand = null;
                DataSet ds = null;
                strExcel = "select * from [sheet1$]";
                myCommand = new OleDbDataAdapter(strExcel, strConn);
                ds = new DataSet();
                myCommand.Fill(ds, "table1");
                conn.Close();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    List<PR_EnterPeopleMoney> epmlist = new List<PR_EnterPeopleMoney>();
                    foreach (DataRow item in ds.Tables[0].Rows)
                    {
                        PR_EnterPeopleMoney epm = new PR_EnterPeopleMoney();
                        epm.Id = Guid.NewGuid().ToString("N");
                        epm.UserId = UserInfo.GetInstance().UserId;
                        epm.Name = item["来往人姓名"].ToString();
                        epm.Remarks = item["来往人备注"].ToString();
                        epm.PRType = item["来往类型"].ToString();
                        epm.PRMoney = item["来往金额"].ToString();
                        epm.PRRemarks = item["来往备注"].ToString();
                        epm.PRDate = item["来往时间"].ToString();
                        epmlist.Add(epm);
                    }
                    mdb.PR_EnterPeopleMoney.AddRange(epmlist);
                    int ret = mdb.SaveChanges();
                    if (ret != 0)
                        return "success";
                }
                else
                    return "nodata";
                return "";
            }
            catch (Exception ex)
            {
                LogHandle.GetInstance().Error(ex.Message, GetType().ToString());
                return string.Empty;
            }
        }
        public int GetExcelDataCount()
        {
            string cuserid = UserInfo.GetInstance().UserId;
            return mdb.PR_EnterPeopleMoney.Where(u => u.UserId.Equals(cuserid)).ToList().Count;
        }
        public string GetExcelData(int currpage, int pagesize)
        {
            string cuserid = UserInfo.GetInstance().UserId;
            List<PR_EnterPeopleMoney> epmlist = mdb.PR_EnterPeopleMoney.Where(u => u.UserId.Equals(cuserid)).OrderBy(u => u.Id).Skip((currpage - 1) * pagesize).Take(pagesize).ToList();
            return putil.GetJsonData(epmlist);
        }
        public int GetCheckDataCount()
        {
            string cuserid = UserInfo.GetInstance().UserId;
            return mdb.pr_importpeoplemoneycheck.Where(u => u.UserId.Equals(cuserid)).ToList().Count;
        }
        public string GetCheckData(int currpage, int pagesize)
        {
            string cuserid = UserInfo.GetInstance().UserId;
            List<pr_importpeoplemoneycheck> epmlist = mdb.pr_importpeoplemoneycheck.Where(u => u.UserId.Equals(cuserid)).OrderBy(u => u.Id).Skip((currpage - 1) * pagesize).Take(pagesize).ToList();
            return putil.GetJsonData(epmlist);
        }
        public string ImportConfirmData()
        {
            try
            {
                string cuserid = UserInfo.GetInstance().UserId;
                List<pr_importpeoplemoneycheck> importdata = mdb.pr_importpeoplemoneycheck.Where(u => u.UserId.Equals(cuserid)).OrderBy(u => u.Id).ToList();
                List<PR_MoneyInfo> moneylist = new List<PR_MoneyInfo>();
                foreach (pr_importpeoplemoneycheck item in importdata)
                {
                    if (item.Memo.Equals("可以导入"))
                    {
                        //判断是否存在
                        PR_PeopleInfo ppi = mdb.PR_PeopleInfo.Where(u => u.UserId.Equals(cuserid) && u.Name.Equals(item.Name) && u.Remarks.Equals(item.Remarks)).FirstOrDefault();
                        if (ppi == null)//不存在
                        {
                            ppi = new PR_PeopleInfo();
                            ppi.Id = Guid.NewGuid().ToString("N");
                            ppi.Name = item.Name;
                            ppi.Remarks = item.Remarks;
                            ppi.InputDate = DateTime.Now;
                            ppi.UserId = UserInfo.GetInstance().UserId;
                            mdb.PR_PeopleInfo.Add(ppi);
                            mdb.SaveChanges();
                        }
                        PR_MoneyInfo moneyinfo = new PR_MoneyInfo();
                        moneyinfo.Id = Guid.NewGuid().ToString("N");
                        moneyinfo.PeopleId = ppi.Id;
                        int mmmoney = 0;
                        int.TryParse(item.PRMoney, out mmmoney);
                        moneyinfo.Money = mmmoney;
                        if (item.PRType.Equals("收情"))
                            moneyinfo.MoneyType = 1;
                        else if (item.PRType.Equals("还情"))
                            moneyinfo.MoneyType = 2;
                        else
                            moneyinfo.MoneyType = 0;
                        if (!string.IsNullOrWhiteSpace(item.PRDate))
                            moneyinfo.InputDate = DateTime.Parse(item.PRDate);
                        moneyinfo.Remarks = item.PRRemarks;
                        moneylist.Add(moneyinfo);
                    }
                }
                mdb.PR_MoneyInfo.AddRange(moneylist);
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
                throw;
            }
        }
    }
}