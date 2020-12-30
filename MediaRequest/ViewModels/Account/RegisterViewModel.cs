using MediaRequest.Domain;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MediaRequest.WebUI.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(256, ErrorMessage = "Username is too long. Max 256 characters")]
        [RegularExpression("[a-zA-Z0-9]*$", ErrorMessage = "Username cannot contain special characters")]
        public string Username { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Passwords must be between 3 and 30 characters")]
        [RegularExpression("^(?=.{3,})[a-zA-Z0-9!-#%&]*$", ErrorMessage = "Invalid password format")]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        public InviteToken Token { get; set; }
    }
}
