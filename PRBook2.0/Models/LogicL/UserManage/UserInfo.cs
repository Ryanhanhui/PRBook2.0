using PRBook2._0.Models.Tool;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace PRBook2._0.Models.LogicL.UserManage
{
    public class UserInfo
    {
        PublicUtil putil = new PublicUtil();
        PRBookEntities mdb = new PRBookEntities();
        public v_userinfo GetEditInfo(string userId)
        {
            v_userinfo userinfo = mdb.v_userinfo.Where(u => u.UserId.Equals(userId)).ToList().FirstOrDefault();
            return userinfo;
        }
        public string UpdateData(PR_UserInfo userInfo)
        {
            try
            {
                DbEntityEntry<PR_UserInfo> entry = mdb.Entry<PR_UserInfo>(userInfo);
                entry.State = System.Data.Entity.EntityState.Unchanged;
                entry.Property("UserId").IsModified = true;
                entry.Property("Name").IsModified = true;
                entry.Property("NickName").IsModified = true;
                entry.Property("Sex").IsModified = true;
                entry.Property("Age").IsModified = true;
                entry.Property("Remark").IsModified = true;
                int ret = mdb.SaveChanges();
                if (ret != 0)
                {
                    LogHandle.GetInstance().Info("【更新数据】" + PRBook2._0.Models.Tool.UserInfo.GetInstance().UserId + " 更新用户信息", GetType().ToString());
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
        /// 新增数据
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public string AddData(PR_UserInfo userInfo)
        {
            try
            {
                if (!IsExists(userInfo))
                    return "exists";
                userInfo.Password = EnDecryptTil.SHA1_Encrypt(userInfo.Password);
                mdb.PR_UserInfo.Add(userInfo);
                int ret = mdb.SaveChanges();
                if (ret != 0)
                {
                    LogHandle.GetInstance().Info("【添加数据】" + PRBook2._0.Models.Tool.UserInfo.GetInstance().UserId + " 添加用户信息", GetType().ToString());
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
        private bool IsExists(PR_UserInfo userInfo)
        {
            int ncount = mdb.PR_UserInfo.Where(u => u.UserId.Equals(userInfo.UserId)).ToList().Count;

            if (ncount > 0)
                return false;
            else
                return true;
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="pid">id</param>
        /// <param name="oldpwd">旧密码</param>
        /// <param name="newpwd">新密码</param>
        /// <param name="newpwdconfirm">确认密码</param>
        /// <returns></returns>
        public string UpdatePwdData(int pid,string oldpwd,string newpwd,string newpwdconfirm)
        {
            try
            {
                PR_UserInfo userinfo = mdb.PR_UserInfo.Where(u => u.Id == pid).ToList().FirstOrDefault();
                if (EnDecryptTil.SHA1_Encrypt(oldpwd) != userinfo.Password)
                    return "旧密码错误";
                else
                {
                    if (newpwd != newpwdconfirm)
                        return "两次密码输入不一致，请修改";
                    else
                    {
                        newpwd = EnDecryptTil.SHA1_Encrypt(newpwd);
                        userinfo.Password = newpwd;
                        mdb.SaveChanges();
                        LogHandle.GetInstance().Info("【更新数据】用户密码更改", GetType().ToString());
                        return "success";
                    }
                }
            }
            catch (Exception ex)
            {
                LogHandle.GetInstance().Error(ex.Message, GetType().ToString());
                return string.Empty;
            }
        }
        /// <summary>
        /// 获取数据条数
        /// </summary>
        /// <returns></returns>
        public int GetDataCount()
        {
            return mdb.v_userinfo.OrderBy(u => u.Id).ToList().Count;
        }
        public int GetDataCount(v_userinfo userinfo)
        {
            try
            {
                DbQuery<v_userinfo> userinfoquery = mdb.v_userinfo as DbQuery<v_userinfo>;
                if (!string.IsNullOrWhiteSpace(userinfo.UserId))
                    userinfoquery = userinfoquery.Where(u => u.UserId.Contains(userinfo.UserId)) as DbQuery<v_userinfo>;
                if (!string.IsNullOrWhiteSpace(userinfo.NickName))
                    userinfoquery = userinfoquery.Where(u => u.NickName.Contains(userinfo.NickName)) as DbQuery<v_userinfo>;
                if (!string.IsNullOrWhiteSpace(userinfo.Name))
                    userinfoquery = userinfoquery.Where(u => u.Name.Contains(userinfo.Name)) as DbQuery<v_userinfo>;
                if (!string.IsNullOrWhiteSpace(userinfo.Sex))
                    userinfoquery = userinfoquery.Where(u => u.Sex.Equals(userinfo.Sex)) as DbQuery<v_userinfo>;
                if (userinfo.Age != null)
                    userinfoquery = userinfoquery.Where(u => u.Age == userinfo.Age) as DbQuery<v_userinfo>;

                return userinfoquery.ToList().Count;
            }
            catch (Exception ex)
            {
                LogHandle.GetInstance().Error(ex.Message, GetType().ToString());
                return 0;
            }
        }
        public string GetData(int currpage, int pagesize)
        {
            List<v_userinfo> pruserinfo =
                mdb.v_userinfo.OrderBy(u => u.Id).Skip((currpage - 1) * pagesize).Take(pagesize).ToList();
            return putil.GetJsonData(pruserinfo);
        }
        public string GetData(int currpage, int pagesize, v_userinfo userinfo)
        {
            try
            {
                DbQuery<v_userinfo> userinfoquery = mdb.v_userinfo as DbQuery<v_userinfo>;
                if (!string.IsNullOrWhiteSpace(userinfo.UserId))
                    userinfoquery = userinfoquery.Where(u => u.UserId.Contains(userinfo.UserId)) as DbQuery<v_userinfo>;
                if (!string.IsNullOrWhiteSpace(userinfo.NickName))
                    userinfoquery = userinfoquery.Where(u => u.NickName.Contains(userinfo.NickName)) as DbQuery<v_userinfo>;
                if (!string.IsNullOrWhiteSpace(userinfo.Name))
                    userinfoquery = userinfoquery.Where(u => u.Name.Contains(userinfo.Name)) as DbQuery<v_userinfo>;
                if (!string.IsNullOrWhiteSpace(userinfo.Sex))
                    userinfoquery = userinfoquery.Where(u => u.Sex.Equals(userinfo.Sex)) as DbQuery<v_userinfo>;
                if (userinfo.Age != null)
                    userinfoquery = userinfoquery.Where(u => u.Age == userinfo.Age) as DbQuery<v_userinfo>;

                userinfoquery = userinfoquery.OrderBy(u => u.Id).Skip((currpage - 1) * pagesize).Take(pagesize) as DbQuery<v_userinfo>;

                List<v_userinfo> pruserinfo = userinfoquery.ToList();
                return putil.GetJsonData(pruserinfo);
            }
            catch (Exception ex)
            {
                LogHandle.GetInstance().Error(ex.Message, GetType().ToString());
                return string.Empty;
            }
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
                int id = int.Parse(Id);
                PR_UserInfo userInfo = mdb.PR_UserInfo.Where(u => u.Id == id).FirstOrDefault();
                mdb.PR_UserInfo.Remove(userInfo);//删除实体
                int ret = mdb.SaveChanges();
                if (ret != 0)
                {
                    LogHandle.GetInstance().Info("【删除数据】" + PRBook2._0.Models.Tool.UserInfo.GetInstance().UserId + " 删除用户", GetType().ToString());
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
        /// 获取角色列表信息
        /// </summary>
        /// <returns>角色列表</returns>
        public string GetRoleList()
        {
            List<SYS_RoleInfo> sysroleinfos = mdb.SYS_RoleInfo.OrderBy(u => u.Id).ToList();
            return putil.GetJsonData(sysroleinfos);
        }
        /// <summary>
        /// 更新用户角色信息
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <returns>标志,成功 success,不成功为空</returns>
        public string SetUserRole(PR_UserInfo userInfo)
        {
            try
            {
                DbEntityEntry<PR_UserInfo> entry = mdb.Entry<PR_UserInfo>(userInfo);
                entry.State = System.Data.Entity.EntityState.Unchanged;
                entry.Property("RoleType").IsModified = true;
                int ret = mdb.SaveChanges();
                if (ret != 0)
                {
                    LogHandle.GetInstance().Info("【更新数据】" + PRBook2._0.Models.Tool.UserInfo.GetInstance().UserId + " 更新用户角色信息", GetType().ToString());
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
        /// 重置密码
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string ResetPassword(string Id)
        {
            try
            {
                int id = int.Parse(Id);
                PR_UserInfo userinfo = new PR_UserInfo();
                userinfo.Id = id;
                userinfo.Password = @"9a9f06a73877922f7a81ceae0921dec85195d54c";
                DbEntityEntry<PR_UserInfo> entry = mdb.Entry<PR_UserInfo>(userinfo);
                entry.State = System.Data.Entity.EntityState.Unchanged;
                entry.Property("Password").IsModified = true;
                int ret = mdb.SaveChanges();
                if (ret != 0)
                {
                    LogHandle.GetInstance().Info("【更新数据】" + PRBook2._0.Models.Tool.UserInfo.GetInstance().UserId + " 重置用户密码", GetType().ToString());
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
    }
}