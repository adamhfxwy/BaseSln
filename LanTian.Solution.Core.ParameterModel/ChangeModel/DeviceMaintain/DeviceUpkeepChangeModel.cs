using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanTian.Solution.Core.ParameterModel.ChangeModel.DeviceMaintain
{
    public class DeviceUpkeepChangeModel
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
        /// 保养周期（月）
        /// </summary>
        public int? UpkeepCycle { get; set; }
        /// <summary>
        /// 下次保养时间（无需传参）
        /// </summary>
        public string? UpkeepNextDate { get; set; }
        /// <summary>
        /// 上次保养时间
        /// </summary>
        public string? UpkeepLastDate { get; set; }
    }
}
