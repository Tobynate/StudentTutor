using MvvmCross.Commands;
using MvvmCross.ViewModels;
using StudentTutor.Core.Helpers;
using StudentTutor.Core.Models.Interfaces;
using StudentTutor.Core.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace StudentTutor.Core.ViewModels
{
    public class CreateTutorialViewModel: MvxViewModel
    {
        private string _username;
        private readonly ILoggedInUserModel _loggedInUser;
        public CreateTutorialViewModel(ILoggedInUserModel loggedInUser)
        {
            _loggedInUser = loggedInUser;

            Username = _loggedInUser.LastName + " " + _loggedInUser.FirstName;
            EmailAddress = _loggedInUser.EmailAddress;

            DisplayImage = getImage(_loggedInUser.Passport, DisplayImage);

            if (DisplayImage == null)
            {
                DefaultPassportIcon();
            }

            LogOut = new MvxAsyncCommand(LogOutCommand);

        }

        private async Task LogOutCommand()
        {
            //TODO Logout Logic
        }

        private BitmapSource getImage(byte[] source, BitmapSource output)
        {
            try
            {
               return ImageConverter.ConvertByteToImg(source); 
            }
            catch (Exception ex)
            {
                var errorMsg = ex.Message;
            }
            return output;
        }
        private void DefaultPassportIcon()
          {
            Random random = new Random();
            
            int pick= random.Next(-5, 4);

            if (pick >= 0)
            {
                DisplayImage = getImage(Resources.Female_Icon, DisplayImage);
            }
            else
            {
                DisplayImage = getImage(Resources.Male_Icon, DisplayImage);
            }
        }
        private BitmapSource _displayImage;

        public BitmapSource DisplayImage
        {
            get { return _displayImage; }
            set 
            {
                SetProperty(ref _displayImage, value); 
            }
        }

        //public override void Prepare()
        //{

        //}
        //public override void Prepare(ILoggedInUserModel loggedInUser)
        //{
        //    _loggedInUser = loggedInUser;

        //   // Username = _loggedInUser.EmailAddress;
        //}

        public string Username
        {
            get { return _username; }
            set 
            {
                SetProperty(ref _username, value);
            }
        }
        private string _emailAddress;

        public string EmailAddress
        {
            get { return _emailAddress; }
            set 
            {
                SetProperty(ref _emailAddress, value); 
            }
        }

        private string _topic;

        public string Topic
        {
            get { return _topic; }
            set 
            {
                SetProperty(ref _topic, value);
            }
        }

        public IMvxCommand LogOut
        {
            get; set;
        }
    }
}
