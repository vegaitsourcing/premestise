using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Configuration;
using Persistence.Interfaces.Contracts;
using Persistence.Interfaces.Entites;

namespace Persistence.Repositories
{
    public class PendingRequestWishRepository : IPendingRequestWishRepository
    {
        private readonly string _connectionString;

        public PendingRequestWishRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        public void CreateWishes(List<PendingRequestWishes> pendingRequestWishes)
        {
            using (var connection = new SqlConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();

                var transaction = connection.BeginTransaction();

                var cmd = connection.CreateCommand();
                cmd.Transaction = transaction;

                var values = new StringBuilder();

                for (var i = 0; i < pendingRequestWishes.Count; i++)
                {
                    values.Append("(@PendingRequestId" + i + ", @KindergardenWishId" + i + "), ");
                }

                cmd.CommandText =
                    @"INSERT INTO pending_request_wishes (pending_request_id, kindergarden_wish_id) VALUES" +
                    values.ToString();

                for (var i = 0; i < pendingRequestWishes.Count; i++)
                {
                    cmd.Parameters.Add(new SqlParameter("@PendingRequestId" + i,
                        pendingRequestWishes[i].PendingRequestId));
                    cmd.Parameters.Add(new SqlParameter("@KindergardenWishId" + i,
                        pendingRequestWishes[i].KindergardenWishId));
                }

                try
                {
                    cmd.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void Delete(PendingRequestWishes pendingRequestWish)
        {
            throw new NotImplementedException();
        }
    }
}
