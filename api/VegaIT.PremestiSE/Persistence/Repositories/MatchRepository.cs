using Microsoft.Extensions.Configuration;
using Persistence.Interfaces.Contracts;
using Persistence.Interfaces.Entites;
using Persistence.Interfaces.Entites.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Persistence.Repositories
{
    public class MatchRepository : RequestRepository<MatchedRequest>, IMatchRequestRepository
    {
        private readonly string _connString;

        public MatchRepository(IConfiguration config)
        {
            _connString = config.GetConnectionString("DefaultConnection");
        }

        public IEnumerable<Match> GetAll()
        {
            List<Match> matchs = new List<Match>();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _connString;
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"SELECT * FROM match;";

    }
}
