using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentTutorApi.Core.Models
{
    public class UserAccountBindingModel
    {
        [Required]
        [Display(Name = "User Identity")]
        public string Id { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        [MinLength(15)]
        public string Address { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "The email address you entered is not a valid email address.")]
        public string EmailAddress { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public string SubjectOfInterest { get; set; }
        public byte[] Passport { get; set; }
    }
}
