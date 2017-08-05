using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NLog;

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
        public void Debug(string msg,string loggername)
        {
            Logger logger = LogManager.GetLogger(loggername);
            logger.Debug(msg);
        }

        public void Info(string msg, string loggername)
        {
            Logger logger = LogManager.GetLogger(loggername);
            logger.Info(msg);
        }

        public void Trace(string msg, string loggername)
        {
            Logger logger = LogManager.GetLogger(loggername);
            logger.Trace(msg);
        }

        public void Error(string msg, string loggername)
        {
            Logger logger = LogManager.GetLogger(loggername);
            logger.Error(msg);
        }

        public void Fatal(string msg, string loggername)
        {
            Logger logger = LogManager.GetLogger(loggername);
            logger.Fatal(msg);
        }
    }
}