using System.Reflection;
using webAPITemplete.Repository.Dapper.interfaces;
using webAPITemplete.Repository.Dapper.services;

namespace webAPITemplete.Helpers
{
    public static class DependencyRegister
    {
        /// <summary>
        /// 將Entities資料夾中所有的Eitity Model輪流做DI註冊
        /// </summary>
        /// <param name="services"></param>
        public static void Register(IServiceCollection services)
        {
            var types = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.Namespace == "webAPITemplete.Models.DTOs");
            foreach (var type in types)
            {
                var interfaceType = typeof(IBaseDapper<>).MakeGenericType(type);
                var serviceType = typeof(BaseDapper<>).MakeGenericType(type);
                services.AddScoped(interfaceType, serviceType);
            }
        }
    }
}
