using System;

namespace Persistence.Interfaces.Entites
{
    public enum Status
    {
        Matched = 1,
        Success = 2,
        Failure = 0
    }

    public class Match
    {
        public int Id { get; set; }
        public DateTime MatchedAt { get; set; }
        public Status Status { get; set; }
    }
}
