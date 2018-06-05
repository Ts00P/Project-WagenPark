﻿using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using Wagenpark.Models;

[assembly: OwinStartupAttribute(typeof(Wagenpark.Startup))]
namespace Wagenpark
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesandUsers();
        }

        private void CreateRolesandUsers()
        {
            var context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            // In Startup iam creating first Admin Role and creating a default Admin User    
            if (!roleManager.RoleExists("Admin"))
            {

                // first we create Admin rool   
                var role = new IdentityRole
                {
                    Name = "Admin"
                };
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website                  

                var user = new ApplicationUser
                {
                    UserName = "admin",
                    Email = "admin@hotmail.nl"
                };

                var userPwd = "A@a12345";

                var chkUser = userManager.Create(user, userPwd);

                //Add default User to Role Admin   
                if (chkUser.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Admin");

                }
            }

            // creating Creating Manager role    
            if (!roleManager.RoleExists("Dealer"))
            {
                var role = new IdentityRole
                {
                    Name = "Dealer"
                };
                roleManager.Create(role);
            }
        }
    }
}