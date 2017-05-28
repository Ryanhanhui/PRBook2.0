using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PRBook2._0.Models.LogicL
{
    public class PRSignIn
    {
        PRBookEntities mdb = new PRBookEntities();
        public PRSignIn()
        {

        }
        public bool LoginConfirm(string username,string pwd)
        {
            PR_UserInfo musers = mdb.PR_UserInfo.Where(u => u.UserId == username && u.Password == pwd).FirstOrDefault();
            if (musers != null)
                return true;
            else
                return false;
        }
    }
}