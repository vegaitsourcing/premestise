using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Contracts.Domain
{
 
    
    public class Request
    {

        public int Id { get; set; }
        public int ParentName { get; set; }
        public string ChildName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public DateTime Date { get; set; }

    }
}
