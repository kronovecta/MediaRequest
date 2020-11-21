using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaRequest.WebUI.Models.IdentityModels
{
    public class ApplicationUser : IdentityUser
    {
        public byte[] Avatar { get; set; }
    }
}
