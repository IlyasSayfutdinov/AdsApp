using Microsoft.EntityFrameworkCore;
using WebApp1.Models;
using Microsoft.AspNetCore.Identity;

namespace WebApp1.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<User> Users { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Метод для поиска пользователя по имени и паролю
        public User FindUserByNameAndPassword(string name, string password)
        {
            return Users.FirstOrDefault(u => u.Name == name && u.Password == password);
        }
    }
}
