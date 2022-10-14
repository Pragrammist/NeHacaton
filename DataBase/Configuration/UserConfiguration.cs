using DataBase.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace DataBase.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            BuildIndexes(builder);
            
            
        }

        void BuildIndexes(EntityTypeBuilder<User> builder)
        {
            builder.HasIndex(e => e.Login).IsUnique();
            builder.HasIndex(e => e.Telephone).IsUnique();
            builder.HasIndex(e => e.Email).IsUnique();
        }
        
    }
}