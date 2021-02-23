using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.ViewModels;
using StudentTutor.Core.Helpers;
using StudentTutor.Core.Models;
using StudentTutor.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudentTutor.Core
{
    public class App: MvxApplication
    {
        public override void Initialize()
        {
            RegisterAppStart<LoginViewModel>();

           // Mvx.IoCProvider.RegisterType<ILoginInputModel, LoginInputModel>();
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IApiHelper, ApiHelper>();

            //CreatableTypes().EndingWith("Services").AsInterfaces().RegisterAsLazySingleton
        }
    }
}
