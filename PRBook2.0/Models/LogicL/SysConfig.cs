using PRBook2._0.Models.Tool;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace PRBook2._0.Models.LogicL
{
    public class SysConfig
    {
        PRBookEntities mdb = new PRBookEntities();
        PublicUtil putil = new PublicUtil();
        public SysConfig() { }
        /// <summary>
        /// 获取信息
        /// </summary>
        /// <returns></returns>
        public string GetBindData()
        {
            SYS_SystemConfigInfo systemConfig = mdb.SYS_SystemConfigInfo.ToList().FirstOrDefault();
            return putil.GetJsonData(systemConfig);
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="sysConfig">系统配置信息</param>
        /// <returns>标志,成功 success,不成功为空</returns>
        public string UpdateData(SYS_SystemConfigInfo sysConfig)
        {
            SYS_SystemConfigInfo tmp = mdb.SYS_SystemConfigInfo.ToList().FirstOrDefault();
            if(tmp==null)//添加
            {
                mdb.SYS_SystemConfigInfo.Add(sysConfig);
                
            }
            else//更新
            {
                DbEntityEntry<SYS_SystemConfigInfo> entry = mdb.Entry<SYS_SystemConfigInfo>(sysConfig);
                entry.State = System.Data.Entity.EntityState.Unchanged;
                entry.Property("System_Name").IsModified = true;
                entry.Property("LoginFooter").IsModified = true;
                entry.Property("MainFooter").IsModified = true;
                entry.Property("PhoneQR").IsModified = true;
                entry.Property("PhoneAddress").IsModified = true;
            }
            int ret = mdb.SaveChanges();
            if (ret != 0)
                return "success";
            else
                return "";
        }
    }
}