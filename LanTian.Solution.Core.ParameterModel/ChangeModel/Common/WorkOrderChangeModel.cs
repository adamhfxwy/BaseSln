

namespace LanTian.Solution.Core.ParameterModel.ChangeModel.Common
{
    public class WorkOrderChangeModel
    {
        public long? Id { get; set; }
        /// <summary>
        /// 工单号
        /// </summary>
        public string? OrderNumber { get; set; }
        /// <summary>
        /// 工单类型  1-维修类、2-安全隐患、3-卫生类
        /// </summary>
        public OrderTypeEnum? OrderType { get; set; }
        /// <summary>
        /// 上报途径  1-巡检上报、2-用户上报、3-设备上报
        /// </summary>
        public ReportingChannelsEnum? ReportingChannels { get; set; }
        /// <summary>
        /// 问题描述
        /// </summary>
        public string? ProblemDescription { get; set; }
        /// <summary>
        /// 问题照片
        /// </summary>
        public string? ProblemPicPath { get; set; }
        /// <summary>
        /// 问题照片数组
        /// </summary>
        public string[]? ProblemPicPathArr { get; set; }
        /// <summary>
        /// 上报人id (移动端无需传参)
        /// </summary>
        public long? ReportPersonId { get; set; }
        /// <summary>
        /// 上报人姓名 (移动端无需传参)
        /// </summary>
        public string? ReportPersonName { get; set; } 
        /// <summary>
        /// 处理人id
        /// </summary>
        public long? HandlePersonId { get; set; }
        /// <summary>
        /// 处理人姓名
        /// </summary>
        public string? HandlePersonName { get; set; }
        /// <summary>
        /// 问题所属部门id
        /// </summary>
        public long? DepartmentId { get; set; }
        /// <summary>
        /// 问题所属部门name
        /// </summary>
        public string? DepartmentName { get; set; }
        /// <summary>
        /// 问题解决描述
        /// </summary>
        public string? HandledDescription { get; set; }
        /// <summary>
        /// 处理后图片
        /// </summary>
        public string? HandledPicPath { get; set; }
        /// <summary>
        /// 处理照片数组
        /// </summary>
        public string[]? HandledPicPathArr { get; set; }
        /// <summary>
        /// 处理时间
        /// </summary>
        public string? HandledTime { get; set; }

    }
}
