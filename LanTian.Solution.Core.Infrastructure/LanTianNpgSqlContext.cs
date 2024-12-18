

namespace LanTian.Solution.Core.Infrastructure
{
    public class LanTianNpgSqlContext:DbContext
    {
        #region 实体
        /// <summary>
        /// 员工上下文
        /// </summary>
        public virtual DbSet<LanTianEmployee>  LanTianEmployees { get; set; } = null!;
        /// <summary>
        /// 部门上下文
        /// </summary>
        public virtual DbSet<LanTianDepartment>  LanTianDepartments { get; set; } = null!;
        /// <summary>
        /// 角色上下文
        /// </summary>
        public virtual DbSet<LanTianRole>  LanTianRoles { get; set; } = null!;
        /// <summary>
        /// 菜单上下文
        /// </summary>
        public virtual DbSet<LanTianMenu>  LanTianMenus { get; set; } = null!;
        #endregion
        public LanTianNpgSqlContext(DbContextOptions<LanTianNpgSqlContext> options)
        : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //string connStr = "Host=127.0.0.1;Database=lantian_server_core;Username=postgres;Password=root";
            //optionsBuilder.UseNpgsql(connStr);
            //optionsBuilder.LogTo(Console.WriteLine);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}
