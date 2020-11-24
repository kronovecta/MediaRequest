using MediaRequest.Application;
using MediaRequest.Domain.Configuration;
using MediaRequest.WebUI.Models.IdentityModels;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaRequest.WebUI.Views.User.Components
{
    [ViewComponent(Name = "CreateUser")]
    public class CreateUserComponent : ViewComponent
    {
        private readonly IMediaDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IDataProtector _protectionProvider;

        public CreateUserComponent(IOptions<AppSettings> appSettings, IMediaDbContext context, UserManager<ApplicationUser> userManager, IDataProtectionProvider protectionProvider)
        {
            _context = context;
            _userManager = userManager;
            _protectionProvider = protectionProvider.CreateProtector("CreteMemberComponent");
        }

        public async Task<IViewComponentResult> InvokeAsync(int maxPriority, string t)
        {
            var token = await _context.InviteTokens.SingleOrDefaultAsync(x => x.Token == t);

            if(token != null)
            {
                if (token.ValidUntil.ToUniversalTime() >= DateTime.Now.ToUniversalTime() && token.Status == false)
                {
                    return View("_CreateMemberValidToken");
                }
            }
            
            return View("_CreateMemberInvalidToken");
        }
    }
}
