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
        MockMatchRepository GetAll(IEnumerable<Match> matches)
        {
            Setup(x => x.GetAll()).Returns(matches);
            return this;
        }

        MockMatchRepository GetAllEmpty()
        {
            return GetAll(Enumerable.Empty<Match>());
        }

        MockMatchRepository Create(Match returnedMatch)
        {
            Setup(x => x.Create(It.IsAny<MatchedRequest>(), It.IsAny<MatchedRequest>())).Returns(returnedMatch);
            return this;
        }

        MockMatchRepository DeleteByRequestIdAny()
        {
            Setup(x => x.DeleteByRequestId(It.IsAny<int>()));
            return this;
        }
    }
}