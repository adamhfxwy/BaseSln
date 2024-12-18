using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanTian.Solution.Core.Infrastructure.Configs.Common
{
    internal class ShiftInfoConfig : IEntityTypeConfiguration<LanTianShiftInfo>
    {
        public void Configure(EntityTypeBuilder<LanTianShiftInfo> builder)
        {
            builder.ToTable("lan_tian_shift_info");
            builder.Property(x => x.Id).HasColumnName("id").UseIdentityColumn().HasComment("id主键");
            builder.Property(x => x.CreateTime).HasColumnName("create_time").HasColumnType("timestamp").HasComment("创建时间");
            builder.Property(x => x.IsDeleted).HasColumnName("is_deleted").HasComment("软删  1-未删除 2-已删除");
            builder.Property(x => x.ShiftName).HasColumnName("shift_name").HasMaxLength(255).HasComment("班次名称");
            builder.Property(x => x.Remark).HasColumnName("remark").HasComment("备注");
            builder.Property(x => x.BeginTime).HasConversion(
                  v => v.ToTimeSpan(),
                  v => TimeOnly.FromTimeSpan(v)
              )
              .HasColumnType("time").HasColumnName("begin_time").HasComment("开始时间");
            builder.Property(x => x.EndTime).HasConversion(
                   v => v.ToTimeSpan(),
                   v => TimeOnly.FromTimeSpan(v)
               )
               .HasColumnType("time").HasColumnName("end_time").HasComment("结束时间");

        }
    }
}
