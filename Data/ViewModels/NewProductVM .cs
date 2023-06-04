using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Data;
using WebApplication3.Data.Base;
using WebApplication3.Data.Enums;


namespace WebApplication3.Models
{
    public class NewProductVM
    {
       
        public int Id { get; set; }

        [Display(Name = "Nazwa")]
        public string Name { get; set; }

        [Display(Name = "Logo")]
        public string Picture { get; set; }

        [Display(Name = "Opis")]
        public string Description { get; set; }
        [Display(Name = "Cena")]
        public double Price { get; set; }
        [Display(Name = "Kategoria")]
        public ProductCategory ProductCategory { get; set; }

        //PartyTheme
        public int ThemeId { get; set; }


    }
}
