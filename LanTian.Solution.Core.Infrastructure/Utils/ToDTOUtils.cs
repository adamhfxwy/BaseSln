


namespace LanTian.Solution.Core.Infrastructure.Utils
{
    public class ToDTOUtils
    {
    
        public static OperationLogDTO ToDTO(LanTianOperationLog entity)
        {
            OperationLogDTO dto = new OperationLogDTO();
            dto = CommonUtils.Mapper<OperationLogDTO, LanTianOperationLog>(entity);
            return dto;
        }
        public static ShiftInfoDTO ToDTO(LanTianShiftInfo entity)
        {
            ShiftInfoDTO dto = new ShiftInfoDTO();
            dto = CommonUtils.Mapper<ShiftInfoDTO, LanTianShiftInfo>(entity);
            return dto;
        }
        public static UserInfoDTO ToDTO(LanTianUserInfo entity)
        {
            UserInfoDTO dto = new UserInfoDTO();
            dto = CommonUtils.Mapper<UserInfoDTO, LanTianUserInfo>(entity);
            return dto;
        }
        public static WorkOrderDTO ToDTO(LanTianWorkOrder entity)
        {
            WorkOrderDTO dto = new WorkOrderDTO();
            dto = CommonUtils.Mapper<WorkOrderDTO, LanTianWorkOrder>(entity);
            dto.Status = (int)entity.Status;
            dto.StatusStr = entity.Status.ToString();
            dto.OrderType = (int)entity.OrderType;
            dto.OrderTypeStr = entity.OrderType.ToString();
            dto.ReportingChannels = (int)entity.ReportingChannels;
            dto.ReportingChannelsStr = entity.ReportingChannels.ToString();
            if (!string.IsNullOrEmpty(entity.ProblemPicPath))
            {
                dto.ProblemPicPathArr = entity.ProblemPicPath.Split(',');
            }
            if (!string.IsNullOrEmpty(entity.HandledPicPath))
            {
                dto.HandledPicPathArr = entity.HandledPicPath.Split(',');
            }

            return dto;
        }
        public static DictionaryDTO ToDTO(LanTianDictionary entity)
        {
            DictionaryDTO dto = new DictionaryDTO();
            dto = CommonUtils.Mapper<DictionaryDTO, LanTianDictionary>(entity);
            return dto;
        }
        public static DeviceUpkeepDTO ToDTO(LanTianDeviceUpkeep entity)
        {
            DeviceUpkeepDTO dto = new DeviceUpkeepDTO();
            dto = CommonUtils.Mapper<DeviceUpkeepDTO, LanTianDeviceUpkeep>(entity);
            return dto;
        }
        public static DeviceUpkeepStatementDTO ToDTO(LanTianDeviceUpkeepStatement entity)
        {
            DeviceUpkeepStatementDTO dto = new DeviceUpkeepStatementDTO();
            dto = CommonUtils.Mapper<DeviceUpkeepStatementDTO, LanTianDeviceUpkeepStatement>(entity);
            switch (entity.IsTimeout)
            {
                case IsTimeoutEnum.否:
                    dto.IsTimeoutStr = "否";
                    break;
                case IsTimeoutEnum.是:
                    dto.IsTimeoutStr = "是";
                    break;

            }
            return dto;
        }
        public static DepartmentDTO ToDTO(LanTianDepartment entity)
        {
            DepartmentDTO dto = new DepartmentDTO();
            dto = CommonUtils.Mapper<DepartmentDTO, LanTianDepartment>(entity);
            if (entity.Employees != null && entity.Employees.Count() > 0)
            {
                dto.Employees = entity.Employees.Select(ToDTO).ToList();
            }
            if (entity.DepartmentLeader != null)
            {
                dto.LeaderCellphone = entity.DepartmentLeader.Cellphone;
                dto.EmployeeId = entity.DepartmentLeader.Id;
                dto.LeaderName = entity.DepartmentLeader.RealName;
            }
            return dto;
        }
        public static EmployeeDTO ToDTO(LanTianEmployee entity)
        {
            EmployeeDTO dto = new EmployeeDTO();
            dto = CommonUtils.Mapper<EmployeeDTO, LanTianEmployee>(entity);
            if (entity.Department != null)
            {
                dto.DepartmentName = entity.Department.DepartmentName;
            }
            if (entity.ShiftInfo != null)
            {
                dto.ShiftName = entity.ShiftInfo.ShiftName;
            }
            dto.Sex = (int)entity.Sex;
            dto.SexStr = entity.Sex.ToString();
            dto.LoginPermissions = (int)entity.LoginPermissions;
            dto.LoginPermissionsStr = entity.LoginPermissions.ToString();
            dto.Status = (int)entity.Status;
            dto.StatusStr = entity.Status.ToString();
            if (entity.Role != null)
            {
                dto.RoleName = entity.Role.RoleName;
                dto.Permissions = entity.Role.Permissions;
                dto.MobilePermissions = entity.Role.MobilePermissions;
            }

            return dto;
        }
        public static MenuDTO ToDTO(LanTianMenu entity)
        {
            MenuDTO dto = new MenuDTO();
            dto = CommonUtils.Mapper<MenuDTO, LanTianMenu>(entity);
            dto.IsButton = (int)entity.IsButton;
            dto.IsButtonStr = entity.IsButton.ToString();
            return dto;
        }
        public static MobilePermissionsDTO ToDTO(LanTianMobilePermissions entity)
        {
            MobilePermissionsDTO dto = new MobilePermissionsDTO();
            dto = CommonUtils.Mapper<MobilePermissionsDTO, LanTianMobilePermissions>(entity);
            return dto;
        }
        public static RoleDTO ToDTO(LanTianRole entity)
        {
            RoleDTO dto = new RoleDTO();
            dto = CommonUtils.Mapper<RoleDTO, LanTianRole>(entity);
            if (entity.Employees != null && entity.Employees.Count() > 0)
            {
                dto.Employees = entity.Employees.Select(ToDTO).ToList();
            }
            return dto;
        }
    }
}
