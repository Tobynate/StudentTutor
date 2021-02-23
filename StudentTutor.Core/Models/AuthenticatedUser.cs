using System;
using System.Collections.Generic;
using System.Text;

namespace StudentTutor.Core.Models
{
    public class AuthenticatedUser
    {
        public string AccessToken { get; set; }
        public string Username { get; set; }
    }
}
