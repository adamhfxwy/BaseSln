

namespace LanTian.Solution.Core.Infrastructure.Configs.Identity
{
    internal class MenuConfig : IEntityTypeConfiguration<LanTianMenu>
    {
        public void Configure(EntityTypeBuilder<LanTianMenu> builder)
        {
            builder.ToTable("lan_tian_menu");
            builder.Property(x => x.Id).HasColumnName("id").UseIdentityColumn().HasComment("id主键");
            builder.Property(x => x.Path).HasColumnName("path").HasMaxLength(255).HasComment("路由").IsRequired(false);
            builder.Property(x => x.Name).HasColumnName("name").HasMaxLength(255).HasComment("菜单名称");
            builder.Property(x => x.ButtonName).HasColumnName("button_name").HasMaxLength(255).HasComment("按钮名称");
            builder.Property(x => x.MenuName).HasColumnName("menu_name").HasMaxLength(255).HasComment("菜单名称");
            builder.Property(x => x.CreateTime).HasColumnName("create_time").HasColumnType("timestamp").HasComment("创建时间");
            builder.Property(x => x.ParentId).HasColumnName("parent_id").HasComment("父级id").IsRequired(false);
            builder.Property(x => x.Description).HasColumnName("description").HasMaxLength(255).HasComment("描述").IsRequired(false);
            builder.Property(x => x.Component).HasColumnName("component").HasMaxLength(255).HasComment("组件").IsRequired(false);
            builder.Property(x => x.Icon).HasColumnName("icon").HasComment("图标").IsRequired(false);
            builder.Property(x => x.IsButton).HasColumnName("is_button").HasComment("是否是按钮 1-否 2-是");
            builder.Property(x => x.Level).HasColumnName("level").HasComment("层级");
            builder.Property(x => x.MenuCode).HasColumnName("menu_code").HasMaxLength(255).HasComment("菜单或按钮编码").IsRequired(false);
            builder.Property(x => x.IsDeleted).HasColumnName("is_deleted").HasComment("软删  1-未删除 2-已删除");
        }
    }
}
