using E_HealthCare.Interfaces;
using E_HealthCare.Migrations;
using E_HealthCare.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_HealthCare.Data.Repo
{
    public class UserRepository : IUserRepository

    {
        private readonly UserDetailContext udc;
        public UserRepository(UserDetailContext udc)
        {
            this.udc = udc;

        }
        public async Task<User> Authenticate(string userName, string password)
        {
            return await udc.Users.FirstOrDefaultAsync(x => x.Username == userName && x.Password == password);
        }

      
    }
}
