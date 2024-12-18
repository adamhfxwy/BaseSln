using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanTian.Solution.Core.DTO.DeviceMaintain
{
    public class DeviceCommonDTO
    {
        public long Id { get; set; }
        /// <summary>
        /// 设备编码
        /// </summary>
        public string DeviceNumber { get; set; } = null!;
    }
}
