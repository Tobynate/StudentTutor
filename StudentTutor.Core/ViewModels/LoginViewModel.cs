using Microsoft.Extensions.Configuration;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using StudentTutor.Core.Helpers.Interfaces;
using StudentTutor.Core.Models;
using StudentTutor.Core.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace StudentTutor.Core.ViewModels
{
    public class LoginViewModel : MvxViewModel
    {
        private readonly IApiHelper _apiHelper;
        private readonly IMvxNavigationService _mvxNavigation;

        public LoginViewModel( IApiHelper apiHelper, IMvxNavigationService mvxNavigation)
        {
            _apiHelper = apiHelper;
            _mvxNavigation = mvxNavigation;
            FixHeight();
            Username = "nathanieltoby99@Gmail.com";
            Submit = new MvxAsyncCommand(SubmitCommand);
            SubmitEnabled = CanSubmitCommand();
            SignUp = new MvxAsyncCommand(SignUpCommand);
        }

        private string _username;

        public string Username
        {
            get { return _username; }
            set 
            {
                SetProperty(ref _username, value);
                SubmitEnabled = CanSubmitCommand();
            }
        }

        private SecureString _securePassword;

        public SecureString SecurePassword
        {
            get { return _securePassword; }
            set
            {
                SetProperty(ref _securePassword, value);
                SubmitEnabled = CanSubmitCommand();
            }
        }

        private bool _vMProperty;

        public bool VMProperty
        {
            get { return _vMProperty; }
            set 
            {
                SetProperty(ref _vMProperty, value);

                FixHeight();
            }
        }
        private void FixHeight()
        {
            if (!VMProperty)
            {
                ErrorViewerHeight = 0;
            }
            else
            {
                ErrorViewerHeight = null;
            }
        }

        private int? _errorViewerHeight;

        public int? ErrorViewerHeight
        {
            get { return _errorViewerHeight;; }
            set 
            {
                SetProperty(ref _errorViewerHeight, value);
            }
        }


        public async Task SubmitCommand()
        {
            ErrorMessage = "";
            try
            {
                var result = await _apiHelper.Authenticate(Username, SecurePassword);
                
                byte returnValue = await _apiHelper.AddDefaultRole(result.Access_Token);

                await _apiHelper.GetLoggedInUserData(result.Access_Token);

                await _mvxNavigation.Navigate<CreateTutorialViewModel>();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            if (!string.IsNullOrWhiteSpace(ErrorMessage))
            {
                VMProperty = true;
            }
            else
            {
                VMProperty = false;
            }
        }

        public async Task SignUpCommand()
        {
            await _mvxNavigation.Navigate<RegisterUserViewModel>();
        }

        private bool _submitEnabled;

        public bool SubmitEnabled
        {
            get { return _submitEnabled; }
            set 
            {
                SetProperty(ref _submitEnabled, value);
            }
        }

        public bool CanSubmitCommand()
        {
            bool output = false;
            if (SecurePassword?.Length > 5 && Username?.Length > 5)
            {
                output = true;
            }
            return output;
        }
        public IMvxCommand Submit
        {
            get; set;
        }

        public IMvxCommand SignUp { get; set; }

        private string _errorMessage;

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set 
            {
                SetProperty(ref _errorMessage, value); 
            }
        }

    }
}
