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
    public class App : MvxApplication
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
}

