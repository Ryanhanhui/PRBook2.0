﻿//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace PRBook2._0.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Data.Entity.Core.Objects.DataClasses;
    using System.Linq;
    
    public partial class PRBookEntities : DbContext
    {
        public PRBookEntities()
            : base("name=PRBookEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<NodeSetInfo> NodeSetInfo { get; set; }
        public DbSet<PR_UserInfo> PR_UserInfo { get; set; }
        public DbSet<SYS_LoginImg> SYS_LoginImg { get; set; }
        public DbSet<SYS_RoleInfo> SYS_RoleInfo { get; set; }
        public DbSet<SYS_SystemConfigInfo> SYS_SystemConfigInfo { get; set; }
        public DbSet<v_getuserpower> v_getuserpower { get; set; }
        public DbSet<v_userinfo> v_userinfo { get; set; }
        public DbSet<PR_MoneyInfo> PR_MoneyInfo { get; set; }
        public DbSet<SYS_RolePower> SYS_RolePower { get; set; }
        public DbSet<PR_PeopleInfo> PR_PeopleInfo { get; set; }
        public DbSet<SYS_LoginLog> SYS_LoginLog { get; set; }
        public DbSet<v_sys_loginlog> v_sys_loginlog { get; set; }
        public DbSet<v_pr_peopleinfo> v_pr_peopleinfo { get; set; }
    
        [EdmFunction("PRBookEntities", "TBFun_GetUserPower")]
        public virtual IQueryable<TBFun_GetUserPower_Result> TBFun_GetUserPower(string usertype, string roletype)
        {
            var usertypeParameter = usertype != null ?
                new ObjectParameter("usertype", usertype) :
                new ObjectParameter("usertype", typeof(string));
    
            var roletypeParameter = roletype != null ?
                new ObjectParameter("roletype", roletype) :
                new ObjectParameter("roletype", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<TBFun_GetUserPower_Result>("[PRBookEntities].[TBFun_GetUserPower](@usertype, @roletype)", usertypeParameter, roletypeParameter);
        }
    }
}
