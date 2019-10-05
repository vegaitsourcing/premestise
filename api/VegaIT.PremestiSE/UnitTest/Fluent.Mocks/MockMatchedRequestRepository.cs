using System;
using Moq;
using Persistence.Interfaces.Contracts;
using Persistence.Interfaces.Entites;

namespace UnitTest.Fluent.Mocks
{
    public class MockMatchedRequestRepository : Mock<IMatchedRequestRepository>
    {
        /**
         * Change the id of the request. Will mutate the object being passed
         */
        public MockMatchedRequestRepository CreateChangeIdOfGiven(Request request)
        {
            request.Id = new Random().Next();
            Setup(x => x.Create(request)).Returns(request as MatchedRequest);
            return this;
        }

        public MockMatchedRequestRepository CreateReturningRequest(MatchedRequest returnedRequest)
        {
            Setup(x => x.Create(It.IsAny<Request>())).Returns(returnedRequest);
            return this;
        }
        
        public MockMatchedRequestRepository CreateReturnsEmptyRequestWithRandomId()
        {
            var request = new MatchedRequest();
            request.Id = new Random().Next();
            Setup(x => x.Create(It.IsAny<Request>())).Returns(request);
            return this;
        }

        public MockMatchedRequestRepository Delete(int id)
        {
            var request = new MatchedRequest();
            request.Id = id;
            Setup(x => x.Delete(id)).Returns(request);
            return this;
        }

        public MockMatchedRequestRepository DeleteAnySuccess()
        {
            var request = new MatchedRequest();
            request.Id = new Random().Next();
            Setup(x => x.Delete(It.IsAny<int>())).Returns(request);
            return this;
        }
    }
}