using DataBase.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Extensions
{
    public static class UserDbSetExtensions
    {
        public static async Task<User?> FindUserByAsync(this DbSet<User> users, string login, string? password = null) =>
            await users.FirstOrDefaultAsync(u => (u.Email == login || u.Telephone == login || u.Login == login) && (password == null || u.Password == password)); // if pass is null always true
        
    }
}
