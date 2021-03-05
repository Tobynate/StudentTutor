using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StudentTutorApi.Models
{
    public class AuthenticatedUser
    {
        [Required]
        [Display(Name ="AccessToken")]
        public string AccessToken { get; set; }
        public string Username { get; set; }
    }
}
