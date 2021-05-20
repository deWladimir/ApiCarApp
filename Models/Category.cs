using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCarApp.Models
{
    public class Category
    {
        public Category() 
        {
            CategoriesAutomobiles = new List<CategoriesAutomobiles>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public string Info { get; set; }

        public virtual ICollection<CategoriesAutomobiles> CategoriesAutomobiles { get; set; }
    }
}
