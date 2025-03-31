using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Infrastructure.Database;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace ZeafloServer.Infrastructure.Extensions
{
    public static class DbContextExtension
    {
        public static void EnsureMigrationsApplied(this DbContext context)
        {
            var applied = context.GetService<IHistoryRepository>().GetAppliedMigrations().Select(m => m.MigrationId);

            var total = context.GetService<IMigrationsAssembly>().Migrations.Select(m => m.Key);

            if (total.Except(applied).Any())
            {
                context.Database.Migrate();
            }
        }

        public static void SeedDatabase(this ApplicationDbContext context)
        {
            bool hasChanges = false;

            // 🟢 1. Check and create Role & User if they're not exist
/*            if (context.Roles.Count() == 0)
            {
                context.Roles.Add(new Domain.Entities.Role(
                    (int)Domain.Enums.UserRole.Administrator,
                    Domain.Enums.UserRole.Administrator.ToString()
                ));
                hasChanges = true;
            }*/

            var adminEmail = "admin@gmail.com";
            var adminUser = context.Users.FirstOrDefault(u => u.Email == adminEmail);

            if (adminUser == null)
            {
                adminUser = new Domain.Entities.User(
                    Domain.Constants.Ids.Seed.GuidId,
                    "administrator",
                    "admin@gmail.com",
                    BCrypt.Net.BCrypt.HashPassword("Admin@123"),
                    "Super Admin",
                    null,
                    Domain.Constants.Profile.User.Avatar,
                    Domain.Constants.Profile.User.Background,
                    "0989665476",
                    null,
                    null,
                    "",
                    new DateTime(1997, 09, 20),
                    Domain.Enums.Gender.Male,
                    false,
                    null,
                    new DateTime(2025, 03, 17),
                    null
                );

                context.Users.Add(adminUser);
                hasChanges = true;
            }

            // Save them to database
            if (hasChanges)
            {
                context.SaveChanges();
            }

            // 🟢 2. Check and create UserRole if it's not exists
/*            var adminRoleId = (int)Domain.Enums.UserRole.Administrator;
            if (context.Roles.Any(r => r.Code == adminRoleId) && context.Users.Any(u => u.Email == adminEmail)) // Ensure that Role & User exists
            {
                if (context.UserRoles.Count() == 0)
                {
                    context.UserRoles.Add(new Domain.Entities.UserRole(
                        Domain.Constants.Ids.Seed.IntId,
                        adminRoleId,
                        adminUser.UserId
                    ));

                    context.SaveChanges();
                }
            }*/
        }
    }
}
