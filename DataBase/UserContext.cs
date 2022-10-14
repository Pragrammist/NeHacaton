using DataBase.Entities;
using Microsoft.EntityFrameworkCore;
using DataBase.Configuration;


namespace DataBase
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;


       

        public UserContext(DbContextOptions<UserContext> options, bool isEnsureCreted = true) : base(options)
        {
            if (isEnsureCreted)
                Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UserConfiguration());


        }

    }
}