using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaRequest.WebUI.Models.IdentityModels
{
    public interface IIdentityContext
    {
        DbSet<ApplicationUser> AspNetUsers { get; set; }

        Task<int> SaveChangesAsync();
    }
}
