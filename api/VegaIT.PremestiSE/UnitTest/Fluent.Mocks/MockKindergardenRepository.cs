using System.Collections.Generic;
using System.Linq;
using Moq;
using Persistence.Interfaces.Contracts;
using Persistence.Interfaces.Entites;

namespace UnitTest.Fluent.Mocks
{
    public class MockKindergardenRepository : Mock<IKindergardenRepository>
    {
        public MockKindergardenRepository GetAll(List<Kindergarden> kindergardens)
        {
            Setup(x => x.GetAll()).Returns(kindergardens);
            return this;
        }

        public MockKindergardenRepository GetAllEmpty()
        {
            return GetAll(Enumerable.Empty<Kindergarden>().ToList());
        }

        public MockKindergardenRepository GetByInt(int id, Kindergarden returned)
        {
            Setup(x => x.GetById(id)).Returns(returned);
            return this;
        }

        public MockKindergardenRepository GetByIntAny(Kindergarden returned)
        {
            Setup(x => x.GetById(It.IsAny<int>())).Returns(returned);
            return this;
        }

        public MockKindergardenRepository GetToByRequestId(int id, List<Kindergarden> returnedList)
        {
            Setup(x => x.GetToByRequestId(id)).Returns(returnedList);
            return this;
        }

        public MockKindergardenRepository GetToByRequestIdAny(List<Kindergarden> returnedList)
        {
            Setup(x => x.GetToByRequestId(It.IsAny<int>())).Returns(returnedList);
            return this;
        }

        public MockKindergardenRepository GetToByRequestIdAnyReturnEmpty()
        {
            return GetToByRequestIdAny(Enumerable.Empty<Kindergarden>().ToList());
        }
    }
}