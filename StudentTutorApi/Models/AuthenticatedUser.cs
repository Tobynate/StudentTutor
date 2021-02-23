using System;
using System.Collections.Generic;
using System.Text;

namespace StudentTutorApi.Models
{
    public class AuthenticatedUser
    {
        public string AccessToken { get; set; }
        public string Username { get; set; }
    }
}
