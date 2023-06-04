using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Data.Base;

namespace WebApplication3.Models
{
    public class Organizator:IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Zdjęcie profilowe")]
        [Required(ErrorMessage = "To pole jest wymagane")]
        public string ProfilePicture { get; set; }

        [Display(Name = "Imię")]
        [Required(ErrorMessage = "To pole jest wymagane")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Imię musi mięć powyżej 3 znaków, ale nie więcej niż 50")]
        public string FirstName { get; set; }

        [Display(Name = "Nazwisko")]
        [Required(ErrorMessage = "To pole jest wymagane")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Nazwisko musi mięć powyżej 3 znaków, ale nie więcej niż 50")]
        public string LastName { get; set; }

        [Display(Name = "Opis")]
        [Required(ErrorMessage = "To pole jest wymagane")]
        public string Description { get; set; }

        //relationships
        public List<Party> Parties { get; set; }
    }
}
