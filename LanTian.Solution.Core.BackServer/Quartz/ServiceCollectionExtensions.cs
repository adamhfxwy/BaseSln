
namespace LanTian.Solution.Core.BackServer.Quartz
{
    public static class ServiceCollectionExtensions
    {
        public static void AddJob(this IServiceCollection services,IConfiguration config)
        {
            //时间段计费模板状态变更
            //string changeChargingTemplateStatusConf = config["Quartz:ChangeChargingTemplateStatus"];         
            //if (!string.IsNullOrEmpty(changeChargingTemplateStatusConf))
            //{
            //    services.AddSingleton(new JobSchedule(
            //   jobType: typeof(ChangeChargingTemplateStatusJob), cronExpression: changeChargingTemplateStatusConf)); //每天零晨2点执行一次
            //}

            
        }
    }
}
