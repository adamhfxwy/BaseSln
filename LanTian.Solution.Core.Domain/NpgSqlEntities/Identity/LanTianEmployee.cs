


namespace LanTian.Solution.Core.Domain.NpgSqlEntities.Identity
{
    /// <summary>
    /// 员工实体
    /// </summary>
    public class LanTianEmployee : BaseEntity
    {
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; private set; } = null!;
        /// <summary>
        /// 用户名
        /// </summary>
        public Guid UserName { get; init; } = Guid.NewGuid();
        /// <summary>
        /// 手机号
        /// </summary>
        public string Cellphone { get; private set; } = null!;
        /// <summary>
        /// 角色（职位）id
        /// </summary>
        public long? RoleId { get; private set; }
        /// <summary>
        /// 班次id
        /// </summary>
        public long? ShiftId { get; private set; }
        /// <summary>
        /// 性别 1-男 2-女
        /// </summary>
        public SexEnum Sex { get; private set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; private set; }
        /// <summary>
        /// 登录权限 1-无权限 2-web权限  3-app权限 4-小程序权限  5-所有权限
        /// </summary>
        public LoginPermissionsEnum LoginPermissions { get; private set; }
        /// <summary>
        /// 密码
        /// </summary>
        private string? passwordHash;
        /// <summary>
        /// 盐
        /// </summary>
        private string? passwordSalt;
        /// <summary>
        /// 部门id
        /// </summary>
        public long? DepartmentId { get; private set; }
        /// <summary>
        /// 员工状态 1-正常 2-离职
        /// </summary>
        public EmployeeStatusEnum Status { get; private set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public int? Age { get; private set; }
        /// <summary>
        /// 照片地址
        /// </summary>
        public string? PhotoPath { get; private set; }
        /// <summary>
        /// 角色（职位）导航属性
        /// </summary>
        public LanTianRole? Role { get; private set; }
        /// <summary>
        /// 部门导航属性
        /// </summary>
        public LanTianDepartment? Department { get; private set; }
        public LanTianShiftInfo? ShiftInfo { get; private set; }
        private LanTianEmployee()
        {

        }
        public LanTianEmployee(string realName, string cellphone, long? roleId, SexEnum sex, string? remark,
            LoginPermissionsEnum loginPermissions, long? departmentId, int? age, string? photoPath,long? shiftId)
        {
            PhotoPath = photoPath;
            Age = age;
            Sex = sex;
            LoginPermissions = loginPermissions;
            Status = EmployeeStatusEnum.正常;
            if (roleId.HasValue && roleId.Value>0)
            {
                RoleId = roleId;
            }
            if (shiftId.HasValue && shiftId.Value > 0)
            {
                ShiftId = shiftId;
            }
            if (departmentId.HasValue && departmentId.Value > 0)
            {
                DepartmentId = departmentId;
            }          
            Remark = remark;
            Cellphone = cellphone;
            RealName = realName;
            //this.UserName = Guid.NewGuid();
        }
        public void ChangePhotoPath(string photoPath)
        {
            PhotoPath = photoPath;
        }
        public void ChangeAge(int age)
        {
            Age = age;
        }
        public void ChangeSex(SexEnum sex)
        {
            Sex = sex;
        }
        public void ChangeLoginPermissions(LoginPermissionsEnum loginPermissions)
        {
            LoginPermissions = loginPermissions;
        }
        public void ChangeStatus(EmployeeStatusEnum status)
        {
            Status = status;
        }
        public void ChangeRoleId(long roleId)
        {
            RoleId = roleId;
        }
        public void ChangeShiftId(long shiftId)
        {
            ShiftId = shiftId;
        }
        public void ChangeDepartmentId(long departmentId)
        {
            DepartmentId = departmentId;
        }
        public void ChangeRemark(string remark)
        {
            Remark = remark;
        }
        public void ChangeCellPhone(string cellphone)
        {
            Cellphone = cellphone;
        }
        public void ChangeRealName(string realName)
        {
            RealName = realName;
        }

        public Tuple<bool, string> ChangePassword(string password)
        {
            if (password.Length < 6)
            {
                return Tuple.Create(false, "密码太短！");
            }
            passwordSalt = Guid.NewGuid().ToString();
            passwordHash = CommonUtils.Hash($"{password}{passwordSalt}");
            return Tuple.Create(true, "success");
        }
        public bool CheckPassword(string password)
        {
            string hash = CommonUtils.Hash($"{password}{passwordSalt}");
            return passwordHash == hash;
        }


    }
}
