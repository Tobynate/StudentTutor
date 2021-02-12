using MvvmCross.ViewModels;
using StudentTutor.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentTutor.Core
{
    public class App: MvxApplication
    {
        public override void Initialize()
        {
            RegisterAppStart<CreateTutorialViewModel>();
        }
    }
}
