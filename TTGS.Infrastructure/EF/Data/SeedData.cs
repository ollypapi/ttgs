using Microsoft.AspNetCore.Identity;
using System.Linq;
using TTGS.Shared.Constants;
using TTGS.Shared.Entity;

namespace TTGS.Infrastructure.EF.Data
{
    public static class SeedData
    {
        public static void Initialize(ApplicationDbContext context, UserManager<AspNetUsers> userManager)
        {
            context.Database.EnsureCreated();

            if(context.Roles.FirstOrDefault(x => x.Name == UserRoleConstants.Client) == null)
            {
                context.Roles.Add(new IdentityRole {
                    Name = UserRoleConstants.Client,
                    NormalizedName = UserRoleConstants.Client
                });
            }

            if (context.Roles.FirstOrDefault(x => x.Name == UserRoleConstants.Contractor) == null)
            {
                context.Roles.Add(new IdentityRole
                {
                    Name = UserRoleConstants.Contractor,
                    NormalizedName = UserRoleConstants.Contractor
                });
            }

            if (context.Roles.FirstOrDefault(x => x.Name == UserRoleConstants.Employee) == null)
            {
                context.Roles.Add(new IdentityRole
                {
                    Name = UserRoleConstants.Employee,
                    NormalizedName = UserRoleConstants.Employee
                });
            }

            if (context.Roles.FirstOrDefault(x => x.Name == UserRoleConstants.Expenses) == null)
            {
                context.Roles.Add(new IdentityRole
                {
                    Name = UserRoleConstants.Expenses,
                    NormalizedName = UserRoleConstants.Expenses
                });
            }

            if (context.Roles.FirstOrDefault(x => x.Name == UserRoleConstants.Fleet) == null)
            {
                context.Roles.Add(new IdentityRole { Name = UserRoleConstants.Fleet,
                    NormalizedName = UserRoleConstants.Fleet
                });
            }

            if (context.Roles.FirstOrDefault(x => x.Name == UserRoleConstants.Admin) == null)
            {
                context.Roles.Add(new IdentityRole { Name = UserRoleConstants.Admin,
                    NormalizedName = UserRoleConstants.Admin
                });
            }

            var email = "operations@toyintrailers.com";
            if (userManager.FindByEmailAsync(email).Result == null)
            {
                AspNetUsers user = new AspNetUsers
                {
                    UserName = email,
                    Email = email
                };

                IdentityResult result = userManager.CreateAsync(user, "Germany123.").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, UserRoleConstants.Admin).Wait();
                }
            }

            context.SaveChanges();
        }
    }
}
