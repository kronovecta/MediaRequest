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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DataInitializer(IConfiguration config, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _config = config;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedData()
        {
            await SeedRoles();
            await SeedUsers();
        }

        public async Task SeedRoles()
        {
            var roles = new List<string> { "admin", "standard" };

            foreach (var roleName in roles)
            {
                if (!_roleManager.RoleExistsAsync(roleName).Result)
                {
                    var role = new IdentityRole(roleName);
                    await _roleManager.CreateAsync(role);
                }
            }
        }
        public async Task SeedUsers()
        {
            foreach (var employee in _config.GetSection("seedusers").GetChildren().ToList())
            {
                if (_userManager.FindByNameAsync(employee.Key).Result == null)
                {
                    var user = new ApplicationUser
                    {
                        UserName = _config[$"seedusers:{employee.Key}:username"],
                        Email = _config[$"seedusers:{employee.Key}:email"],
                        PhoneNumber = _config[$"seedusers:{employee.Key}:phone"]
                    };

                    await _userManager.CreateAsync(user, _config[$"seedusers:{employee.Key}:password"]);
                    await _userManager.AddToRoleAsync(user, employee.Key);
                }
            }
        }
    }
}