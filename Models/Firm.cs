using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ApiCarApp.Models
{
    public class Firm
    {
        public Firm()
        {
            Automobiles = new List<Automobile>();
        }

        public int Id { get; set; }

        public int YearOfFoundation { get; set; }

        public string Name { get; set; }

        public string Info { get; set; }

        public int CountryId { get; set; }

        //[JsonIgnore]
        public virtual Country Country { get; set; }

        public virtual ICollection<Automobile> Automobiles { get; set; }
    }
}
