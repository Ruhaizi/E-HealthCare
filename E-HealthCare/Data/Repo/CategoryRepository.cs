using E_HealthCare.Interfaces;
using E_HealthCare.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_HealthCare.Data.Repo
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly UserDetailContext udc;
        public CategoryRepository(UserDetailContext udc)
        {
            this.udc = udc;
        }

       

        public void AddCategory(Category category)
        {
            udc.Categories.AddAsync(category);
        }

        public void DeleteCategory(int CatId)
        {
            var category = udc.Categories.Find(CatId);
            udc.Categories.Remove(category);
        }

        public async Task<Category> FindCategory(int CatId)
        {
            return await udc.Categories.FindAsync(CatId);
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await udc.Categories.ToListAsync();
        }

     
    }
}
