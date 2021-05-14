using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E_HealthCare.Models
{
    public class Category
    {
        [Key]
        public int CatId { get; set;}

        [Required(ErrorMessage ="Name is mandatory field")]
        public string CategoryName { get; set; }
    }
}
