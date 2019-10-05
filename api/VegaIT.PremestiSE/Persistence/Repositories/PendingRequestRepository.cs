using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Microsoft.Extensions.Configuration;
using Persistence.Interfaces.Contracts;
using Persistence.Interfaces.Entites;

namespace Persistence.Repositories
{
    public class PendingRequestRepository : IPendingRequestRepository
    {
        private readonly string _connectionString;

        public PendingRequestRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        public PendingRequest Create(PendingRequest request)
        {
            request.SubmittedAt = DateTime.Now;

            SqlCommand command = new SqlCommand();
            command.CommandText = $@"INSERT INTO pending_request (parent_email, parent_phone_number, child_name, child_birth_date, from_kindergarden_id, submitted_at) 
                                    VALUES (@email, @phoneNumber, @childName, @childBirthDate, @fromKindergardenId, @submittedAt)
                                    SELECT SCOPE_IDENTITY()";

            command.Parameters.Add("@email", SqlDbType.NVarChar).Value = request.ParentEmail;
            command.Parameters.Add("@phoneNumber", SqlDbType.NVarChar).Value = request.ParentPhoneNumber;
            command.Parameters.Add("@childName", SqlDbType.NVarChar).Value = request.ChildName;
            command.Parameters.Add("@childBirthDate", SqlDbType.DateTime).Value = request.ChildBirthDate;
            command.Parameters.Add("@fromKindergardenId", SqlDbType.Int).Value = request.FromKindergardenId;
            command.Parameters.Add("@submittedAt", SqlDbType.NVarChar).Value = request.SubmittedAt.ToString("yyyy-MM-dd HH:mm:ss");

            SqlCommand secondCommand = new SqlCommand();
            var values = new StringBuilder();
            for (var i = 0; i < request.KindergardenWishIds.Length; i++)
            {
                values.Append("(@PendingRequestId, @KindergardenWishId" + i + "), ");
            }

            secondCommand.CommandText =
                @"INSERT INTO pending_request_wishes (pending_request_id, kindergarden_wish_id) VALUES" +
                values.ToString();

            for (var i = 0; i < request.KindergardenWishIds.Length; i++)
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

                    secondCommand.Parameters.Add(new SqlParameter("@PendingRequestId", request.Id));

                    secondCommand.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }

            return request;
        }



        public void Delete(int id)
        {
            SqlCommand deletePendingReqCommand = new SqlCommand($"DELETE FROM pending_request WHERE ID = @id");
            deletePendingReqCommand.Parameters.Add("@id", SqlDbType.Int).Value = id;

            SqlCommand deletePendingRequestWishes = new SqlCommand($"DELETE FROM pending_request_wishes WHERE pending_request_id = @id");
            deletePendingRequestWishes.Parameters.Add("@id", SqlDbType.Int).Value = id;

            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();

                SqlTransaction transaction = connection.BeginTransaction();
                deletePendingReqCommand.Transaction = transaction;
                deletePendingRequestWishes.Transaction = transaction;

                try
                {
                    deletePendingReqCommand.ExecuteNonQuery();
                    deletePendingRequestWishes.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public IEnumerable<PendingRequest> GetAllMatchesFor(PendingRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}
