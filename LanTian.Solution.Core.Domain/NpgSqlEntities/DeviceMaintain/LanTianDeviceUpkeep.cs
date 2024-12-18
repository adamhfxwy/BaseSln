using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanTian.Solution.Core.Domain.NpgSqlEntities.DeviceMaintain
{
    public class LanTianDeviceUpkeep : BaseEntity
    {
        /// <summary>
        /// 设备编码
        /// </summary>
        public string DeviceNumber { get; private set; } = null!;
        /// <summary>
        /// 设备类型（字典获取）
        /// </summary>
        public string DeviceType { get; private set; } = null!;
        /// <summary>
        /// 保养周期（月）
        /// </summary>
        public int UpkeepCycle { get; private set; }
        /// <summary>
        /// 下次保养时间
        /// </summary>
        public DateTime? UpkeepNextDate { get; private set; }
        /// <summary>
        /// 上次保养时间
        /// </summary>
        public DateTime? UpkeepLastDate { get; private set; }
        private LanTianDeviceUpkeep() { }
        public LanTianDeviceUpkeep(string deviceNumber, string deviceType, int upkeepCycle,DateTime? upkeepLastDate)
        {
            this.DeviceNumber = deviceNumber;
            this.DeviceType = deviceType;
            this.UpkeepCycle = upkeepCycle;
            this.UpkeepLastDate = upkeepLastDate;
            if (upkeepLastDate.HasValue)
            {
                this.UpkeepNextDate = upkeepLastDate.Value.AddMonths(upkeepCycle);
            }
        }
        public void ChangeDeviceNumber(string deviceNumber)
        {
            this.DeviceNumber = deviceNumber;
        }
        public void ChangeDeviceType(string deviceType)
        {
            this.DeviceType = deviceType;
        }
        public void ChangeUpkeepCycle(int upkeepCycle)
        {
            this.UpkeepCycle = upkeepCycle;
            if (this.UpkeepLastDate.HasValue)
            {
                this.UpkeepNextDate = this.UpkeepLastDate.Value.AddMonths(upkeepCycle);
            }
        }
        public void ChangeUpkeepLastDate(DateTime upkeepLastDate)
        {
            this.UpkeepLastDate = upkeepLastDate;
            this.UpkeepNextDate = upkeepLastDate.AddMonths(this.UpkeepCycle);
        }
    }
}
