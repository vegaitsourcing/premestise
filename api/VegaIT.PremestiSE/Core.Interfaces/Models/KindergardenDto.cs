using System;
using System.Collections.Generic;
using System.Text;
using Persistence.Interfaces.Entites;

namespace Core.Interfaces.Models
{
    public class KindergardenDto
    {
        public static KindergardenDto FromKindergarden(Kindergarden kindergarden)
        {
            return new KindergardenDto
            {
                Id = kindergarden.Id,
                Name = kindergarden.Name,
                Longitude = kindergarden.Longitude,
                Latitude = kindergarden.Latitude
            };
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
    }
}
