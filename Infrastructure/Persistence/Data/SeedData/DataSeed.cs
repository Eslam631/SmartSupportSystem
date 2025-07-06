using Domain.Contracts;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Persistence.Data.SeedData
{
    public class DataSeed(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,IOptions<SettingJsonAdmin> options):IDataSeed
    {
        private readonly UserManager<ApplicationUser> _UserManager = userManager;
        private readonly RoleManager<IdentityRole> _RoleManager = roleManager;
        private readonly SettingJsonAdmin _Options = options.Value;

        public async Task SeedAdminAsync()
        {
            try
            {
                if (!await _RoleManager.RoleExistsAsync("Admin"))
                {
                    IdentityRole role = new IdentityRole
                    {
                        Name = "Admin"
                    };
                    await _RoleManager.CreateAsync(role);
                }
                if (!await _UserManager.Users.AnyAsync())
                {
                    var role = await _RoleManager.FindByNameAsync("Admin");
                    var User = new ApplicationUser
                    {
                        UserName = _Options.Email,
                        Email = _Options.Email,
                        FirstName = _Options.FirstName,
                        LastName = _Options.LastName,

                        RoleId = role!.Id
                    };
                    var result = await _UserManager.CreateAsync(User, _Options.Password);
                }

            }
            catch (Exception )
            {

                throw;
            }
         
        }
    }
}
