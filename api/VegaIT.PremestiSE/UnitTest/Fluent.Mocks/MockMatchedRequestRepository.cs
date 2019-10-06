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
        MockMatchedRequestRepository CreateChangeIdOfGiven(Request request, int matchId)
        {
            request.Id = new Random().Next();
            ((MatchedRequest)request).MatchId = matchId;
            Setup(x => x.Create(request, matchId)).Returns(request as MatchedRequest);
            return this;
        }

        MockMatchedRequestRepository CreateReturningRequest(MatchedRequest returnedRequest, int matchId)
        {
            returnedRequest.MatchId = matchId;
            Setup(x => x.Create(It.IsAny<Request>(), It.IsAny<int>())).Returns(returnedRequest);
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
