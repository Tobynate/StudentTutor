using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace StudentTutorApi.Core.Models
{
    public class UserRegistrationModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Address { get; set; }
        public byte[] Passport { get; set; }
        public DateTime CreatedDate { get; set; } 
        public FormUrlEncodedContent EncodedAuthContent { get; set; }
    }
}
