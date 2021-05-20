using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCarApp.Models
{
    public class BodyType
    {
        public BodyType()
        {
            Automobiles = new List<Automobile>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Info { get; set; }

        public virtual ICollection<Automobile> Automobiles { get; set; }
    }
}
