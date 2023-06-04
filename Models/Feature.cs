using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Data.Base;

namespace WebApplication3.Models
{
    public class Feature : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Nazwa")]
        [Required(ErrorMessage ="Pole nazwa jest polem wymaganym")]
        [StringLength(50, MinimumLength =3, ErrorMessage = "Name musi mieć minimum 3 znaki, maksimum 50 znaków")]
        public string Name { get; set; }
       
        [Display(Name = "Opis")]
        [Required(ErrorMessage = "Pole opis jest polem wymaganym")]
        public string Description { get; set; }

        [Display(Name = "Cena")]
        [Required(ErrorMessage = "Pole cena jest polem wymaganym")]
        public double Price { get; set; }

        //Relationship
        public List<Party_Feature> Party_Feature { get; set; }
    }
}
