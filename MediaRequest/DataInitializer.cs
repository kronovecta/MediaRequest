using MediaRequest.Application;
using MediaRequest.Domain;
using MediaRequest.WebUI.Models.IdentityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaRequest
{
    public class DataInitializer
    {
        private IConfiguration _config { get; }

        public DataInitializer(IConfiguration config)
        {
            _config = config;
        }

        public void SeedData(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        private void SeedRequests()
        {
            //var request = new UserRequest { UserId = "a4ab47cc-ddf1-419a-90eb-3e86dd8fa50c", MovieId =  }
        }

        public void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            var roles = new List<string> { "admin", "standard" };

            foreach (var roleName in roles)
            {
                if (!roleManager.RoleExistsAsync(roleName).Result)
                {
                    var role = new IdentityRole(roleName);
                    roleManager.CreateAsync(role).Wait();
                }
            }
        }

        public void SeedUsers(UserManager<ApplicationUser> userManager)
        {
            foreach (var employee in _config.GetSection("seedusers").GetChildren().ToList())
            {
                if (userManager.FindByNameAsync(employee.Key).Result == null)
                {
                    //var user = new ApplicationUser
                    //{
                    //    UserName = _config[$"seedusers:{employee.Key}:username"],
                    //    Email = _config[$"seedusers:{employee.Key}:email"],
                    //    PhoneNumber = _config[$"seedusers:{employee.Key}:phone"]
                    //};

                    //userManager.CreateAsync(user, _config[$"seedusers:{employee.Key}:password"]).Wait();
                    //userManager.AddToRoleAsync(user, employee.Key).Wait();
                }
            }
        }
    }
}