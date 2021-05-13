using System.ComponentModel;
using System.Net.Http;
using System.Windows.Media.Imaging;

namespace StudentTutor.Core.Models.Interfaces
{
    public interface IUserRegistrationTrModel
    {
        BitmapSource Passport { get; set; }
        UserRegistrationModel userModel { get; set; }
        BindingList<SubjectOfInterestModel> SubjectOfInterest { get; set; }
        FormUrlEncodedContent EncodedContent { get; set; }
    }
}