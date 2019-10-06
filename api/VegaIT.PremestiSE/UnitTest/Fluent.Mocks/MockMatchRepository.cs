using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Persistence.Interfaces.Contracts;
using Persistence.Interfaces.Entites;
using Match = Persistence.Interfaces.Entites.Match;

namespace UnitTest.Fluent.Mocks
{
    public class MockMatchRepository : Mock<IMatchRepository>
    {
        public MockMatchRepository GetAll(IEnumerable<Match> matches)
        {
            Setup(x => x.GetAll()).Returns(matches);
            return this;
        }

        public MockMatchRepository GetAllEmpty()
        {
            return GetAll(Enumerable.Empty<Match>());
        }

        public MockMatchRepository Create(Match returnedMatch)
        {
            Setup(x => x.Create()).Returns(returnedMatch);
            return this;
        }

        public MockMatchRepository DeleteByRequestIdAny()
        {
            Setup(x => x.DeleteByRequestId(It.IsAny<int>()));
            return this;
        }
    }
}
