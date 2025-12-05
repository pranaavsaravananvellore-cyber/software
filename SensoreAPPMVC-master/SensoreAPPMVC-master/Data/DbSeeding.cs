using System.Linq;
using SensoreAPPMVC.Models;
using SensoreAPPMVC.Utilities;
using System.Threading.Tasks;
using SensoreAPPMVC.Data; // Add this if AppDBContext is in this namespace

namespace SensoreAPPMVC.Data
{
    public static class DbSeeding
    {
        public static async Task DbInitialization(AppDBContext context) // Correct spelling: Initialization
        {
            await context.Database.EnsureCreatedAsync();
            //check if admin user exists
            if (context.Set<User>().Any())
            {
                return; //DB has already been seeded
            }

            //create default admin user
            string adminPasswordRaw = "12345"; //default password
            string hashedPassword = PasswordHasher.HashPassword(adminPasswordRaw);
            //   //TODO: hash password when hashing class is made
            var adminUser = new User()
            {
                Email = "Admin@aru.com",
                HashedPassword = hashedPassword, //TODO: set hashed password
                Name = "Admin User",
                DOB = DateOnly.Parse("16-12-2003"),
                Role = "Admin"
            };
            await context.Set<User>().AddAsync(adminUser);
            await context.SaveChangesAsync();
        } 
    }  
}