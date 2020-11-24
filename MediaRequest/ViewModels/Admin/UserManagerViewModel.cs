using MediaRequest.WebUI.Models.IdentityModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaRequest.WebUI.ViewModels.Admin
{
    public class UserManagerViewModel
    {
        public List<UserRoleViewModel> Users { get; set; }

        public UserManagerViewModel()
        {
            Users = new List<UserRoleViewModel>();
        }
    }

    public class UserRoleViewModel
    {
        public ApplicationUser User { get; set; }
        public IList<string> Roles { get; set; }
    }
}
