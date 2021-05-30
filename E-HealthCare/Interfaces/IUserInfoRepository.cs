using E_HealthCare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_HealthCare.Interfaces
{
    public interface IUserInfoRepository
    {
        Task<UserInfo> Authenticate(string userName, string password);

        void Register(string userName, string password);

        Task<bool> UseralreadyExists(string userName);
    }
}
