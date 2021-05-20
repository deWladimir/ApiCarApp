using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCarApp.Models
{
    public class CategoriesAutomobiles
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int AutomobileId { get; set; }
        public virtual Category Category { get; set; }
        public virtual Automobile Automobile { get; set; }
    }
}
