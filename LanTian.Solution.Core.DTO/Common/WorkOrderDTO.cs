

namespace LanTian.Solution.Core.DTO.Common
{
    public class WorkOrderDTO
    {
        public long Id { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 工单号
        /// </summary>
        public string OrderNumber { get; set; } = null!;
        /// <summary>
        /// 工单类型  1-维修类、2-安全隐患、3-卫生类
        /// </summary>
        public int OrderType { get; set; }
        /// <summary>
        /// 工单类型  1-维修类、2-安全隐患、3-卫生类
        /// </summary>
        public string OrderTypeStr { get; set; } = null!;
        /// <summary>
        /// 上报途径  1-巡检上报、2-用户上报、3-设备上报
        /// </summary>
        public int ReportingChannels { get; set; }
        /// <summary>
        /// 上报途径  1-巡检上报、2-用户上报、3-设备上报
        /// </summary>
        public string ReportingChannelsStr { get; set; } = null!;
        /// <summary>
        /// 问题描述
        /// </summary>
        public string? ProblemDescription { get; set; }
        /// <summary>
        /// 问题照片
        /// </summary>
        public string? ProblemPicPath { get; set; }
        /// <summary>
        /// 工单状态 1-未处理 2-已处理
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 工单状态 1-未处理 2-已处理
        /// </summary>
        public string StatusStr { get; set; } = null!;
        /// <summary>
        /// 上报人id
        /// </summary>
        public long ReportPersonId { get; set; }
        /// <summary>
        /// 上报人姓名
        /// </summary>
        public string ReportPersonName { get; set; } = null!;
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
        /// 处理时间
        /// </summary>
        public DateTime? HandledTime { get; set; }
        /// <summary>
        /// 时间轴
        /// </summary>
        public int? TimeAxis { get; set; }
        /// <summary>
        /// 已处理数量
        /// </summary>
        public int ProcessedCount {  get; set; }
        /// <summary>
        /// 未处理数量
        /// </summary>
        public int UnprocessedCount { get; set; }
        /// <summary>
        /// 问题照片数组
        /// </summary>
        public string[]? ProblemPicPathArr { get; set; }
        /// <summary>
        /// 处理照片数组
        /// </summary>
        public string[]? HandledPicPathArr { get; set; }
       
    }
}
