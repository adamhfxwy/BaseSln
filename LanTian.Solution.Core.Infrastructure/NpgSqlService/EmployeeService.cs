

using LanTian.Solution.Core.Domain.INpgSqlService;
using LanTian.Solution.Core.DTO.Identity;
using LanTian.Solution.Core.Infrastructure.Utils;
using LanTian.Solution.Core.ParameterModel.ChangeModel.Identity;
using LanTian.Solution.Core.ParameterModel.QueryModel.Identity;
using static LanTian.Solution.Core.EnumAndConstent.Enums.LanTianEnum;

namespace LanTian.Solution.Core.Infrastructure.NpgSqlService
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<LanTianEmployee> _repository;
        public EmployeeService(IRepository<LanTianEmployee> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Tuple<long, string>> AddEmployeeAsync(AddEditEmployeeModel model, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(model.RealName))
            {
                return new Tuple<long, string>(0, "姓名不可为空");
            }
            if (string.IsNullOrEmpty(model.Cellphone))
            {
                return new Tuple<long, string>(0, "手机号不可为空");
            }
            bool exists = await _repository.AnyAsync(x => x.Cellphone == model.Cellphone && x.IsDeleted == IsDeletedEnum.未删除, cancellationToken);
            if (exists)
            {
                return new Tuple<long, string>(0, "手机号不能重复");
            }
            if (model.LoginPermissions == null)
            {
                return new Tuple<long, string>(0, "权限配置不可为空");
            }
            if (model.Sex == null)
            {
                return new Tuple<long, string>(0, "性别不可为空");
            }
            LanTianEmployee entity = new LanTianEmployee(model.RealName, model.Cellphone, model.RoleId, model.Sex.Value, model.Remark
                , model.LoginPermissions.Value, model.DepartmentId, model.Age, model.PhotoPath, model.ShiftId);
            if (!string.IsNullOrEmpty(model.Password))
            {
                var tuple = entity.ChangePassword(model.Password);
                if (!tuple.Item1)
                {
                    return new Tuple<long, string>(0, tuple.Item2);
                }
            }
            entity = await _repository.InsertAsync(entity, true, cancellationToken);
            return new Tuple<long, string>(entity.Id, "success");
        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Tuple<bool, string>> EditEmployeeAsync(AddEditEmployeeModel model, CancellationToken cancellationToken = default)
        {
            var obj = await _repository.FindAsync(model.Id.Value);
            if (obj == null)
            {
                return new Tuple<bool, string>(false, $"id={model.Id}的员工不存在");
            }

            if (!string.IsNullOrEmpty(model.Cellphone))
            {
                bool exists = await _repository.AnyAsync(x => x.Cellphone == model.Cellphone && x.IsDeleted == IsDeletedEnum.未删除 && x.Id != model.Id, cancellationToken);
                if (exists)
                {
                    return new Tuple<bool, string>(false, "手机号不能重复");
                }
                obj.ChangeCellPhone(model.Cellphone);
            }
            if (!string.IsNullOrEmpty(model.Password))
            {
                var tuple = obj.ChangePassword(model.Password);
                if (!tuple.Item1)
                {
                    return new Tuple<bool, string>(false, tuple.Item2);
                }
            }
            if (!string.IsNullOrEmpty(model.Remark))
            {
                obj.ChangeRemark(model.Remark);
            }
            if (!string.IsNullOrEmpty(model.RealName))
            {
                obj.ChangeRealName(model.RealName);
            }
            if (!string.IsNullOrEmpty(model.PhotoPath))
            {
                obj.ChangePhotoPath(model.PhotoPath);
            }
            if (model.Sex.HasValue)
            {
                obj.ChangeSex(model.Sex.Value);
            }
            if (model.Status.HasValue)
            {
                obj.ChangeStatus(model.Status.Value);
            }
            if (model.LoginPermissions.HasValue)
            {
                obj.ChangeLoginPermissions(model.LoginPermissions.Value);
            }
            if (model.RoleId.HasValue && model.RoleId.Value > 0)
            {
                obj.ChangeRoleId(model.RoleId.Value);
            }
            if (model.ShiftId.HasValue && model.ShiftId.Value > 0)
            {
                obj.ChangeShiftId(model.ShiftId.Value);
            }
            if (model.DepartmentId.HasValue && model.DepartmentId.Value > 0)
            {
                obj.ChangeDepartmentId(model.DepartmentId.Value);
            }
            if (model.Age.HasValue)
            {
                obj.ChangeAge(model.Age.Value);
            }
            obj = await _repository.UpdateAsync(obj, true, cancellationToken);
            return new Tuple<bool, string>(true, "success");
        }
        /// <summary>
        /// 登录校验
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<Tuple<bool, string, EmployeeDTO>> CheckLoginAsync(string cellphone, string password, CancellationToken cancellationToken = default)
        {
            var user = await _repository.FindAsync(x => x.Cellphone == cellphone && x.IsDeleted == IsDeletedEnum.未删除, cancellationToken, x => x.Include(i => i.Role));
            if (user == null)
            {
                return new Tuple<bool, string, EmployeeDTO>(false, $"手机号={cellphone}的用户不存在", null);
            }
            bool isOK = user.CheckPassword(password);
            if (isOK)
            {
                var dto = CommonUtils.Mapper<EmployeeDTO, LanTianEmployee>(user);
                if (user.Department != null)
                {
                    dto.DepartmentName = user.Department.DepartmentName;
                }
                if (user.ShiftInfo != null)
                {
                    dto.ShiftName = user.ShiftInfo.ShiftName;
                }
                dto.Sex = (int)user.Sex;
                dto.SexStr = user.Sex.ToString();
                dto.LoginPermissions = (int)user.LoginPermissions;
                dto.LoginPermissionsStr = user.LoginPermissions.ToString();
                dto.Status = (int)user.Status;
                dto.StatusStr = user.Status.ToString();
                if (user.Role != null)
                {
                    dto.RoleName = user.Role.RoleName;
                    dto.Permissions = user.Role.Permissions;
                    dto.MobilePermissions = user.Role.MobilePermissions;
                }
                return new Tuple<bool, string, EmployeeDTO>(true, $"success", dto);
            }
            else
            {
                return new Tuple<bool, string, EmployeeDTO>(false, $"密码错误", null);
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Tuple<bool, string>> RemoveEmployeeAsync(long id, CancellationToken cancellationToken = default)
        {
            var obj = await _repository.FindAsync(id);
            if (obj == null || obj.IsDeleted == IsDeletedEnum.已删除)
            {
                return new Tuple<bool, string>(false, $"id={id}的员工不存在");
            }
            obj.ChangeIsDeleted();
            await _repository.UpdateAsync(obj, true, cancellationToken);
            return new Tuple<bool, string>(true, "success");
        }
        /// <summary>
        /// 根据条件获取分页数据
        /// </summary>
        /// <param name="queryModel"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<Pagination<EmployeeDTO>> GetEmployeeAsync(EmployeeQueryModel queryModel, CancellationToken cancellationToken = default)
        {

            var data = _repository.GetAllAsync().Where(x => x.RealName != "超级管理员");
            var tuple = await CommonWhereBuilder.WhereBuilder(data, queryModel, cancellationToken);
            var list = await tuple.Item1.Select(x => new EmployeeDTO
            {
                Id = x.Id,
                DepartmentId = x.DepartmentId,
                DepartmentName = x.Department.DepartmentName,
                Age = x.Age,
                Cellphone = x.Cellphone,
                LoginPermissions = (int)x.LoginPermissions,
                LoginPermissionsStr = x.LoginPermissions.ToString(),
                Sex = (int)x.Sex,
                SexStr = x.Sex.ToString(),
                PhotoPath = x.PhotoPath,
                CreateTime = x.CreateTime,
                RealName = x.RealName,
                Remark = x.Remark,
                RoleId = x.RoleId,
                RoleName = x.Role.RoleName,
                Status = (int)x.Status,
                StatusStr = x.Status.ToString(),
                UserName = x.UserName,
                ShiftId = x.ShiftId,
                ShiftName = x.ShiftInfo.ShiftName
            }).ToListAsync(cancellationToken);
            return new Pagination<EmployeeDTO> { List = list, Total = tuple.Item2, Code = 1 };
        }
        /// <summary>
        /// 根据条件获取一条数据
        /// </summary>
        /// <param name="queryModel"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<EmployeeDTO> GetEmployeeByPropAsync(EmployeeQueryModel queryModel, CancellationToken cancellationToken = default)
        {
            var data = _repository.GetAllAsync();
            var tuple = await CommonWhereBuilder.WhereBuilder(data, queryModel, cancellationToken);
            var obj = await tuple.Item1
                .Select(x => new EmployeeDTO
                {
                    Id = x.Id,
                    DepartmentId = x.DepartmentId,
                    DepartmentName = x.Department.DepartmentName,
                    Age = x.Age,
                    Cellphone = x.Cellphone,
                    LoginPermissions = (int)x.LoginPermissions,
                    LoginPermissionsStr = x.LoginPermissions.ToString(),
                    Sex = (int)x.Sex,
                    SexStr = x.Sex.ToString(),
                    PhotoPath = x.PhotoPath,
                    CreateTime = x.CreateTime,
                    RealName = x.RealName,
                    Remark = x.Remark,
                    RoleId = x.RoleId,
                    RoleName = x.Role.RoleName,
                    Status = (int)x.Status,
                    StatusStr = x.Status.ToString(),
                    UserName = x.UserName,
                    ShiftId = x.ShiftId,
                    ShiftName = x.ShiftInfo.ShiftName,
                    Permissions = x.Role.Permissions,
                    MobilePermissions = x.Role.MobilePermissions
                }).FirstOrDefaultAsync(cancellationToken);
            if (obj == null)
            {
                return null;
            }
            return obj;
        }
        /// <summary>
        /// 根据id获取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<EmployeeDTO> GetEmployeeByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            var obj = await _repository.GetAllAsync().Where(x => x.Id == id)
             .Select(x => new EmployeeDTO
             {
                 Id = x.Id,
                 DepartmentId = x.DepartmentId,
                 DepartmentName = x.Department.DepartmentName,
                 Age = x.Age,
                 Cellphone = x.Cellphone,
                 LoginPermissions = (int)x.LoginPermissions,
                 LoginPermissionsStr = x.LoginPermissions.ToString(),
                 Sex = (int)x.Sex,
                 SexStr = x.Sex.ToString(),
                 PhotoPath = x.PhotoPath,
                 CreateTime = x.CreateTime,
                 RealName = x.RealName,
                 Remark = x.Remark,
                 RoleId = x.RoleId,
                 RoleName = x.Role.RoleName,
                 Status = (int)x.Status,
                 StatusStr = x.Status.ToString(),
                 UserName = x.UserName,
                 ShiftId = x.ShiftId,
                 ShiftName = x.ShiftInfo.ShiftName,
                 Permissions = x.Role.Permissions,
                 MobilePermissions = x.Role.MobilePermissions
             }).FirstOrDefaultAsync(cancellationToken);
            if (obj == null)
            {
                return null;
            }
            return obj;
        }
        /// <summary>
        /// 根据条件查看是否有符合条件的数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> EmployeeAnyAsync(EmployeeQueryModel queryModel, CancellationToken cancellationToken = default)
        {
            var exp = await CommonWhereBuilder.WhereBuilderToExp<LanTianEmployee, EmployeeQueryModel>(queryModel, cancellationToken);
            return await _repository.AnyAsync(exp, cancellationToken);
        }
    }
}
