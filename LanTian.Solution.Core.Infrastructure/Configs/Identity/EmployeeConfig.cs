

namespace LanTian.Solution.Core.Infrastructure.Configs.Identity;

internal class EmployeeConfig : IEntityTypeConfiguration<LanTianEmployee>
{
    public void Configure(EntityTypeBuilder<LanTianEmployee> builder)
    {
        builder.ToTable("lan_tian_employee");
        builder.Property(x => x.Id).HasColumnName("id").UseIdentityColumn().HasComment("id主键");
        builder.Property(x => x.RealName).HasColumnName("real_name").HasMaxLength(255).HasComment("真实姓名");
        builder.Property(x => x.UserName).HasColumnName("user_name").HasColumnType("uuid").HasComment("用户名");
        builder.Property(x => x.Cellphone).HasColumnName("cellphone").HasMaxLength(255).HasComment("手机号");
        builder.Property(x => x.CreateTime).HasColumnName("create_time").HasColumnType("timestamp").HasComment("创建时间");
        builder.Property("passwordHash").IsRequired(false).HasComment("密码").HasMaxLength(255);
        builder.Property("passwordSalt").IsRequired(false).HasComment("盐").HasMaxLength(255);
        builder.Property(x => x.Remark).HasColumnName("remark").HasMaxLength(255).HasComment("备注");
        builder.Property(x => x.RoleId).HasColumnName("role_id").HasComment("角色（职位）id").IsRequired(false);
        builder.Property(x => x.ShiftId).HasColumnName("shift_id").HasComment("班次id").IsRequired(false);
        builder.Property(x => x.Sex).HasColumnName("sex").HasComment(" 性别 1-男 2-女");
        builder.Property(x => x.LoginPermissions).HasColumnName("login_permissions").HasComment("登录权限 1-无权限 2-web权限  3-app权限 4-小程序权限  5-所有权限");
        builder.Property(x => x.DepartmentId).HasColumnName("department_id").HasComment("部门id").IsRequired(false); ;
        builder.Property(x => x.Status).HasColumnName("status").HasComment("员工状态 1-正常 2-离职");
        builder.Property(x => x.Age).HasColumnName("age").HasComment("年龄");
        builder.Property(x => x.PhotoPath).HasColumnName("photoPath").HasMaxLength(255).HasComment("照片地址");
        builder.Property(x => x.IsDeleted).HasColumnName("is_deleted").HasComment("软删  1-未删除 2-已删除");
        //一对多配置
        builder.HasOne(x => x.Role).WithMany(x => x.Employees).IsRequired(false).HasForeignKey(x => x.RoleId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.Department).WithMany(x => x.Employees).IsRequired(false).HasForeignKey(x => x.DepartmentId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.ShiftInfo).WithMany().HasForeignKey(x => x.ShiftId);
    }
}
