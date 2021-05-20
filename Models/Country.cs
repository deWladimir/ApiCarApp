using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCarApp.Models
{
    public class Country
    {
        public Country()
        {
            Firms = new List<Firm>(); 
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public string Info { get; set; }

        public virtual ICollection<Firm> Firms { get; set; }
    }
}
