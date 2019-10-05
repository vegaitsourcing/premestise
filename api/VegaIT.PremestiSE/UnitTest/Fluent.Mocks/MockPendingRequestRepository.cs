using System.Collections.Generic;
using Moq;
using Persistence.Interfaces.Contracts;
using Persistence.Interfaces.Entites;

namespace UnitTest.Fluent.Mocks
{
    public class MockPendingRequestRepository : Mock<IPendingRequestRepository>
    {
        MockPendingRequestRepository GetAnyId(PendingRequest returnedRequest)
        {
            Setup(x => x.Get(It.IsAny<int>())).Returns(returnedRequest);
            return this;
        }

        MockPendingRequestRepository Create(PendingRequest returnedRequest)
        {
            Setup(x => x.Create(It.IsAny<PendingRequest>())).Returns(returnedRequest);
            return this;
        }

        MockPendingRequestRepository GetAllMatchesFor(IEnumerable<PendingRequest> returnedRequests)
        {
            Setup(x => x.GetAllMatchesFor(It.IsAny<PendingRequest>())).Returns(returnedRequests);
            return this;
        }

        MockPendingRequestRepository DeleteAnyId()
        {
            Setup(x => x.Delete(It.IsAny<int>()));
            return this;
        }

        MockPendingRequestRepository VerifyAnyId()
        {
            Setup(x => x.Verify(It.IsAny<int>()));
            return this;
        }
    }
}