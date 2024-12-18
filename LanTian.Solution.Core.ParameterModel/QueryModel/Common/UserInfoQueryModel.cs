

namespace LanTian.Solution.Core.ParameterModel.QueryModel.Common
{
    public class UserInfoQueryModel : QueryBase
    {
        /// <summary>
        /// 用户名称
        /// </summary>
        [SearchProperty("Name")]
        public string? Name { get; set; }
        /// <summary>
        /// 联系方式
        /// </summary>
        [SearchProperty("Cellphone")]
        public string? Cellphone { get; set; }
    }
}
