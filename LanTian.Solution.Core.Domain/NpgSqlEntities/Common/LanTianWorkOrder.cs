using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanTian.Solution.Core.Domain.NpgSqlEntities.Common
{
    public class LanTianWorkOrder:BaseEntity
    {
        /// <summary>
        /// 工单号
        /// </summary>
        public string OrderNumber { get; init; } = null!;
        /// <summary>
        /// 工单类型  1-维修类、2-安全隐患、3-卫生类
        /// </summary>
        public OrderTypeEnum OrderType { get; init; }
        /// <summary>
        /// 上报途径  1-巡检上报、2-用户上报、3-设备上报
        /// </summary>
        public ReportingChannelsEnum ReportingChannels { get; init; }
        /// <summary>
        /// 问题描述
        /// </summary>
        public string? ProblemDescription { get; private set; }
        /// <summary>
        /// 问题照片
        /// </summary>
        public string? ProblemPicPath { get; private set; }
        /// <summary>
        /// 工单状态 1-未处理 2-已处理
        /// </summary>
        public WorkOrderStatusEnum Status { get; private set; }
        /// <summary>
        /// 上报人id
        /// </summary>
        public long ReportPersonId { get; init; }
        /// <summary>
        /// 上报人姓名
        /// </summary>
        public string ReportPersonName { get; init; } = null!;
        /// <summary>
        /// 处理人id
        /// </summary>
        public long? HandlePersonId { get; private set; }
        /// <summary>
        /// 处理人姓名
        /// </summary>
        public string? HandlePersonName { get; private set; }
        /// <summary>
        /// 问题所属部门id
        /// </summary>
        public long? DepartmentId { get; private set; }
        /// <summary>
        /// 问题所属部门name
        /// </summary>
        public string? DepartmentName { get; private set; }
        /// <summary>
        /// 问题解决描述
        /// </summary>
        public string? HandledDescription { get; private set; }
        /// <summary>
        /// 处理后图片
        /// </summary>
        public string? HandledPicPath { get; private set; }
        /// <summary>
        /// 处理时间
        /// </summary>
        public DateTime? HandledTime { get; private set; }
        private LanTianWorkOrder()
        {

        }
        public LanTianWorkOrder(string orderNumber, OrderTypeEnum orderType, ReportingChannelsEnum reportingChannels, string? problemDescription, string? problemPicPath
            , long reportPersonId, string reportPersonName, long? departmentId, string? departmentName)
        {
            this.OrderNumber = orderNumber;
            this.OrderType = orderType;
            this.ReportingChannels = reportingChannels;
            this.ProblemDescription = problemDescription;
            this.ProblemPicPath = problemPicPath;
            this.ReportPersonId = reportPersonId;
            this.ReportPersonName = reportPersonName;
            this.DepartmentId = departmentId;
            this.DepartmentName = departmentName;
            this.Status = WorkOrderStatusEnum.未处理;
        }
        public void ChangeHandlePersonId(long handlePersonId)
        {
            this.HandlePersonId = handlePersonId;
        }
        public void ChangeHandlePersonName(string handlePersonName)
        {
            this.HandlePersonName = handlePersonName;
        }
        public void ChangeHandledDescription(string handledDescription)
        {
            this.HandledDescription = handledDescription;
        }
        public void ChangeHandledPicPath(string handledPicPath)
        {
            this.HandledPicPath = handledPicPath;
        }
        public void ChangeHandledTime(DateTime handledTime)
        {
            this.HandledTime = handledTime;
        }
        public void ChangeStatus()
        {
            this.Status = WorkOrderStatusEnum.已处理;
        }
    }
}
