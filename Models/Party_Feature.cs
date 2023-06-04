using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models
{
    public class Party_Feature
    {
        public int PartyId { get; set; }
        public Party Party { get; set; }

        public int FeatureId { get; set; }
        public Feature Feature { get; set; }
    }
}
