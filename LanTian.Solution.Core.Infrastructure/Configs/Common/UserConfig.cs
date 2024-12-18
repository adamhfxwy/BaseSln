

namespace LanTian.Solution.Core.Infrastructure.Configs.Common
{
    internal class UserConfig : IEntityTypeConfiguration<LanTianUserInfo>
    {
        public void Configure(EntityTypeBuilder<LanTianUserInfo> builder)
        {
            builder.ToTable("lan_tian_user_info");
            builder.Property(x => x.Id).HasColumnName("id").UseIdentityColumn().HasComment("id主键");
            builder.Property(x => x.CreateTime).HasColumnName("create_time").HasColumnType("timestamp").HasComment("创建时间");
            builder.Property(x => x.IsDeleted).HasColumnName("is_deleted").HasComment("软删  1-未删除 2-已删除");
            builder.Property(x => x.Name).HasColumnName("name").HasMaxLength(255).HasComment("用户名称");
            builder.Property(x => x.Cellphone).HasColumnName("cellphone").HasMaxLength(255).HasComment("联系方式");
            builder.Property(x => x.Address).HasColumnName("address").HasMaxLength(255).HasComment("地址");
        }
    }
}
