using E_HealthCare.Interfaces;
using E_HealthCare.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_HealthCare.Data.Repo
{
    public class UserInfoRepository : IUserInfoRepository
    {
        private readonly UserDetailContext udc;
        public UserInfoRepository(UserDetailContext udc)
        {
            this.udc = udc;
 
        }
        public async Task<UserInfo> Authenticate(string userName, string password)
        {
            return await udc.UsersInfo.FirstOrDefaultAsync(x => x.Username == userName && x.Password == password);
        }
    }
}
