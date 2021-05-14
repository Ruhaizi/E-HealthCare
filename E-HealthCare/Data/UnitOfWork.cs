using E_HealthCare.Data.Repo;
using E_HealthCare.Interfaces;
using E_HealthCare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_HealthCare.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly UserDetailContext udc;
        public UnitOfWork(UserDetailContext udc)
        {
            this.udc = udc;
        }
        public ICategoryRepository CategoryRepository =>
            new CategoryRepository(udc);

        public UserDetailContext Udc { get; }

        public IUserInfoRepository UserInfoRepository =>
            new UserInfoRepository(udc);

        public async  Task<bool> SaveAsync()
        {
            return await udc.SaveChangesAsync()>0;
        }
    }
}
