using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiWithEF.Models
{
    public class EmpDbContext : DbContext
    {
        public EmpDbContext(DbContextOptions<EmpDbContext> options)
            : base(options) { }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasData(new User
                {
                    UserId = 1,
                    UserName = "user1",
                    Password = "user1"
                },
                new User
                {
                    UserId = 2,
                    UserName = "user2",
                    Password = "user2"
                },
                new User
                {
                    UserId = 3,
                    UserName = "user3",
                    Password = "user3"
                }
                );


        }
    }
}
