using System;

namespace StudentTutorApi.Library.Models
{
    public class UserModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Address { get; set; }
        public string SubjectOfInterest { get; set; }
        public byte Passport { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}