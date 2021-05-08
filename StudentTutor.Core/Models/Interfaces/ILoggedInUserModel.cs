using System;

namespace StudentTutor.Core.Models.Interfaces
{
    public interface ILoggedInUserModel
    {
        string Token { get; set; }
        string Address { get; set; }
        DateTime CreatedDate { get; set; }
        string EmailAddress { get; set; }
        string FirstName { get; set; }
        string Id { get; set; }
        string LastName { get; set; }
        byte[] Passport { get; set; }
        //string SubjectOfInterest { get; set; }
        //byte[] PassportAsBytes();
    }
}