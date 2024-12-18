using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanTian.Solution.Core.Infrastructure.Configs.DeviceMaintain
{
    internal class DeviceUpkeepConfig : IEntityTypeConfiguration<LanTianDeviceUpkeep>
    {
        public void Configure(EntityTypeBuilder<LanTianDeviceUpkeep> builder)
        {
            builder.ToTable("lan_tian_device_upkeep");
            builder.Property(x => x.Id).HasColumnName("id").UseIdentityColumn().HasComment("id主键");
            builder.Property(x => x.CreateTime).HasColumnName("create_time").HasColumnType("timestamp").HasComment("创建时间");
            builder.Property(x => x.IsDeleted).HasColumnName("is_deleted").HasComment("软删  1-未删除 2-已删除");
            builder.Property(x => x.DeviceNumber).HasColumnName("device_number").HasMaxLength(255).HasComment("设备编码");
            builder.Property(x => x.DeviceType).HasColumnName("device_type").HasMaxLength(255).HasComment("设备类型");
            builder.Property(x => x.UpkeepCycle).HasColumnName("upkeep_cycle").HasComment("保养周期（月）");
            builder.Property(x => x.UpkeepNextDate).HasColumnName("upkeep_next_date").HasColumnType("timestamp").HasComment("下次保养时间");
            builder.Property(x => x.UpkeepLastDate).HasColumnName("upkeep_last_date").HasColumnType("timestamp").HasComment("上次保养时间");
        }
    }
}
