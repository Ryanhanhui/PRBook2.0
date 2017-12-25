using PRBook2._0.Models.Tool;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace PRBook2._0.Models.LogicL
{
    public class RoleManage
    {
        PublicUtil putil = new PublicUtil();
        PRBookEntities mdb = new PRBookEntities();
        public RoleManage()
        {
            
        }
        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <param name="currpage">当前页</param>
        /// <param name="pagesize">每一页个数</param>
        /// <returns>数据列表json</returns>
        public string GetData(int currpage, int pagesize)
        {
            List<SYS_RoleInfo> sysRoleInfo =
                mdb.SYS_RoleInfo.OrderBy(u => u.Id).Skip((currpage - 1) * pagesize).Take(pagesize).ToList();
            return putil.GetJsonData(sysRoleInfo);
        }
        /// <summary>
        /// 获取信息条数（无条件）
        /// </summary>
        /// <returns></returns>
        public int GetDataCount()
        {
            return mdb.SYS_RoleInfo.OrderBy(u => u.Id).ToList().Count;
        }
        /// <summary>
        /// 获取编辑页的数据
        /// </summary>
        /// <param name="Id">数据ID，为空则为新增状态</param>
        /// <returns>单条记录json</returns>
        public string GetDetail(string Id)
        {
            int id = int.Parse(Id);
            SYS_RoleInfo sysRoleInfo = mdb.SYS_RoleInfo.Where(u => u.Id == id).ToList().SingleOrDefault();
            return putil.GetJsonData(sysRoleInfo);
        }
        /// <summary>
        /// 获取编辑页的数据
        /// </summary>
        /// <param name="Id">数据ID，为空则为新增状态</param>
        /// <returns>单条记录json</returns>
        public SYS_RoleInfo GetDetailObj(string Id)
        {
            int id = int.Parse(Id);
            SYS_RoleInfo sysRoleInfo = mdb.SYS_RoleInfo.Where(u => u.Id == id).ToList().SingleOrDefault();
            return sysRoleInfo;
        }
        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="sysRoleInfo"></param>
        /// <returns></returns>
        public string AddData(SYS_RoleInfo sysRoleInfo)
        {
            try
            {
                if (!IsExists(sysRoleInfo))
                    return "exists";
                mdb.SYS_RoleInfo.Add(sysRoleInfo);
                int ret = mdb.SaveChanges();
                if (ret != 0)
                {
                    LogHandle.GetInstance().Info("【添加数据】" + UserInfo.GetInstance().UserId + " 添加角色信息", GetType().ToString());
                    return "success";
                }
                else
                    return "";
            }
            catch (Exception ex)
            {
                LogHandle.GetInstance().Error(ex.Message, GetType().ToString());
                return "";
            }
        }
        private bool IsExists(SYS_RoleInfo sysRoleInfo)
        {
            int ncount = mdb.SYS_RoleInfo.Where(u => u.RoleName.Equals(sysRoleInfo.RoleName)).ToList().Count;

            if (ncount > 0)
                return false;
            else
                return true;
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="sysRoleInfo"></param>
        /// <returns>标志,成功 success,不成功为空</returns>
        public string UpdateData(SYS_RoleInfo sysRoleInfo)
        {
            try
            {
                DbEntityEntry<SYS_RoleInfo> entry = mdb.Entry<SYS_RoleInfo>(sysRoleInfo);
                entry.State = System.Data.Entity.EntityState.Unchanged;
                entry.Property("RoleDesc").IsModified = true;
                entry.Property("RoleIndexPage").IsModified = true;
                int ret = mdb.SaveChanges();
                if (ret != 0)
                {
                    LogHandle.GetInstance().Info("【更新数据】" + UserInfo.GetInstance().UserId + " 更新角色信息", GetType().ToString());
                    return "success";
                }
                else
                    return "";
            }
            catch (Exception ex)
            {
                LogHandle.GetInstance().Error(ex.Message, GetType().ToString());
                return "";
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
                SYS_RoleInfo sysRoleInfo = mdb.SYS_RoleInfo.Where(u => u.Id == id).FirstOrDefault();
                mdb.SYS_RoleInfo.Remove(sysRoleInfo);//删除实体
                int ret = mdb.SaveChanges();
                if (ret != 0)
                {
                    LogHandle.GetInstance().Info("【删除数据】"+UserInfo.GetInstance().UserId+" 删除角色数据", GetType().ToString());
                    return "success";
                }
                else
                    return "";
            }
            catch (Exception ex)
            {
                LogHandle.GetInstance().Error(ex.Message, GetType().ToString());
                return "";
            }
        }
        /// <summary>
        /// 获取角色的权限节点信息
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <returns>节点集合</returns>
        public string GetRolePower(string roleId)
        {
            int rid=int.Parse(roleId);
            List<SYS_RolePower> sysRolePower = mdb.SYS_RolePower.Where(u => u.RoleId == rid).ToList();
            return putil.GetJsonData(sysRolePower);
        }
        /// <summary>
        /// 更新角色权限
        /// </summary>
        /// <param name="sysRolePowers">角色权限</param>
        /// <param name="roleId">角色id</param>
        /// <returns>标志,成功 success,不成功为空</returns>
        public string UpdateRolePower(List<SYS_RolePower> sysRolePowers,string roleId)
        {
            try
            {
                int roleid = int.Parse(roleId);
                //删除之前的权限
                List<SYS_RolePower> dlist = mdb.SYS_RolePower.Where(u => u.RoleId == roleid).ToList();
                mdb.SYS_RolePower.RemoveRange(dlist);
                //重新插入
                mdb.SYS_RolePower.AddRange(sysRolePowers);
                int ret = mdb.SaveChanges();
                if (ret != 0)
                {
                    LogHandle.GetInstance().Info("【更新数据】" + UserInfo.GetInstance().UserId + " 更新角色权限", GetType().ToString());
                    return "success";
                }
                else
                    return "";
            }
            catch (Exception ex)
            {
                LogHandle.GetInstance().Error(ex.Message, GetType().ToString());
                return "";
            }
        }
    }
}