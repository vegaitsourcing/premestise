using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Contracts.Domain
{
    public enum Status
    {
        Approved,
        Waiting
    }
    public class Matches
    {
        public int RequestOne { get; set; }
        public int RequestTwo { get; set; }
        public Status Status { get; set; }
    }
}
