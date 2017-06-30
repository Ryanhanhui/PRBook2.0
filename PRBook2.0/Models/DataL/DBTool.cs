using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PRBook2._0.Models.DataL
{
    /// <summary>
    /// ef数据库工具
    /// </summary>
    public class DBTool
    {
        
        private static DBTool _Instance;
        private static readonly object locker = new object();
        public PRBookEntities mdb { set; get; }

        private DBTool()
        {
            mdb = new PRBookEntities();
        }
        /// <summary>
        /// 获取数据工具单例对象
        /// </summary>
        /// <returns>全局单例对象</returns>
        public static DBTool GetInstance()
        {
            if (_Instance == null)
            {
                lock (locker)
                {
                    if (_Instance == null)
                    {
                        _Instance = new DBTool();
                    }
                }
            }
            return _Instance;
        }
    }
}