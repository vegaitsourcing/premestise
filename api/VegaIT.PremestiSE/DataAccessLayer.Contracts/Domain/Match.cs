namespace DataAccessLayer.Contracts.Domain
{
    public enum Status
    {
        Approved,
        Waiting
    }

    public class Match
    {
        public int RequestOne { get; set; }
        public int RequestTwo { get; set; }
        public Status Status { get; set; }
    }
}
