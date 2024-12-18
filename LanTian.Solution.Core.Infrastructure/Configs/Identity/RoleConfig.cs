

namespace LanTian.Solution.Core.Infrastructure.Configs.Identity
{
    internal class RoleConfig : IEntityTypeConfiguration<LanTianRole>
    {
        public void Configure(EntityTypeBuilder<LanTianRole> builder)
        {
            builder.ToTable("lan_tian_role");
            builder.Property(x => x.Id).HasColumnName("id").UseIdentityColumn().HasComment("id主键");
            builder.Property(x => x.RoleName).HasColumnName("role_name").HasMaxLength(255).HasComment("角色（职位）名称");
            builder.Property(x => x.Remark).HasColumnName("remark").HasComment("备注").HasMaxLength(255);
            builder.Property(x => x.CreateTime).HasColumnName("create_time").HasColumnType("timestamp").HasComment("创建时间");
            builder.Property(x => x.Permissions).HasColumnName("permissions").HasComment("权限项");
            builder.Property(x => x.MobilePermissions).HasColumnName("mobile_permissions").HasComment("移动端权限项");
            builder.Property(x => x.IsDeleted).HasColumnName("is_deleted").HasComment("软删  1-未删除 2-已删除");
        }
    }
}
