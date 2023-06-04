using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models;

namespace WebApplication3.Data.ViewModels
{
    public class NewProductDropdownsVM
    {
            public NewProductDropdownsVM()
            {
                Themes = new List<Theme>();
            }

            public List<Theme> Themes { get; set; }
    }
}
