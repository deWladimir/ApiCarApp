using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ApiCarApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCarApp.Validators
{
    public class BodyTypeValidator : ValidationAttribute
    {
        private readonly CarsContext _context = new CarsContext();
        public override bool IsValid(object value)
        {
            BodyType c = value as BodyType;
            var areRepeats = _context.BodyTypes.Where(obj => obj.Name.ToLower() == c.Name.ToLower() && obj.Id != c.Id);
            if (areRepeats.Count() > 0) return false;
            else return true;
        }
    }
}
