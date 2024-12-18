

using LanTian.Solution.Core.EnumAndConstent.SubEntitys;
using Newtonsoft.Json;

namespace LanTian.Solution.Core.Infrastructure.Configs.Identity;

internal class DepartmentConfig : IEntityTypeConfiguration<LanTianDepartment>
{
    public void Configure(EntityTypeBuilder<LanTianDepartment> builder)
    {
        builder.ToTable("lan_tian_department");
        builder.Property(x => x.Id).HasColumnName("id").UseIdentityColumn().HasComment("id主键");
        builder.Property(x => x.IsDeleted).HasColumnName("is_deleted").HasComment("软删  1-未删除 2-已删除");
        builder.Property(x => x.DepartmentName).HasColumnName("department_name").HasMaxLength(255).HasComment("部门名称");
        builder.Property(x => x.Remark).HasColumnName("remark").HasMaxLength(255).HasComment("备注");
        builder.Property(x => x.CreateTime).HasColumnName("create_time").HasColumnType("timestamp").HasComment("创建时间");
        builder.Property(x => x.DepartmentLeader).HasColumnType("jsonb")
            .HasConversion(
            v => JsonConvert.SerializeObject(v),
             v => JsonConvert.DeserializeObject<DepartmentLeaderEntity>(v))
            .HasColumnName("department_leader").HasComment("部门负责人");
    }
}
