using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Data.Base;

namespace WebApplication3.Models
{
    public class Theme : IEntityBase
    {

        [Key]
        public int Id { get; set;  }
        [Display(Name = "Logo")]
        public string ProfilePicture { get; set; }
        [Display(Name = "Nazwa")]
        public string ThemeName { get; set; }
        [Display(Name = "Opis")]
        public string Description { get; set; }
        [Display(Name = "Cena")]
        public double Price { get; set; }

        //Relationship
        public List<Party> Parties { get; set; }

        public List<Product> Products { get; set; }
    }
}
