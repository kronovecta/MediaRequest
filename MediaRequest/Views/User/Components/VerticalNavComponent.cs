using MediaRequest.Application;
using MediaRequest.WebUI.Models.IdentityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MediaRequest.WebUI.Views.User.Components
{
    [ViewComponent(Name = "VerticalNav")]
    public class VerticalNavComponent : ViewComponent
    {
        private readonly UserManager<ApplicationUser> _usermanager;

        public VerticalNavComponent(UserManager<ApplicationUser> usermanager)
        {
            _usermanager = usermanager;
        }

        public async Task<IViewComponentResult> InvokeAsync(int maxPriority, bool isDone)
        {
            var user = await GetUser();
            return View(user);
        }

        public async Task<ApplicationUser> GetUser()
        {
            return await _usermanager.GetUserAsync(UserClaimsPrincipal);
        }
    }
}
