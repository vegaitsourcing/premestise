using System;

namespace Persistence.Interfaces.Entites
{
    public class Match
    {
        public int Id { get; set; }
        public int FirstMatchedRequest { get; set; }
        public int SecondMatchedRequest { get; set; }
        public DateTime MatchedAt { get; set; }
    }
}
