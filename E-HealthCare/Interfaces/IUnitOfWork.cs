using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_HealthCare.Interfaces
{
    public interface IUnitOfWork 
    {
        ICategoryRepository CategoryRepository { get; }

        IUserInfoRepository UserInfoRepository { get; }
   
        Task<bool> SaveAsync();
       
    }
}
