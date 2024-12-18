using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanTian.Solution.Core.ParameterModel.ChangeModel.DeviceMaintain
{
    public class DeviceUpkeepStatementChangeModel
    {
        /// <summary>
        /// id主键
        /// </summary>
        public long? Id { get; set; }
        /// <summary>
        /// 设备编码
        /// </summary>
        public string? DeviceNumber { get; set; } 
        /// <summary>
        /// 设备类型（字典获取）
        /// </summary>
        public string? DeviceType { get; set; }
        /// <summary>
        /// 保养周期（月）（无需传参）
        /// </summary>
        public int? UpkeepCycle { get; set; }
        /// <summary>
        /// 保养描述
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// 维护人id（移动端无需传参）
        /// </summary>
        public long? EmployeeId { get; set; }
        /// <summary>
        /// 维护人姓名（移动端无需传参）
        /// </summary>
        public string? EmployeeName { get; set; }
        /// <summary>
        /// 本次应保养时间（无需传参）
        /// </summary>
        public DateTime? ThisUpkeepTime { get; set; }
        /// <summary>
        /// 实际保养时间(移动端无需传参)
        /// </summary>
        public string? RealityUpkeepTime { get; set; }
        /// <summary>
        /// 是否超时保养：1-否 2-是（无需传参）
        /// </summary>
        public IsTimeoutEnum? IsTimeout { get; set; }
        /// <summary>
        /// 产生的费用
        /// </summary>
        public decimal? GenerateCosts { get; set; }
    }
}
