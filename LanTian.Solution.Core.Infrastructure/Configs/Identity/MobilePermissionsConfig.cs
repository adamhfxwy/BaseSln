

namespace LanTian.Solution.Core.Infrastructure.Configs.Identity
{
    internal class MobilePermissionsConfig : IEntityTypeConfiguration<LanTianMobilePermissions>
    {
        public void Configure(EntityTypeBuilder<LanTianMobilePermissions> builder)
        {
            builder.ToTable("lan_tian_mobile_permissions");
            builder.Property(x => x.Id).HasColumnName("id").UseIdentityColumn().HasComment("id主键");
            builder.Property(x => x.PermissionName).HasColumnName("permission_name").HasMaxLength(255).HasComment("权限项名称");
            builder.Property(x => x.Remark).HasColumnName("remark").HasComment("备注").HasMaxLength(255);
            builder.Property(x => x.ParentId).HasColumnName("parent_id").HasComment("父级id");
            builder.Property(x => x.PermissionCode).HasColumnName("permission_code").HasComment("权限项编码").HasMaxLength(255);
            builder.Property(x => x.CreateTime).HasColumnName("create_time").HasColumnType("timestamp").HasComment("创建时间");
            builder.Property(x => x.IsDeleted).HasColumnName("is_deleted").HasComment("软删  1-未删除 2-已删除");
        }
    }
}
