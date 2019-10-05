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
    public class MatchRepository : IMatchRepository
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

                using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
                {
                    DataSet dataSet = new DataSet();

                    dataAdapter.SelectCommand = cmd;
                    dataAdapter.Fill(dataSet, "match");

                    foreach (DataRow row in dataSet.Tables["match"].Rows)
                    {
                        matchs.Add(new Match
                        {
                            Id = (int)row["id"],
                            FirstMatchedRequest = null,
                            SecondMatchedRequest = null,
                            MatchedAt = (DateTime)row["matched_at"]
                        });
                    }
                }
            }
            return matchs;
        }

        public Match Create(MatchedRequest firstMatchedRequest, MatchedRequest secondMatchedRequest)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = _connString;
                connection.Open();

                DateTime matched_at = DateTime.Now;

                SqlTransaction transaction = connection.BeginTransaction();

                SqlCommand cmd = connection.CreateCommand();
                cmd.Transaction = transaction;
                cmd.CommandText = @"INSERT INTO match (first_matched_request_id, second_matched_request_id, matched_at) " +
                    "VALUES (@FirstMatchedRequestId, @SecondMatchedRequestId, @MatchedAt);";
                cmd.Parameters.Add(new SqlParameter("@FirstMatchedRequestId", firstMatchedRequest.Id));
                cmd.Parameters.Add(new SqlParameter("@SecondMatchedRequestId", secondMatchedRequest.Id));
                cmd.Parameters.Add(new SqlParameter("@MatchedAt", matched_at.ToString("yyyy-MM-dd HH:mm:ss")));
                cmd.Transaction = transaction;
                cmd.ExecuteNonQuery();

                SqlCommand cmdGetId = connection.CreateCommand();
                cmdGetId.CommandText = @"SELECT CONVERT(int, IDENT_CURRENT('match')) AS id;";
                cmdGetId.Transaction = transaction;

                try
                {
                    int id = (int)cmdGetId.ExecuteScalar();
                    transaction.Commit();
                    return new Match
                    {
                        Id = id,
                        FirstMatchedRequest = firstMatchedRequest,
                        SecondMatchedRequest = secondMatchedRequest,
                        MatchedAt = matched_at
                    };
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public void DeleteByRequestId(int id)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _connString;
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"DELETE FROM match WHERE first_matched_request_id=@Id OR second_matched_request_id=@Id;";
                cmd.Parameters.Add(new SqlParameter("@Id", id));

                cmd.ExecuteNonQuery();
            }
        }
    }
}