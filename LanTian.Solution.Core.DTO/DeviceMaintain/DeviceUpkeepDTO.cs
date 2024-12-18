using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanTian.Solution.Core.DTO.DeviceMaintain
{
    public class DeviceUpkeepDTO
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
        /// 下次保养时间
        /// </summary>
        public DateTime? UpkeepNextDate { get; set; }
        /// <summary>
        /// 上次保养时间
        /// </summary>
        public DateTime? UpkeepLastDate { get; set; }
      
    }
}
