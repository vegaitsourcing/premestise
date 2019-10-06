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
    public class MatchedRequestRepository : IMatchedRequestRepository
    {
        private readonly string _connectionString;

        public MatchedRequestRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");

        }

        public IEnumerable<MatchedRequest> GetAll()
        {
            List<MatchedRequest> matchedRequests = new List<MatchedRequest>();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _connectionString;
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"SELECT * FROM matched_request;";

                using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
                {
                    DataSet dataSet = new DataSet();

                    dataAdapter.SelectCommand = cmd;
                    dataAdapter.Fill(dataSet, "matched_request");

                    foreach (DataRow row in dataSet.Tables["matched_request"].Rows)
                    {
                        matchedRequests.Add(new MatchedRequest()
                        {
                            Id = (int)row["id"],
                            MatchId = (int)row["match_id"],
                            FromKindergardenId = (int)row["from_kindergarden_id"],
                            SubmittedAt = row["submitted_at"] == System.DBNull.Value ? DateTime.Now : (DateTime)row["submitted_at"],
                            ParentEmail = row["email"] == System.DBNull.Value ? null : (string)row["email"],
                            ParentName = row["parent_name"] == System.DBNull.Value ? null : (string)row["parent_name"],
                            ParentPhoneNumber = row["phone_number"] == System.DBNull.Value ? null : (string)row["phone_number"],
                            ChildName = row["child_name"] == System.DBNull.Value ? null : (string)row["child_name"],
                            ChildBirthDate = row["child_birth_date"] == System.DBNull.Value ? DateTime.Now : (DateTime)row["child_birth_date"],


                            KindergardenWishIds = GetMachedWishes((int)row["id"])
                        });
                    }
                }

            }
            return matchedRequests;
        }

        private List<int> GetMachedWishes(int id)
        {
            List<int> kindergardenWishIds = new List<int>();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _connectionString;
                conn.Open();

                string newQuery = @"SELECT kindergarden_wish_id from matched_request_wishes WHERE matched_request_id = @id";
                using (SqlCommand command = new SqlCommand(newQuery, conn))
                {
                    command.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            kindergardenWishIds.Add((int)reader["kindergarden_wish_id"]);
                        }
                    }
                }

                return kindergardenWishIds;
            }
        }

        public MatchedRequest Create(Request request, int matchId)
        {
            request.SubmittedAt = request.SubmittedAt;

            SqlCommand command = new SqlCommand();
            command.CommandText = $@"INSERT INTO matched_request (email, parent_name, phone_number, child_name, child_birth_date, from_kindergarden_id, submitted_at, match_id) 
                                    VALUES (@email, @parentName, @parentPhoneNumber, @childName, @childBirthDate, @fromKindergardenId, @submittedAt, @matchId)
                                    SELECT SCOPE_IDENTITY()";

            command.Parameters.Add("@email", SqlDbType.NVarChar).Value = request.ParentEmail;
            command.Parameters.Add("@parentPhoneNumber", SqlDbType.NVarChar).Value = request.ParentPhoneNumber;
            command.Parameters.Add("@parentName", SqlDbType.NVarChar).Value = request.ParentName;
            command.Parameters.Add("@childName", SqlDbType.NVarChar).Value = request.ChildName;
            command.Parameters.Add("@childBirthDate", SqlDbType.DateTime).Value = request.ChildBirthDate;
            command.Parameters.Add("@fromKindergardenId", SqlDbType.Int).Value = request.FromKindergardenId;
            command.Parameters.Add("@submittedAt", SqlDbType.NVarChar).Value = request.SubmittedAt.ToString("yyyy-MM-dd HH:mm:ss");
            command.Parameters.Add("@matchId", SqlDbType.Int).Value = matchId;


            SqlCommand secondCommand = new SqlCommand();

            var values = new StringBuilder();
            for (var i = 0; i < request.KindergardenWishIds.Count; i++)
            {
                if (i == request.KindergardenWishIds.Count - 1)
                    values.Append("(@MatchedRequestId, @KindergardenWishId" + i + ")");
                else
                    values.Append("(@MatchedRequestId, @KindergardenWishId" + i + "), ");
            }

            secondCommand.CommandText =
                @"INSERT INTO matched_request_wishes (matched_request_id, kindergarden_wish_id) VALUES" +
                values.ToString();

            for (var i = 0; i < request.KindergardenWishIds.Count; i++)
            {
                secondCommand.Parameters.Add(new SqlParameter("@KindergardenWishId" + i,
                    request.KindergardenWishIds[i]));
            }
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = _connectionString;
                command.Connection = connection;
                secondCommand.Connection = connection;
                connection.Open();

                SqlTransaction transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                secondCommand.Transaction = transaction;

                try
                {
                    int id = Convert.ToInt32(command.ExecuteScalar());
                    request.Id = id;

                    secondCommand.Parameters.Add(new SqlParameter("@MatchedRequestId", request.Id));

                    secondCommand.ExecuteNonQuery();

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
            return new MatchedRequest()
            {
                Id = request.Id,
                ParentEmail = request.ParentEmail,
                ParentName = request.ParentName,
                ParentPhoneNumber = request.ParentPhoneNumber,
                ChildName = request.ChildName,
                ChildBirthDate = request.ChildBirthDate,
                SubmittedAt = request.SubmittedAt,
                FromKindergardenId = request.FromKindergardenId
            };
        }


        public MatchedRequest Delete(int id)
        {
            MatchedRequest matchedRequest = Get(id);

            SqlCommand deleteMatchedRequest = new SqlCommand($"DELETE FROM matched_request WHERE ID = @id");
            deleteMatchedRequest.Parameters.Add("@id", SqlDbType.Int).Value = id;

            SqlCommand deleteMatchedRequestWishes = new SqlCommand($"DELETE FROM matched_request_wishes WHERE matched_request_id = @id");
            deleteMatchedRequestWishes.Parameters.Add("@id", SqlDbType.Int).Value = id;

            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = _connectionString;
                deleteMatchedRequest.Connection = connection;
                deleteMatchedRequestWishes.Connection = connection;
                connection.Open();

                SqlTransaction transaction = connection.BeginTransaction();
                deleteMatchedRequest.Transaction = transaction;
                deleteMatchedRequestWishes.Transaction = transaction;

                try
                {
                    deleteMatchedRequestWishes.ExecuteNonQuery();
                    deleteMatchedRequest.ExecuteNonQuery();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }

            return matchedRequest;
        }

        public MatchedRequest Get(int id)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _connectionString;
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();

                cmd.CommandText = @"SELECT * FROM matched_request WHERE id=@Id;";
                cmd.Parameters.Add(new SqlParameter("Id", id));

                SqlDataReader reader = cmd.ExecuteReader();

                if (!reader.Read())
                {
                    throw new EntityNotFoundException();
                }

                int idOrd = reader.GetOrdinal("id");
                int submittedAtOrd = reader.GetOrdinal("submitted_at");
                int parentEmailOrd = reader.GetOrdinal("email");
                int parentNameOrd = reader.GetOrdinal("parent_name");
                int parentPhoneNumberOrd = reader.GetOrdinal("phone_number");
                int childNameOrd = reader.GetOrdinal("child_name");
                int childBirthDateOrd = reader.GetOrdinal("child_birth_date");
                int fromKindergardenIdOrd = reader.GetOrdinal("from_kindergarden_id");

                List<int> kindergardenWishIds = GetMatchedWishes(id);

                return new MatchedRequest()
                {
                    Id = reader.GetInt32(idOrd),
                    ChildBirthDate = reader.GetDateTime(childBirthDateOrd),
                    SubmittedAt = reader.GetDateTime(submittedAtOrd),
                    ParentEmail = reader.GetString(parentEmailOrd),
                    ParentName = reader.GetString(parentNameOrd),
                    ParentPhoneNumber = reader.GetString(parentPhoneNumberOrd),
                    ChildName = reader.GetString(childNameOrd),
                    FromKindergardenId = reader.GetInt32(fromKindergardenIdOrd),
                    KindergardenWishIds = kindergardenWishIds
                };
            }
        }

        // helper method
        private List<int> GetMatchedWishes(int id)
        {
            List<int> kindergardenWishIds = new List<int>();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _connectionString;
                conn.Open();

                string newQuery = @"SELECT kindergarden_wish_id from matched_request_wishes WHERE matched_request_id = @id";
                using (SqlCommand command = new SqlCommand(newQuery, conn))
                {
                    command.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            kindergardenWishIds.Add((int)reader["kindergarden_wish_id"]);
                        }
                    }
                }

                return kindergardenWishIds;
            }
        }
    }
}
