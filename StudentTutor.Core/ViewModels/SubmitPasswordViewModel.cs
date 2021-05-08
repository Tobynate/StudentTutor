using MvvmCross.Commands;
using MvvmCross.ViewModels;
using StudentTutor.Core.Helpers.Interfaces;
using StudentTutor.Core.Models;
using StudentTutor.Core.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace StudentTutor.Core.ViewModels
{
    public class SubmitPasswordViewModel : MvxViewModel
    {
        private readonly IUserRegistrationTrModel _registrationTrModel;
        private readonly IApiHelper _apiHelper;

        public SubmitPasswordViewModel(IUserRegistrationTrModel registrationTrModel, IApiHelper apiHelper)
        {
            this._registrationTrModel = registrationTrModel;
            this._apiHelper = apiHelper;
            Passport = _registrationTrModel.Passport;
            SubjectOfInterest = _registrationTrModel.SubjectOfInterest;
        }

        private BitmapSource _passport;

        public BitmapSource Passport
        {
            get { return _passport; }
            set 
            {
                SetProperty(ref _passport, value); 
            }
        }

        private BindingList<SubjectOfInterestModel> _subjectOfInterest;

        public BindingList<SubjectOfInterestModel> SubjectOfInterest
        {
            get { return _subjectOfInterest; }
            set 
            {
                SetProperty(ref _subjectOfInterest, value); 
            }
        }

        public SecureString SecurePassword { private get; set; }
        public SecureString ConfirmPassword { private get; set; }

        public IMvxAsyncCommand Submit => new MvxAsyncCommand(SubmitCommand);

        private async Task SubmitCommand()
        {
            await _apiHelper.RegisterUser(_registrationTrModel, SecurePassword, ConfirmPassword);   
        }
    }
}
