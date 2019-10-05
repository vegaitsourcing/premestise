using System;

namespace Persistence.Interfaces.Entites
{
    public class MatchedRequest : Request
    {
        public static explicit operator MatchedRequest(PendingRequest v)
        {
            throw new NotImplementedException();
        }
    }
}
