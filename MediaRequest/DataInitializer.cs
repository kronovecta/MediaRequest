using MediaRequest.Application;
using MediaRequest.Domain;
using MediaRequest.Domain.Configuration;
using MediaRequest.WebUI.Models.IdentityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MediaRequest
{
    public class DataInitializer
    {
        private IConfiguration _config { get; }
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppSettings _appSetting;

        public DataInitializer(IConfiguration config, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<AppSettings> appSettings)
        {
            _config = config;
            _userManager = userManager;
            _roleManager = roleManager;
            _appSetting = appSettings.Value;
        }

        public async Task SeedData()
        {
            await SeedRoles();
            await SeedUsers();
        }

        public async Task SetConfigValues()
        {
            await GenerateAppIdentifier();
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

        private async Task GenerateAppIdentifier()
        {
            if(_appSetting.AppIdentifier == null && _appSetting.AppIdentifier == string.Empty)
            {
                var key = "Apollo:AppIdentifier";
                var val = Guid.NewGuid();

                try
                {
                    var filePath = Path.Combine(AppContext.BaseDirectory, "settings.json");
                    string json = File.ReadAllText(filePath);
                    dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);

                    var sectionPath = key.Split(":")[0];
                    if (!string.IsNullOrEmpty(sectionPath))
                    {
                        var keyPath = key.Split(":")[1];
                        jsonObj[sectionPath][keyPath] = val;
                    }
                    else
                    {
                        jsonObj[sectionPath] = val; // if no sectionpath just set the value
                    }
                    string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
                    File.WriteAllText(filePath, output);

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error writing app settings: " + ex);
                }
            }
        }
    }
}