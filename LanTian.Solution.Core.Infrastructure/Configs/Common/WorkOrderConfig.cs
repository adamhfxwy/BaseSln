using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanTian.Solution.Core.Infrastructure.Configs.Common
{
    internal class WorkOrderConfig : IEntityTypeConfiguration<LanTianWorkOrder>
    {
        public void Configure(EntityTypeBuilder<LanTianWorkOrder> builder)
        {
            builder.ToTable("lan_tian_work_order");
            builder.Property(x => x.Id).HasColumnName("id").UseIdentityColumn().HasComment("id主键");
            builder.Property(x => x.CreateTime).HasColumnName("create_time").HasColumnType("timestamp").HasComment("创建时间");
            builder.Property(x => x.IsDeleted).HasColumnName("is_deleted").HasComment("软删  1-未删除 2-已删除");
            builder.Property(x => x.OrderNumber).HasColumnName("order_number").HasMaxLength(255).HasComment("工单号");
            builder.Property(x => x.OrderType).HasColumnName("order_type").HasComment("工单类型  1-维修类、2-安全隐患、3-卫生类");
            builder.Property(x => x.ReportingChannels).HasColumnName("reporting_channels").HasComment("上报途径  1-巡检上报、2-用户上报、3-设备上报");
            builder.Property(x => x.ProblemDescription).HasColumnName("problem_description").HasComment("问题描述").IsRequired(false);
            builder.Property(x => x.ProblemPicPath).HasColumnName("problem_pic_path").HasComment("问题照片").IsRequired(false);
            builder.Property(x => x.Status).HasColumnName("status").HasComment("状态 1-未处理 2-已处理");
            builder.Property(x => x.ReportPersonId).HasColumnName("report_person_id").HasComment("上报人id");
            builder.Property(x => x.ReportPersonName).HasColumnName("report_person_name").HasMaxLength(255).HasComment("上报人姓名");
            builder.Property(x => x.HandlePersonId).HasColumnName("handle_person_id").HasComment("处理人id").IsRequired(false);
            builder.Property(x => x.HandlePersonName).HasColumnName("handle_person_name").HasMaxLength(255).HasComment("处理人姓名").IsRequired(false);
            builder.Property(x => x.DepartmentId).HasColumnName("department_id").HasComment("问题所属部门id").IsRequired(false);
            builder.Property(x => x.DepartmentName).HasColumnName("department_name").HasComment("问题所属部门name").IsRequired(false);
            builder.Property(x => x.HandledDescription).HasColumnName("handled_description").HasComment("问题解决描述").IsRequired(false);
            builder.Property(x => x.HandledPicPath).HasColumnName("handled_pic_path").HasComment("处理后图片").IsRequired(false);
            builder.Property(x => x.HandledTime).HasColumnName("handled_time").HasColumnType("timestamp").HasComment("处理时间").IsRequired(false);
        }
    }
}
