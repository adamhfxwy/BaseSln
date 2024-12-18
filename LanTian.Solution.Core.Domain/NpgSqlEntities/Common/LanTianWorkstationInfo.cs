using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanTian.Solution.Core.Domain.NpgSqlEntities.Common
{
    public class LanTianWorkstationInfo:BaseEntity
    {
        /// <summary>
        /// 工位号
        /// </summary>
        public string WorkstationNumber { get; init; } = null!;
        /// <summary>
        /// 楼id
        /// </summary>
        public long BuildingId { get; private set; }      
        /// <summary>
        /// 层id
        /// </summary>
        public long StoreyId { get; private set; }       
        /// <summary>
        /// 房间id
        /// </summary>
        public long RoomId { get; private set; }        
        /// <summary>
        /// 房间id
        /// </summary>
        public long? EmployeeId { get; private set; }    
        /// <summary>
        /// 部门id
        /// </summary>
        public long? DepartmentId { get; private set; }
       
        /// <summary>
        /// 状态 1-空闲 2-占用
        /// </summary>
        public WorkstationStatusEnum Status { get; private set; }
        /// <summary>
        /// 员工导航属性
        /// </summary>
        public LanTianEmployee? Employee { get; init; }
        /// <summary>
        /// 部门导航属性
        /// </summary>
        public LanTianDepartment? Department { get; init; }

        private LanTianWorkstationInfo()
        {

        }
        public LanTianWorkstationInfo(string workstationNumber, long buildingId, long storeyId, long roomId, long? employeeId, long? departmentId)
        {
            this.WorkstationNumber = workstationNumber;
            this.BuildingId = buildingId;
            this.StoreyId = storeyId;
            this.RoomId = roomId;
            this.EmployeeId = employeeId;
            this.DepartmentId = departmentId;
            if (employeeId.HasValue && employeeId.Value > 0)
            {
                this.Status = WorkstationStatusEnum.占用;
            }
            else
            {
                this.Status = WorkstationStatusEnum.空闲;
            }
          
        }
        public void ChangeBuildingId(long buildingId)
        {
            this.BuildingId = buildingId;
        }
        public void ChangeStoreyId(long storeyId)
        {
            this.StoreyId = storeyId;
        }
        public void ChangeRoomId(long roomId)
        {
            this.RoomId = roomId;
        }
        public void ChangeEmployeeId(long employeeId)
        {
            this.EmployeeId = employeeId;
        }
        public void ChangeDepartmentId(long departmentId)
        {
            this.DepartmentId = departmentId;
        }
        public void ChangeStatus(WorkstationStatusEnum status)
        {
            this.Status = status;
        }
    }
}
