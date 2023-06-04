using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Data.Base;

namespace WebApplication3.Models
{
    public class Party:IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Logo")]
        public string Picture { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        [Display(Name = "Price")]
        public double Price { get; set; }
       
        public double AdditionalDecorationsCost { get; set; }
        //Relationship
        public List<Party_Feature> Party_Feature { get; set; }
        
        //Room
        public int RoomId { get; set; }
        [ForeignKey("RoomId")]
        public Room PartyRoom { get; set; }

        //Organizator
        public int OrgId { get; set; }
        [ForeignKey("OrgId")]
        public Organizator PartyOrganizator { get; set; }

        //PartyTheme
        public int ThemeId { get; set; }
        [ForeignKey("ThemeId")]
        public Theme PartyTheme { get; set; }

        [Display(Name = "Ilość osób")]
        public int Guests { get; set; }

    }
}
