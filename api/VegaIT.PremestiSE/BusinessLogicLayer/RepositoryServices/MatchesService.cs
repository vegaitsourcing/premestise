using System;
using System.Collections.Generic;
using DataAccessLayer.Contracts.Domain;
using BusinessLogicLayer.Contracts.RepositoryServices;

namespace BusinessLogicLayer.RepositoryServices
{
    public class MatchesService : RepositoryServiceBase<Match, Tuple<int, int>>, IMatchesService
    {
        public override Match Create(Match model)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Tuple<int, int> id)
        {
            throw new NotImplementedException();
        }

        public override Match Get(Tuple<int, int> id)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<Match> GetAll()
        {
            throw new NotImplementedException();
        }

        public override Match Update(Match model)
        {
            throw new NotImplementedException();
        }
    }
}
