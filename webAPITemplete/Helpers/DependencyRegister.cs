using System.Reflection;
using webAPITemplete.Repository.Dapper.DbContexts;
using webAPITemplete.Repository.Dapper.interfaces;
using webAPITemplete.Repository.Dapper.services;

namespace webAPITemplete.Helpers
{
    public static class DependencyRegister
    {
        /// <summary>
        /// 將Entities資料夾中所有的Eitity Model輪流做DI註冊
        /// 針對Default資料庫的Entity做註冊
        /// </summary>
        /// <param name="services"></param>
        public static void DefaultDBRegister(IServiceCollection services)
        {
            var types = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.Namespace == "webAPITemplete.Models.DTOs.DefaultDB"); //讀取DefaultDB資料夾中的所有Model
            foreach (var type in types)
            {
                var interfaceType = typeof(IBaseDapper<,>).MakeGenericType(new Type[] { type, typeof(ProjectDBContext_Default) });
                var serviceType = typeof(BaseDapper<,>).MakeGenericType(new Type[] { type, typeof(ProjectDBContext_Default) });
                services.AddScoped(interfaceType, serviceType);
            }
        }

        /// <summary>
        /// 將Entities資料夾中所有的Eitity Model輪流做DI註冊
        /// 針對Test1資料庫的Entity做註冊
        /// </summary>
        /// <param name="services"></param>
        public static void Test1DBRegister(IServiceCollection services)
        {
            var types = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.Namespace == "webAPITemplete.Models.DTOs.TestDB1"); //讀取TestDB1資料夾中的所有Model
            foreach (var type in types)
            {
                var interfaceType = typeof(IBaseDapper<,>).MakeGenericType(new Type[] { type, typeof(ProjectDBContext_Test1) });
                var serviceType = typeof(BaseDapper<,>).MakeGenericType(new Type[] { type, typeof(ProjectDBContext_Test1) });
                services.AddScoped(interfaceType, serviceType);
            }
        }
    }
}
