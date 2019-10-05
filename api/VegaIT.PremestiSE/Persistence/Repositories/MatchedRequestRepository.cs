using Microsoft.Extensions.Configuration;
using Persistence.Interfaces.Contracts;
using Persistence.Interfaces.Entites;
using Persistence.Interfaces.Entites.Exceptions;
using System;
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

        public MatchedRequest Create(Request request)
        {
            request.SubmittedAt = request.SubmittedAt;

            SqlCommand command = new SqlCommand();
            command.CommandText = $@"INSERT INTO matched_request (parent_email, parent_phone_number, child_name, child_birth_date, from_kindergarden_id, submitted_at) 
                                    VALUES (@email, @phoneNumber, @childName, @childBirthDate, @fromKindergardenId, @submittedAt)
                                    SELECT SCOPE_IDENTITY()";

            command.Parameters.Add("@title", SqlDbType.NVarChar).Value = request.ParentEmail;
            command.Parameters.Add("@childName", SqlDbType.NVarChar).Value = request.ParentPhoneNumber;
            command.Parameters.Add("@childName", SqlDbType.NVarChar).Value = request.ChildName;
            command.Parameters.Add("@childBirthDate", SqlDbType.DateTime).Value = request.ChildBirthDate;
            command.Parameters.Add("@fromKindergardenId", SqlDbType.Int).Value = request.FromKindergardenId;
            command.Parameters.Add("@submittedAt", SqlDbType.NVarChar).Value = request.SubmittedAt.ToString("yyyy-MM-dd HH:mm:ss");


            SqlCommand secondCommand = new SqlCommand();

            var values = new StringBuilder();
            for (var i = 0; i < request.KindergardenWishIds.Count; i++)
            {
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
                connection.Open();

                SqlTransaction transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                secondCommand.Transaction = transaction;

                try
                {
                    int id = (int)command.ExecuteScalar();
                    request.Id = id;

                    secondCommand.Parameters.Add(new SqlParameter("@MatchedRequestId", request.Id));

                    secondCommand.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
            return request as MatchedRequest;
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
                connection.Open();

                SqlTransaction transaction = connection.BeginTransaction();
                deleteMatchedRequest.Transaction = transaction;

                try
                {
                    deleteMatchedRequest.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }

            return matchedRequest;
        }

        // helper
        private MatchedRequest Get(int id)
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
                int parentEmailOrd = reader.GetOrdinal("parent_email");
                int parentNameOrd = reader.GetOrdinal("parent_name");
                int parentPhoneNumberOrd = reader.GetOrdinal("parent_phone_number");
                int childNameOrd = reader.GetOrdinal("child_name");
                int childBirthDateOrd = reader.GetOrdinal("child_birth_date");
                int fromKindergardenIdOrd = reader.GetOrdinal("from_kindergarden_id");

                return new MatchedRequest()
                {
                    Id = reader.GetInt32(idOrd),
                    ChildBirthDate = reader.GetDateTime(childBirthDateOrd),
                    SubmittedAt = reader.GetDateTime(submittedAtOrd),
                    ParentEmail = reader.GetString(parentEmailOrd),
                    ParentName = reader.GetString(parentNameOrd),
                    ParentPhoneNumber = reader.GetString(parentPhoneNumberOrd),
                    ChildName = reader.GetString(childNameOrd),
                    FromKindergardenId = reader.GetInt32(fromKindergardenIdOrd)
                };
            }

        }
    }
}
