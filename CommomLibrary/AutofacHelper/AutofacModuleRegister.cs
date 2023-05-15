using Autofac;
using System.Reflection;

namespace CommomLibrary.AutofacHelper
{
    public class AutofacModuleRegister : Autofac.Module
    {
        private readonly string _nameSpace;
        private readonly Assembly _assembly;

        public AutofacModuleRegister(string nameSpace, Assembly assembly)
        {
            _nameSpace = nameSpace;
            _assembly = assembly;
        }

        protected override void Load(ContainerBuilder builder)
        {
            //RegisterAssemblyTypes => 註冊所有集合
            //Where(t => t.Name.EndsWith("Service")) => 找出所有Service結尾的檔案
            //AsImplementedInterfaces => 找到Service後註冊到其所繼承的介面 
            builder.RegisterAssemblyTypes(_assembly)
                  .Where(t => t.Namespace == _nameSpace && t.Name.EndsWith("Service"))
                  .AsImplementedInterfaces();
        }
    }
}
