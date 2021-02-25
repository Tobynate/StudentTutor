using System;
using System.Collections.Generic;
using System.Text;

namespace StudentTutor.Core.Models
{
    public class AuthenticatedUser
    {
        public string Access_Token { get; set; }
        public string Username { get; set; }
        public string Issued { get; set; }
        public string Expires { get; set; }
    }
}
