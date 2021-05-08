using StudentTutor.Core.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentTutor.Core.Models
{
    public class LoggedInUserModel : ILoggedInUserModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Address { get; set; }
       // public string SubjectOfInterest { get; set; }
        public byte[] Passport { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Token { get; set; }
       // public string Passport { get; set; }

        //public byte[] PassportAsBytes()
        //{
        //    return Encoding.ASCII.GetBytes(Passport);
        //}
    }

}