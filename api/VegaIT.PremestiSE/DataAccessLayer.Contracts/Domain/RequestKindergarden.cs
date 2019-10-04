using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Contracts.Domain
{
    public class RequestKindergarden
    {
        public int RequestId { get; set; }
        public int KindergardenId { get; set; }
        public int Priority { get; set; }
    }
}
