

namespace LanTian.Solution.Core.ParameterModel.QueryModel.Common
{
    public class WorkOrderQueryModel:QueryBase
    {
        /// <summary>
        /// 工单号
        /// </summary>
        [SearchProperty("OrderNumber")]
        public string? OrderNumber {  get; set; }
        /// <summary>
        /// 工单类型  1-维修类、2-安全隐患、3-卫生类
        /// </summary>
        [SearchProperty("OrderType")]
        public OrderTypeEnum? OrderType { get; set; }
        /// <summary>
        /// 上报途径  1-巡检上报、2-用户上报、3-设备上报
        /// </summary>
        [SearchProperty("ReportingChannels")]
        public ReportingChannelsEnum? ReportingChannels { get; set; }
        /// <summary>
        /// 工单状态 1-未处理 2-已处理
        /// </summary>
        [SearchProperty("Status")]
        public WorkOrderStatusEnum? Status { get; set; }
        /// <summary>
        /// 问题所属部门id
        /// </summary>
        [SearchProperty("DepartmentId")]
        public long? DepartmentId { get; set; }
        /// <summary>
        /// 上报人id
        /// </summary>
        [SearchProperty("ReportPersonId")]
        public long? ReportPersonId { get; set; }
        /// <summary>
        /// 按年查询参数
        /// </summary>
        public string? SearchYear { get; set; }
        /// <summary>
        /// 按月查询参数
        /// </summary>
        public string? SearchYearMonth { get; set; }
    }
}
