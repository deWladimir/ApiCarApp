using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using ApiCarApp.Validators;


namespace ApiCarApp.Models
{
    [BodyTypeValidator(ErrorMessage ="Такий тип кузова вже існує")]
    public class BodyType
    {
        public BodyType()
        {
            Automobiles = new List<Automobile>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Info { get; set; }
        [JsonIgnore]
        public virtual ICollection<Automobile> Automobiles { get; set; }
    }
}
