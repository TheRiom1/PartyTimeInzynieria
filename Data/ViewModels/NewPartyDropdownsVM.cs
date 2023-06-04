using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models;

namespace WebApplication3.Data.ViewModels
{
    public class NewPartyDropdownsVM
    {
            public NewPartyDropdownsVM()
            {
                Organizators = new List<Organizator>();
                Rooms = new List<Room>();
                Features = new List<Feature>();
                Themes = new List<Theme>();
            }

            public List<Organizator> Organizators { get; set; }
            public List<Room> Rooms { get; set; }
            public List<Feature> Features { get; set; }
            public List<Theme> Themes { get; set; }
    }
}
