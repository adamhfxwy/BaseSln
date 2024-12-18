using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanTian.Solution.Core.ParameterModel.QueryModel.DeviceMaintain
{
    public class DeviceUpkeepQueryModel:QueryBase
    {
        /// <summary>
        /// 设备编号
        /// </summary>
        [SearchProperty("DeviceNumber")]
        public string? DeviceNumber { get; set; }
        /// <summary>
        /// 设备类型
        /// </summary>
        [SearchProperty("DeviceType")]
        public string? DeviceType { get; set; }
    }
}
