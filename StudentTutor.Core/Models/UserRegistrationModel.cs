using StudentTutor.Core.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Media.Imaging;

namespace StudentTutor.Core.Models
{
    public class UserRegistrationModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Address { get; set; }
        public byte[] Passport { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
    public class UserRegistrationTrModel : IUserRegistrationTrModel
    {
        public UserRegistrationModel userModel { get; set; }
        public BitmapSource Passport { get; set; }
        public BindingList<SubjectOfInterestModel> SubjectOfInterest { get; set; }

    }
    public class UserSubjectsOfInterestModel
    {
        public List<SubjectOfInterestModel> SubjectsOfInterest { get; set; }
    }
}
