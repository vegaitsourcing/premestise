using System;
using System.Collections.Generic;
using System.Text;
using Persistence.Interfaces.Entites;

namespace Core.Interfaces.Models
{
    public class KindergardenDto
    {  
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal? Longitude { get; set; }
        public decimal? Latitude { get; set; }
    }
}
