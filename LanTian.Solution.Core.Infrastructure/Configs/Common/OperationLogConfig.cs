

namespace LanTian.Solution.Core.Infrastructure.Configs.Common
{
    internal class OperationLogConfig : IEntityTypeConfiguration<LanTianOperationLog>
    {
        public void Configure(EntityTypeBuilder<LanTianOperationLog> builder)
        {
            builder.ToTable("lan_tian_operation_log");
            builder.Property(x => x.Id).HasColumnName("id").UseIdentityColumn().HasComment("id主键");
            builder.Property(x => x.CreateTime).HasColumnName("create_time").HasColumnType("timestamp").HasComment("创建时间");
            builder.Property(x => x.IsDeleted).HasColumnName("is_deleted").HasComment("软删  1-未删除 2-已删除");
            builder.Property(x => x.EmpId).HasColumnName("employee_id").HasComment("员工id");
            builder.Property(x => x.EmpName).HasColumnName("employee_name").HasMaxLength(255).HasComment("员工姓名");
            builder.Property(x => x.OperationName).HasColumnName("operation_name").HasMaxLength(255).HasComment("操作项目的名称");
            builder.Property(x => x.ApiPath).HasColumnName("api_path").HasMaxLength(255).HasComment("接口地址");
            builder.Property(x => x.RequestMessage).HasColumnName("request_message").HasComment("请求参数").IsRequired(false);
            builder.Property(x => x.ResponseMessage).HasColumnName("response_message").HasComment("响应参数").IsRequired(false);
        }
    }
}
