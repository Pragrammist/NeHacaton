using DataBase;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using DataBase.Entities;

namespace Tests
{
    public class DbTests
    {

        DbContextOptions<UserContext> GetDbOptions() => new DbContextOptionsBuilder<UserContext>().UseSqlite("Data source= testdbusers.db").Options;

        UserContext GetTestContext() => new UserContext(GetDbOptions());

        [Test]
        public void TestDbConfigurationThrowDbUpdateException() 
        {
            var ex = Assert.Throws<DbUpdateException>(() => 
            {
                using (UserContext userContext = GetTestContext())
                {
                    userContext.Users.Add(new User { });
                    userContext.SaveChanges();
                }
            });            
        }

        [Test]
        public void TestDbConfig()
        {
            using (UserContext userContext = GetTestContext())
            {
                userContext.Users.Add(
                    new User { City = "some city", Email = "some email", Login = "some login", Password = "passhash", Telephone = "21321", TokenHash = "hash" });
                userContext.SaveChanges();
            }
        }
    }
}