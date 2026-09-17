using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Requests
{
    public class EmployeeCreateRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Position { get; set; }
        public string Username { get; set; }

        [Required(ErrorMessage = "This filed is required!")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Min Length 6 character!")]
        [Compare("ConfirmPassword", ErrorMessage = "Passwords do not match!")]
        public string Password { get; set; }

        [Required(ErrorMessage = "This filed is required!")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Min Length 6 character!")]
        [Compare("Password", ErrorMessage = "Passwords do not match!")]
        public string ConfirmPassword { get; set; }
    }
}
