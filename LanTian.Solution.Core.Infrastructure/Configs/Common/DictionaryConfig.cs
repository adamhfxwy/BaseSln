

namespace LanTian.Solution.Core.Infrastructure.Configs.Common
{
    internal class DictionaryConfig : IEntityTypeConfiguration<LanTianDictionary>
    {
        public void Configure(EntityTypeBuilder<LanTianDictionary> builder)
        {
            builder.ToTable("lan_tian_dictionary");
            builder.Property(x => x.Id).HasColumnName("id").UseIdentityColumn().HasComment("id主键");
            builder.Property(x => x.CreateTime).HasColumnName("create_time").HasColumnType("timestamp").HasComment("创建时间");
            builder.Property(x => x.IsDeleted).HasColumnName("is_deleted").HasComment("软删  1-未删除 2-已删除");
            builder.Property(x => x.Key).HasColumnName("key").HasMaxLength(255).HasComment("键");
            builder.Property(x => x.Value).HasColumnName("value").HasMaxLength(255).HasComment("值");
            builder.Property(x => x.Description).HasColumnName("description").HasMaxLength(255).HasComment("描述");
            builder.Property(x => x.Type).HasColumnName("type").HasComment("类型  0-创建类型");
        }
    }
}
