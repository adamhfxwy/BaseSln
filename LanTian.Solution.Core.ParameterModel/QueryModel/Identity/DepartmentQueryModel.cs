

namespace LanTian.Solution.Core.ParameterModel.QueryModel.Identity
{
    /// <summary>
    /// 部门查询模型
    /// </summary>
    public class DepartmentQueryModel : QueryBase
    {
        /// <summary>
        /// 部门名称
        /// </summary>
        [SearchProperty("DepartmentName")]
        [ContainsProp]
        public string? DepartmentName { get; set; }
    }
}
