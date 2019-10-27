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
                            MatchedAt = (DateTime)row["matched_at"]
                        });
                    }
                }
            }
            return matchs;
        }

        public Match Create()
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = _connString;
                connection.Open();

                DateTime matched_at = DateTime.Now;

                SqlTransaction transaction = connection.BeginTransaction();

                SqlCommand cmd = connection.CreateCommand();
                cmd.Transaction = transaction;
                cmd.CommandText = @"INSERT INTO matches (matched_at, status) " +
                    "VALUES ( @MatchedAt, @Status); SELECT SCOPE_IDENTITY()";

                cmd.Parameters.Add("@MatchedAt", SqlDbType.DateTime2).Value = matched_at;
                cmd.Parameters.Add("@Status", SqlDbType.Int).Value = Status.Matched;

                try
                {
                    int id = Convert.ToInt32(cmd.ExecuteScalar());
                    transaction.Commit();
                    return new Match
                    {
                        Id = id,
                        MatchedAt = matched_at,
                        Status = Status.Matched
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

        public void SetStatus(int id, Status status)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _connString;
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"UPDATE matches SET status = @status WHERE id = @id;";
                cmd.Parameters.Add(new SqlParameter("@Id", id));
                cmd.Parameters.Add(new SqlParameter("@Status", status));

                cmd.ExecuteNonQuery();
            }
        }
    }
}