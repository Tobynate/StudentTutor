using MvvmCross.Commands;
using MvvmCross.ViewModels;
using StudentTutor.Core.Helpers;
using StudentTutor.Core.Properties;
using System;
using Microsoft.Win32;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using StudentTutor.Core.Models;
using System.Drawing;
using StudentTutor.Core.Models.Interfaces;
using StudentTutor.Core.Helpers.Interfaces;
using System.Linq;
using System.Linq.Expressions;
using MvvmCross.Navigation;

namespace StudentTutor.Core.ViewModels
{
    public class RegisterUserViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _mvxNavigation;
        private readonly ISubjectOfInterestModelList _subjectsOfInterest;
        private readonly ISubjectOfInterestModel _subjectOfInterestModel;
        private readonly IApiHelper _apiHelper;
        private readonly IUserRegistrationTrModel _registrationTrModel;

        public RegisterUserViewModel(IMvxNavigationService mvxNavigation, ISubjectOfInterestModelList subjectsOfInterestModel, ISubjectOfInterestModel subjectOfInterestModel, IApiHelper apiHelper, IUserRegistrationTrModel registrationTrModel)
        {
            this._apiHelper = apiHelper;
            this._registrationTrModel = registrationTrModel;
            Passport = GetImage(Resources.Female_Icon, Passport);

            OperatingSystem operatingSystem = Environment.OSVersion;

            Upload = new MvxCommand(UploadCommand);
            Remove = new MvxCommand(RemoveSelectedSubjectCommand);/*MvxAsyncCommand*/
            this._mvxNavigation = mvxNavigation;
            this._subjectsOfInterest = subjectsOfInterestModel;
            this._subjectOfInterestModel = subjectOfInterestModel;

            Task.Run(() => GetSubjectOfInterest()).Wait();

            SubjectsAndNotes = new BindingList<SubjectOfInterestModel>(_subjectsOfInterest.SubjectOfInterestModels);
            
        }
        private async Task GetSubjectOfInterest()
        {
            await _apiHelper.GetSubjectOfInterest();
        }

        private MvxInteraction<FileDialogInteraction> _interaction = new MvxInteraction<FileDialogInteraction>();

        public IMvxInteraction<FileDialogInteraction> Interaction => _interaction;

        public MvxCommand Upload { get; set; }

        #region
        /// <summary>
        /// This region handles the emptying of textboxes on focus and repopulating on Lostfocus
        /// </summary>
        public IMvxAsyncCommand<string> GotFocusCommand => Foc();
        public IMvxAsyncCommand<string> LostFocusCommand => Foc();

        private MvxAsyncCommand<string> Foc()
        {
            return new MvxAsyncCommand<string>(ExecuteFocusCommands);
        }

        private async Task ExecuteFocusCommands(string parameter)
        {
            string action = await Task.Run(() => parameter.Substring(parameter.Length - 1, 1));
            string prop = parameter.Replace(action, "").Trim();
            switch (prop)
            {
                case "Lastname":
                    if (action == "1" && LastName == prop)
                    {
                        LastName = "";
                    }
                    else if (action == "0" && LastName == "")
                    {
                        LastName = "Lastname";
                    }
                    break;
                case "Firstname":
                    if (action == "1" && FirstName == prop)
                    {
                        FirstName = "";
                    }
                    else if (action == "0" && FirstName == "")
                    {
                        FirstName = "Firstname";
                    }
                    break;
                case "Email Address":
                    if (action == "1" && EmailAddress == prop)
                    {
                        EmailAddress = "";
                    }
                    else if (action == "0" && EmailAddress == "")
                    {
                        EmailAddress = "Email Address";
                    }
                    break;
                case "Address":
                    if (action == "1" && Address == prop)
                    {
                        Address = "";
                    }
                    else if (action == "0" && Address == "")
                    {
                        Address = "Address";
                    }
                    break;
            }
        }


        private async Task ExecuteLostFocusLastName()
        {
            if (LastName == "")
            {
                await Task.Run(() => LastName = "Lastname");
            }
        }


        #endregion

        #region
        /// <summary>
        /// MvxInteraction to remove Selected Subject of interest
        /// </summary>
        private MvxInteraction<RemoveSelectedSubjectInteraction> _subjectInteraction = new MvxInteraction<RemoveSelectedSubjectInteraction>();

        public IMvxInteraction<RemoveSelectedSubjectInteraction> SubjectInteraction => _subjectInteraction;

        public MvxCommand Remove { get; set; }

        private /*async Task*/void RemoveSelectedSubjectCommand()
        {
            byte status = 0;
            var request = new RemoveSelectedSubjectInteraction
            {
                SubjectSelected = async (ok) =>
                {
                    if (ok)
                    {
                        status = 1;
                    }
                    else
                    {
                        status = 0;
                    }

                },
            };

            _subjectInteraction.Raise(request);
            if (status == 1)
            {
                /*await Task.Run(() => */
                SubjectsAndNotes.Add(SelectedSubjectOfInterest);//);
                /*await Task.Run(() => */
                SubjectOfInterest.Remove(SelectedSubjectOfInterest);//);
                SelectedSubjectsAndNote = null;
            }
        }

        #endregion

        private void UploadCommand()
        {

            byte status = 0;
            // 1. do cool stuff with profile data
            // ...

            // 2. request interaction from view
            // 3. execution continues in callbacks
            var request = new FileDialogInteraction
            {
                SelectedFile = async (ok) =>
                {
                    if (ok) 
                    {
                        await Task.Run(() => status = 1);
                    }
                    else
                    {
                        status = 0;
                    }
                       
                },
            };

            _interaction.Raise(request);
            if (status == 1)
            {//TODO Error handling for the passport upload
                Passport = GetImage(ConvertToByte(request.File), Passport);
            }
        }

        private byte[] ConvertToByte(FileInfo filePath)
        {

            Image image = Image.FromFile(filePath.FullName);
            using(var ms =new MemoryStream())
            {
                image.Save(ms, image.RawFormat);

                return ms.ToArray();
            }
        }

        private BitmapSource GetImage(byte[] source, BitmapSource output)
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
               
        private string _lastName = "Lastname";

        public string LastName
        {
            get { return _lastName; }
            set 
            {
                SetProperty(ref _lastName, value);
            }
        }
        private string _firstName = "Firstname";

        public string FirstName
        {
            get { return _firstName; }
            set 
            {
                SetProperty(ref _firstName, value); 
            }
        }
        private string _emailAddress = "Email Address";

        public string EmailAddress 
        {
            get { return _emailAddress; }
            set 
            {
                SetProperty(ref _emailAddress, value); 
            }
        }
        private string _address = "Address";

        public string Address
        {
            get { return _address; }
            set
            {
                SetProperty(ref _address, value);
            }
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
               
        private BindingList<SubjectOfInterestModel> _subjectsAndNotes;

        public BindingList<SubjectOfInterestModel> SubjectsAndNotes
        {
            get { return _subjectsAndNotes; }
            set
            {
                SetProperty(ref _subjectsAndNotes, value); 
            }
        }
        private BindingList<SubjectOfInterestModel> _subjectOfInterest = new BindingList<SubjectOfInterestModel>();

        public BindingList<SubjectOfInterestModel> SubjectOfInterest
        {
            get { return _subjectOfInterest; }
            set 
            {
                SetProperty(ref _subjectOfInterest, value);
            }
        }

        private SubjectOfInterestModel _selectedSubjectsAndNote;

        public SubjectOfInterestModel SelectedSubjectsAndNote
        {
            get { return _selectedSubjectsAndNote; }
            set 
            {
                SetProperty(ref _selectedSubjectsAndNote, value);

                if (SelectedSubjectsAndNote != null && !SubjectOfInterest.Contains(SelectedSubjectsAndNote))
                {
                    SubjectsAndNotes.Remove(SelectedSubjectsAndNote);
                    SubjectOfInterest.Add(SelectedSubjectsAndNote);
                }

                //PopulateSubjectOfInterest();
            }
        }

        private SubjectOfInterestModel _selectedSubjectOfInterest;

        public SubjectOfInterestModel SelectedSubjectOfInterest
        {
            get { return _selectedSubjectOfInterest; }
            set 
            {
                SetProperty(ref _selectedSubjectOfInterest, value);
            }
        }
        private async Task<KeyValuePair<string,bool>> ValidateSubmit(params object[] modelItem)
        {
            for (int i = 0; i < modelItem.Length; i+=2)
            {
                if (modelItem[i] is string && string.IsNullOrEmpty((string)modelItem[i]) || (string)modelItem[i] == (string)modelItem[i +1])
                {
                    return await Task.Run(() => new KeyValuePair<string, bool>((string)modelItem[i + 1], false));
                }
            }
            return await Task.Run(() => new KeyValuePair<string, bool>("default", true));
        }

        public IMvxAsyncCommand Submit => new MvxAsyncCommand(SubmitCommand);

        private async Task SubmitCommand()
        {
            UserRegistrationModel registrationModel = new UserRegistrationModel();

            registrationModel.Address = Address;
            registrationModel.CreatedDate = DateTime.UtcNow;
            registrationModel.EmailAddress = EmailAddress;
            registrationModel.FirstName = FirstName;
            registrationModel.LastName = LastName;
            // registrationModel.Passport = Passport;
            var output = await ValidateSubmit(registrationModel.Address, "Address", registrationModel.EmailAddress, "Email Address", registrationModel.FirstName, "Firstname", registrationModel.LastName, "Lastname");
            if (output.Value)
            {
                _registrationTrModel.userModel = registrationModel;
                _registrationTrModel.Passport = Passport;
                _registrationTrModel.SubjectOfInterest = SubjectOfInterest;

                await _mvxNavigation.Navigate<SubmitPasswordViewModel>();
            }
            else
            {//TODO create error display method
                string errorMsg = "Invalid value input for " + output.Key;
            }

        }
    }
   
}
