using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanTian.Solution.Core.Domain.NpgSqlEntities.DeviceMaintain
{
    public class LanTianDeviceUpkeepStatement:BaseEntity
    {
        /// <summary>
        /// 设备编码
        /// </summary>
        public string DeviceNumber { get; init; } = null!;
        /// <summary>
        /// 设备类型（字典获取）
        /// </summary>
        public string DeviceType { get; init; } = null!;
        /// <summary>
        /// 保养周期（月）
        /// </summary>
        public int UpkeepCycle { get; init; }
        /// <summary>
        /// 保养描述
        /// </summary>
        public string? Description { get; init; }
        /// <summary>
        /// 维护人id
        /// </summary>
        public long EmployeeId { get; init; }
        /// <summary>
        /// 维护人姓名
        /// </summary>
        public string EmployeeName { get; init; } = null!;
        /// <summary>
        /// 本次应保养时间
        /// </summary>
        public DateTime? ThisUpkeepTime { get; init; }
        /// <summary>
        /// 是否超时保养：1-否 2-是
        /// </summary>
        public IsTimeoutEnum IsTimeout { get; init; }
        /// <summary>
        /// 产生的费用
        /// </summary>
        public decimal GenerateCosts { get; init; }
        /// <summary>
        /// 实际保养时间
        /// </summary>
        public DateTime RealityUpkeepTime { get; init; }
        private LanTianDeviceUpkeepStatement()
        {

        }
        public LanTianDeviceUpkeepStatement(string deviceNumber, string deviceType, int upkeepCycle, string? description
            , long employeeId, string employeeName, DateTime? thisUpkeepTime, IsTimeoutEnum isTimeout, decimal generateCosts, DateTime realityUpkeepTime)
        {
            this.DeviceNumber = deviceNumber;
            this.DeviceType = deviceType;
            this.UpkeepCycle = upkeepCycle;
            this.Description = description;
            this.EmployeeId = employeeId;
            this.EmployeeName = employeeName;
            this.ThisUpkeepTime = thisUpkeepTime;
            this.GenerateCosts = generateCosts;
            this.IsTimeout = isTimeout;
            this.RealityUpkeepTime = realityUpkeepTime;
        }
    }
}
