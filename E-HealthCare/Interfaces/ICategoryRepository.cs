using E_HealthCare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_HealthCare.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetCategoriesAsync();

        void AddCategory(Category category);

        void DeleteCategory(int CatId);
        
        Task<Category> FindCategory(int CatId);

     
    }
}
