using E_HealthCare.Data.Repo;
using E_HealthCare.Interfaces;
using E_HealthCare.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace E_HealthCare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {


        private readonly IUnitOfWork uow;
        public CategoryController( IUnitOfWork uow)
        {
            this.uow = uow;
           
        }

        //[HttpGet]
        //public IEnumerable<string> GetStrings()
        //{
        //    return new string[] { "Delhi", "Hyderabad", "Mumbai", "Chennai" };
        //}

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetCategories()
        {
            throw new UnauthorizedAccessException();
            var categories = await uow.CategoryRepository.GetCategoriesAsync ();
            return Ok(categories);
            
        }

        //Post api/category/add?catName=" ";

        //[HttpPost("add")]
        //[HttpPost("add/{catName}")]
        //public async Task<IActionResult> AddCategory(string catName)
        //{
        //    Category category = new Category();
        //    category.CategoryName = catName;
            
        //    await udc.Categories.AddAsync(category);
        //    await udc.SaveChangesAsync();
        //    return Ok(category);
        //}

        //post the data in json format

        [HttpPost("post")]
        public async Task<IActionResult> AddCategory(Category category)
        {
            

            uow.CategoryRepository.AddCategory(category);
             await uow.SaveAsync();
            return StatusCode(201);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateCategory(int id, Category category)
        {
           
                if (id != category.CatId)
                    return BadRequest("Update not allowed");
                var categoryFromDb = await uow.CategoryRepository.FindCategory(id);
                if (categoryFromDb == null)
                    return BadRequest("Update not allowed");

                //throw new Exception("some unknown error occured");
                await uow.SaveAsync();
                return StatusCode(200);

           
           
        }

            [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
           
           
            uow.CategoryRepository.DeleteCategory(id);
            await uow.SaveAsync();
            return Ok(id);
        }

    }
}
