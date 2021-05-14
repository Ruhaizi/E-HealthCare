using E_HealthCare.Migrations;
using E_HealthCare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_HealthCare.Interfaces
{
    interface IUserRepository
    {
        Task<User> Authenticate(string userName, string password);
    }
}
