using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Data.Base;
using WebApplication3.Data.Enums;

namespace WebApplication3.Models
{
    public class Product:IEntityBase
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
       
        public string Description { get; set; }

        public double Price { get; set; }
        public ProductCategory ProductCategory { get; set; }

        //PartyTheme
        public int ThemeId { get; set; }
        [ForeignKey("ThemeId")]
        public Theme ProductTheme { get; set; }
    }
}
