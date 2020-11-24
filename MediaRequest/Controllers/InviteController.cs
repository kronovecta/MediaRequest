using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Web;
using MediaRequest.WebUI.Models.IdentityModels;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using MediaRequest.Application;
using MediaRequest.Domain;
using Microsoft.Extensions.Options;
using MediaRequest.Domain.Configuration;
using MediaRequest.WebUI.Business.Extensions;

namespace MediaRequest.WebUI.Controllers
{
    public class InviteController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMediaDbContext _context;
        private readonly IUrlHelper urlHelper;
        private readonly IDataProtector _protectionProvider;
        private readonly AppSettings _appSettings;

        public InviteController(UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager, 
            IDataProtectionProvider protectionProvider, 
            IUrlHelperFactory urlHelperFactory, 
            IActionContextAccessor contextAccessor, 
            IMediaDbContext context, 
            IOptions<AppSettings> appSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _protectionProvider = protectionProvider.CreateProtector("TokenCreator");
            _appSettings = appSettings.Value;

            this.urlHelper = urlHelperFactory.GetUrlHelper(contextAccessor.ActionContext);
        }

        public async Task<IActionResult> Invite()
        {
            var token = GenerateToken();
            var creatorId = await _userManager.GetUserAsync(User);

            var invite = new InviteToken
            {
                CreatedAt = DateTime.Now,
                ValidUntil = DateTime.Now.AddHours(24),
                Status = false,
                Token = token,
                TokenOwnerId = creatorId.Id
            };

            await _context.InviteTokens.AddAsync(invite);
            await _context.SaveChangesAsync();

            var url = GenerateInviteUrl(token);
            return Content(url);
        }


        public IActionResult HandleInvite(string t)
        {
            var token = _context.InviteTokens.SingleOrDefault(x => x.Token == t);
            string parsedToken = "";

            try
            {
                parsedToken = _protectionProvider.Unprotect(t);
            }
            catch (Exception)
            {
                TempData["Error"] = "Error handling invite. Invite might be expired or has already been used";
                return RedirectToAction("Index", "Home");
            }

            if(token != null && parsedToken == _appSettings.AppIdentifier)
            {
                if (token.ValidUntil.ToUniversalTime() >= DateTime.Now.ToUniversalTime() && token.Status == false)
                {
                    return RedirectToAction(nameof(CreateMember), "Invite", new { t = token.Token });
                }
            }

            TempData["Error"] = "Error handling invite. Invite might be expired";
            return RedirectToAction("Index", "Home");
        }

        public IActionResult CreateMember(string t)
        {
            return View(nameof(CreateMember), t);
        }

        private string GenerateInviteUrl(string token)
        {
            return urlHelper.Action(nameof(HandleInvite), "Invite", new { t = token }, HttpContext.Request.Scheme);
        }
        private string GenerateToken()
        {
            return _protectionProvider.Protect(_appSettings.AppIdentifier);
        }
    }
}