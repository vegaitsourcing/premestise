using System;

namespace Persistence.Interfaces.Entites
{
    public class Match
    {
        public int Id { get; set; }
        public MatchedRequest FirstMatchedRequest { get; set; }
        public MatchedRequest SecondMatchedRequest { get; set; }
        public DateTime MatchedAt { get; set; }
    }
}
