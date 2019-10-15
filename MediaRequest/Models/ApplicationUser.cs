using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaRequest.WebUI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Avatar { get; set; }
    }
}
