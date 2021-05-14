using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_HealthCare.Models
{
    public class UserDetailContext : DbContext
    { 
        public UserDetailContext ( DbContextOptions<UserDetailContext> options): base(options)
            {
            }
        public DbSet<UserDetail> UserDetail { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserInfo> UsersInfo { get; set; }

    }
}
