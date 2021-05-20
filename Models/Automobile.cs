using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCarApp.Models
{
    public class Automobile
    {
        public Automobile()
        {
            CategoriesAutomobiles = new List<CategoriesAutomobiles>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set;}
        public string EngineVolume { get; set; }
        public int YearOfRelease { get; set; }
        public string Info { get; set; }
        public int FirmId { get; set; }
        public int FuelTypeId { get; set; }
        public int BodyTypeId { get; set; }
        public virtual Firm Firm { get; set; }
        public virtual FuelType FuelType { get; set; }
        public virtual BodyType BodyType { get; set; }
        public virtual ICollection<CategoriesAutomobiles> CategoriesAutomobiles { get; set; }
    }
}
