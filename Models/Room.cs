using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Data.Base;

namespace WebApplication3.Models
{
    public class Room:IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Logo")]
        [Required(ErrorMessage = "To pole jest obowiązkowe")]
        public string Picture { get; set; }

        [Display(Name = "Nazwa")]
        [Required(ErrorMessage = "To pole jest obowiązkowe")]
        public string Name { get; set; }
        [Display(Name = "Opis")]
        [Required(ErrorMessage = "To pole jest obowiązkowe")]
        public string Description { get; set; }

        [Display(Name = "Cena")]
        [Required(ErrorMessage = "To pole jest obowiązkowe")]
        public double Price { get; set; }

        //Relationship
        public List<Party> Parties { get; set; }
    }
}
