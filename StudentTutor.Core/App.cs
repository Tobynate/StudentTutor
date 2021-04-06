using Caliburn.Micro;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.ViewModels;
using StudentTutor.Core.Helpers;
using StudentTutor.Core.Helpers.Interfaces;
using StudentTutor.Core.Models;
using StudentTutor.Core.Models.Interfaces;
using StudentTutor.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace StudentTutor.Core
{
    public class App: MvxApplication
    {
        //[Obsolete]
        public override void Initialize()
        {
            RegisterAppStart<LoginViewModel>();

           // Mvx.IoCProvider.RegisterType<ILoginInputModel, LoginInputModel>();
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IApiHelper, ApiHelper>();
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<ILoggedInUserModel, LoggedInUserModel>();

           // Mvx.IoCProvider.IoCConstruct(typeof(IConfiguration), "IConfiguration", AddConfiguration());
           // Mvx.IoCConstruct<IConfiguration>(AddConfiguration());
            //Mvx.IoCProvider.RegisterType<IConfiguration>(AddConfiguration());
            //Mvx.IoCProvider.RegisterType(typeof(IConfiguration), AddConfigurationClass)
            //CreatableTypes().EndingWith("Services").AsInterfaces().RegisterAsLazySingleton
        }
//        private IConfiguration AddConfiguration()
//        {
//            DirectoryInfo path = Directory.GetParent(Directory.GetCurrentDirectory());
//            string p = path.FullName + "\\netcoreapp3.1\\AppSettings";
//            IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetParent(Directory.GetCurrentDirectory()).FullName + "\\netcoreapp3.1\\AppSettings").AddJsonFile("appsettings.json");
//#if DEBUG
//            builder.AddJsonFile("appsettings.development.json", optional: true, reloadOnChange: true);
//#else
//            builder.AddJsonFile("appsettings.production.json", optional: true, reloadOnChange: true);
//#endif
//            return builder.Build();
//        }

    }    
//    public class bootstrapper:BootstrapperBase
//    {
//        private SimpleContainer _container = new SimpleContainer();
//        public bootstrapper()
//        {
//            Initialize();
//        }
//        protected override void Configure()
//        {
//            _container.Instance(_container);

//          //  _container.RegisterInstance(typeof(IConfiguration), "IConfiguration", AddConfiguration());
//        }
//        protected override object GetInstance(Type service, string key)
//        {
//            return _container.GetInstance(service, key);
//        }
//        protected override IEnumerable<object> GetAllInstances(Type service)
//        {
//            return _container.GetAllInstances(service);
//        }
//        protected override void BuildUp(object instance)
//        {
//            _container.BuildUp(instance);
//        }
//        private IConfiguration AddConfiguration()
//        {
//            IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetParent(Directory.GetCurrentDirectory()).FullName + "\\StudentTutor.Wpf\\AppSettings").AddJsonFile("appsettings.json");
//#if DEBUG
//            builder.AddJsonFile("appsettings.development.json", optional: true, reloadOnChange: true);
//#else
//            builder.AddJsonFile("appsettings.production.json", optional: true, reloadOnChange: true);
//#endif
//            return builder.Build();
//        }
//    }
}
