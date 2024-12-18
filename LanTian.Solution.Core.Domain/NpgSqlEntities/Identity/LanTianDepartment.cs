
using LanTian.Solution.Core.EnumAndConstent.SubEntitys;

namespace LanTian.Solution.Core.Domain.NpgSqlEntities.Identity
{
    /// <summary>
    /// 部门实体
    /// </summary>
    public class LanTianDepartment : BaseEntity
    {
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { get; private set; } = null!;
        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; private set; }
        /// <summary>
        /// 员工导航属性
        /// </summary>
        public List<LanTianEmployee>? Employees { get; private set; }
        /// <summary>
        /// 部门负责人
        /// </summary>
        public DepartmentLeaderEntity? DepartmentLeader { get; private set; }
        private LanTianDepartment()
        {

        }
        public LanTianDepartment(string departmentName, string? remark, DepartmentLeaderEntity? departmentLeader)
        {
            this.DepartmentName = departmentName;
            this.Remark = remark;
            this.DepartmentLeader = departmentLeader;
        }
        public void ChangeDepartMentName(string departmentName)
        {
            this.DepartmentName = departmentName;
        }
        public void ChangeRemark(string remark)
        {
            Remark = remark;
        }
        public void ChangeDepartmentLeader(DepartmentLeaderEntity departmentLeader)
        {
            this.DepartmentLeader = departmentLeader;
        }
    }
}
