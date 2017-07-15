using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PRBook2._0.Models.Tool
{
    public class LogHandle
    {
        private static LogHandle _Instance;
        private static readonly object locker=new object();

        private LogHandle()
        {

        }
        public static LogHandle GetInstance()
        {
            if(_Instance==null)
            {
                lock(locker)
                {
                    if(_Instance==null)
                    {
                        _Instance = new LogHandle();
                    }
                }
            }
            return _Instance;
        }
        public void WriteLog(string logtype,string loglevel,string loginfo)
        {

        }
    }
}