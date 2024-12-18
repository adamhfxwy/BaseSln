using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LanTian.Solution.Core.EnumAndConstent.Enums.LanTianEnum;

namespace LanTian.Solution.Core.DTO.DeviceMaintain
{
    public class DeviceUpkeepStatementDTO
    {
        public long Id { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 设备编码
        /// </summary>
        public string DeviceNumber { get; set; } = null!;
        /// <summary>
        /// 设备类型（字典获取）
        /// </summary>
        public string DeviceType { get; set; } = null!;
        /// <summary>
        /// 保养周期（月）
        /// </summary>
        public int UpkeepCycle { get; set; }
        /// <summary>
        /// 保养描述
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// 维护人id
        /// </summary>
        public long EmployeeId { get; set; }
        /// <summary>
        /// 维护人姓名
        /// </summary>
        public string EmployeeName { get; set; } = null!;
        /// <summary>
        /// 本次应保养时间
        /// </summary>
        public DateTime? ThisUpkeepTime { get; set; }
        /// <summary>
        /// 实际保养时间
        /// </summary>
        public DateTime? RealityUpkeepTime { get; set; }
        /// <summary>
        /// 是否超时保养：1-否 2-是
        /// </summary>
        public int IsTimeout { get; set; }
        /// <summary>
        /// 是否超时保养：1-否 2-是
        /// </summary>
        public string? IsTimeoutStr {  get; set; }
        /// <summary>
        /// 产生的费用
        /// </summary>
        public decimal GenerateCosts { get; set; }
       
    }
}
