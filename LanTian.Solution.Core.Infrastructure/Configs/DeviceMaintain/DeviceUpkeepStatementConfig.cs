using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanTian.Solution.Core.Infrastructure.Configs.DeviceMaintain
{
    internal class DeviceUpkeepStatementConfig : IEntityTypeConfiguration<LanTianDeviceUpkeepStatement>
    {
        public void Configure(EntityTypeBuilder<LanTianDeviceUpkeepStatement> builder)
        {
            builder.ToTable("lan_tian_device_upkeep_statement");
            builder.Property(x => x.Id).HasColumnName("id").UseIdentityColumn().HasComment("id主键");
            builder.Property(x => x.CreateTime).HasColumnName("create_time").HasColumnType("timestamp").HasComment("创建时间");
            builder.Property(x => x.IsDeleted).HasColumnName("is_deleted").HasComment("软删  1-未删除 2-已删除");
            builder.Property(x => x.DeviceNumber).HasColumnName("device_number").HasMaxLength(255).HasComment("设备编码");
            builder.Property(x => x.DeviceType).HasColumnName("device_type").HasMaxLength(255).HasComment("设备类型");
            builder.Property(x => x.UpkeepCycle).HasColumnName("upkeep_cycle").HasComment("保养周期（月）");
            builder.Property(x => x.Description).HasColumnName("description").HasComment("保养描述");
            builder.Property(x => x.EmployeeId).HasColumnName("employee_id").HasComment("维护人id");
            builder.Property(x => x.EmployeeName).HasColumnName("employee_name").HasMaxLength(255).HasComment("维护人姓名");
            builder.Property(x => x.ThisUpkeepTime).HasColumnName("this_upkeep_time").HasColumnType("timestamp").HasComment("本次应保养时间");
            builder.Property(x => x.RealityUpkeepTime).HasColumnName("reality_upkeep_time").HasColumnType("timestamp").HasComment("实际保养时间");
            builder.Property(x => x.IsTimeout).HasColumnName("is_timeout").HasComment("是否超时保养：1-否 2-是");
            builder.Property(x => x.GenerateCosts).HasColumnName("generate_costs").HasPrecision(10, 2).HasComment("产生的费用");
        }
    }
}
