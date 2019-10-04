using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Contracts.Domain
{
    public class Kindergarden
    {

        public enum LocationType
        {
            Base,
            Remote
        }

        public class Request
        {
            public int Id { get; set; }
            public string Government { get; set; }
            public string City { get; set; }
            public string Name { get; set; }
            public string Department { get; set; }
            public string Street { get; set; }
            public string Street_Number { get; set; }
            public int PostalCode { get; set; }
            public LocationType LocationType { get; set; }
            public decimal Longitude { get; set; }
            public decimal Latitude { get; set; }

        }
    }
}
