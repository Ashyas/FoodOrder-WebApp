using DataAccess.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace DataAccess.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(ApplicationDbContext db,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public void Initialize()
        {
            try
            {
                if(_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
                if (!_roleManager.RoleExistsAsync(StaticDetail.KitchenRole).GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new IdentityRole(StaticDetail.KitchenRole)).GetAwaiter().GetResult();
                    _roleManager.CreateAsync(new IdentityRole(StaticDetail.ManagerRole)).GetAwaiter().GetResult();
                    _roleManager.CreateAsync(new IdentityRole(StaticDetail.FrontDeskRole)).GetAwaiter().GetResult();
                    _roleManager.CreateAsync(new IdentityRole(StaticDetail.CustomerRole)).GetAwaiter().GetResult();

                    _userManager.CreateAsync(new ApplicationUser
                    {
                        UserName = "ash.app.test274@gmail.com",
                        Email = "ash.app.test274@gmail.com",
                        EmailConfirmed = true,
                        FirstName = "Asher",
                        LastName = "Yasia"
                    }, "Admin1234*").GetAwaiter().GetResult();

                    ApplicationUser user = _db.ApplicationUser.FirstOrDefault(u => u.Email == "ash.app.test274@gmail.com");
                    if (user != null)
                    {
                        _userManager.AddToRoleAsync(user, StaticDetail.ManagerRole).GetAwaiter().GetResult();
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }
    }
}
