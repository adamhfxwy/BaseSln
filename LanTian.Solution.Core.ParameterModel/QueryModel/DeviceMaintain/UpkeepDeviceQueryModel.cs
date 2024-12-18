using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanTian.Solution.Core.ParameterModel.QueryModel.DeviceMaintain
{
    public class UpkeepDeviceQueryModel
    {
        public IsDeletedEnum IsDeleted { get; set; } = IsDeletedEnum.未删除;
    }
}
