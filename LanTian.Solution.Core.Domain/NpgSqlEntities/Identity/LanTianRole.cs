

namespace LanTian.Solution.Core.Domain.NpgSqlEntities.Identity
{
    /// <summary>
    /// 角色（职位）实体
    /// </summary>
    public class LanTianRole : BaseEntity
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; private set; } = null!;
        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; private set; }

        /// <summary>
        /// web权限项
        /// </summary>
        public long[]? Permissions { get; private set; }
        /// <summary>
        /// 移动端权限项
        /// </summary>
        public long[]? MobilePermissions { get; private set; }
        /// <summary>
        /// 员工导航属性
        /// </summary>
        public List<LanTianEmployee>? Employees { get; private set; }
        private LanTianRole()
        {

        }
        public LanTianRole(string roleName, long[]? permissions, long[]? mobilePermissions, string? remark)
        {
            RoleName = roleName;
            Permissions = permissions;
            Remark = remark;
            //this.CreateTime = DateTime.Now;
            MobilePermissions = mobilePermissions;
        }
        public void ChangeMobilePermissions(long[] mobilePermissions)
        {
            MobilePermissions = mobilePermissions;
        }
        public void ChangePermissions(long[] permissions)
        {
            Permissions = permissions;
        }
        public void ChangeRemark(string remark)
        {
            Remark = remark;
        }
        public void ChangeRoleName(string roleName)
        {
            RoleName = roleName;
        }
    }
}
