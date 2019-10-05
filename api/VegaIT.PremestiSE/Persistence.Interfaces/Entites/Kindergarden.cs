namespace Persistence.Interfaces.Entites
{
    public enum LocationType
    {
        Base,
        Remote
    }

    public class Kindergarden
    {
        public int Id { get; set; }
        public string Municipality { get; set; }
        public string Government { get; set; }
        public string City { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public string PostalCode { get; set; }
        public LocationType LocationType { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
    }
}
