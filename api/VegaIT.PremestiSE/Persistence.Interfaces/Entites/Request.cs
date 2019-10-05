using System;

namespace Persistence.Interfaces.Entites
{
    public class Request
    {
        public int Id { get; set; }
        public Kindergarden FromKindergarden { get; set; }
        public Kindergarden ToKindergarden { get; set; }
        public DateTime SubmittedAt { get; set; }
        public string ParentEmail { get; set; }
        public string ParentName { get; set; }
        public string ParentPhoneNumber { get; set; }
        public string ChildName { get; set; }
        public DateTime ChildBirthDate { get; set; }
    }
}
