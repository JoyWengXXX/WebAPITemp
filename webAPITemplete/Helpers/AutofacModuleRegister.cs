using Autofac;
using System.Reflection;

namespace WebAPITemplete.Helpers
{
    public class AutofacModuleRegister : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //RegisterAssemblyTypes => 註冊所有集合
            //Where(t => t.Name.EndsWith("Service")) => 找出所有Service結尾的檔案
            //AsImplementedInterfaces => 找到Service後註冊到其所繼承的介面 
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                  .Where(t => t.Namespace == "webAPITemplete.Services" && t.Name.EndsWith("Service") || t.Name == "IProjectDBContext")
                  .AsImplementedInterfaces();
        }
    }
}
