using E_HealthCare.Interfaces;
using E_HealthCare.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
        public async Task<UserInfo> Authenticate(string userName, string passwordText)
        {
 
           var userinfo= await udc.UsersInfo.FirstOrDefaultAsync(x => x.Username == userName);

            if (userinfo == null || userinfo.PasswordKey==null)
                return null;

            if (!MatchPasswordHash(passwordText, userinfo.Password, userinfo.PasswordKey))
                return null;

            return userinfo;

        }

        private bool MatchPasswordHash(string passwordText, byte[] password, byte[] passwordKey)
        {
            using (var hmac=new HMACSHA512(passwordKey))
            {

                var passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(passwordText));

                for (int i = 0; i < passwordHash.Length; i++)
                {
                    if (passwordHash[i] != password[i])
                        return false;
                }

                return true;

            }

        }



        public void Register(string userName, string password)
        {
            byte[] passwordHash, passwordKey;

            using(var hmac = new HMACSHA512())
            {
                passwordKey = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }


            UserInfo userinfo = new UserInfo();
            userinfo.Username = userName;
            userinfo.Password = passwordHash;
            userinfo.PasswordKey = passwordKey;
            udc.UsersInfo.Add(userinfo);


        }

        public  async Task<bool> UseralreadyExists(string userName)
        {
            return await udc.UsersInfo.AnyAsync(x => x.Username == userName);
        }
    }
}
