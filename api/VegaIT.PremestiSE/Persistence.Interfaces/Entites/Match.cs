using System;

namespace Persistence.Interfaces.Entites
{
    public class Match
    {
        public int Id { get; set; }
        public Request FirstMatchedRequest { get; set; }
        public Request SecondMatchedRequest { get; set; }
        public DateTime MatchedAt { get; set; }
    }
}
