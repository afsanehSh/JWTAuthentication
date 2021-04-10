using JWTAuthentication.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTAuthentication.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {

        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>()
                .Property(o => o.Id).HasDefaultValueSql("NEXT VALUE FOR [dbo].[AppUserSeqId]");
            builder.Entity<Role>()
                .Property(o => o.Id).HasDefaultValueSql("NEXT VALUE FOR [dbo].[RoleSeqId]");
        }
        public DbSet<AppUser> AppUser { get; set; }
        public DbSet<Role> Role { get; set; }
    }
}
