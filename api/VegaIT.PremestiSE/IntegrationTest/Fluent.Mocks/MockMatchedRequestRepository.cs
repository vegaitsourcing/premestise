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
        MockMatchedRequestRepository CreateChangeIdOfGiven(Request request)
        {
            request.Id = new Random().Next();
            Setup(x => x.Create(request)).Returns(request as MatchedRequest);
            return this;
        }

        MockMatchedRequestRepository CreateReturningRequest(MatchedRequest returnedRequest)
        {
            Setup(x => x.Create(It.IsAny<Request>())).Returns(returnedRequest);
            return this;
        }
        
        MockMatchedRequestRepository CreateReturnsEmptyRequestWithRandomId()
        {
            var request = new MatchedRequest();
            request.Id = new Random().Next();
            Setup(x => x.Create(It.IsAny<Request>())).Returns(request);
            return this;
        }

        MockMatchedRequestRepository Delete(int id)
        {
            var request = new MatchedRequest();
            request.Id = id;
            Setup(x => x.Delete(id)).Returns(request);
            return this;
        }

        MockMatchedRequestRepository DeleteAnySuccess()
        {
            var request = new MatchedRequest();
            request.Id = new Random().Next();
            Setup(x => x.Delete(It.IsAny<int>())).Returns(request);
            return this;
        }
    }
}