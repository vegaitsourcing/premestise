using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Microsoft.Extensions.Configuration;
using Persistence.Interfaces.Contracts;
using Persistence.Interfaces.Entites;
using Persistence.Interfaces.Entites.Exceptions;

namespace Persistence.Repositories
{
    public class PendingRequestRepository : IPendingRequestRepository
    {
        private readonly string _connectionString;

        public PendingRequestRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        public PendingRequest Create(Request request)
        {
            request.SubmittedAt = DateTime.Now;

            SqlCommand command = new SqlCommand();
            command.CommandText = $@"INSERT INTO pending_request (email, parent_name, phone_number, child_name, child_birth_date, from_kindergarden_id, submitted_at, verified) 
                                    VALUES (@email, @parentName, @phoneNumber, @childName, @childBirthDate, @fromKindergardenId, @submittedAt, @verified);
                                    SELECT SCOPE_IDENTITY()";

            command.Parameters.Add("@email", SqlDbType.NVarChar).Value = request.ParentEmail;
            command.Parameters.Add("@parentName", SqlDbType.NVarChar).Value = request.ParentName;
            command.Parameters.Add("@phoneNumber", SqlDbType.NVarChar).Value = request.ParentPhoneNumber;
            command.Parameters.Add("@childName", SqlDbType.NVarChar).Value = request.ChildName;
            command.Parameters.Add("@childBirthDate", SqlDbType.DateTime).Value = request.ChildBirthDate;
            command.Parameters.Add("@fromKindergardenId", SqlDbType.Int).Value = request.FromKindergardenId;
            command.Parameters.Add("@submittedAt", SqlDbType.NVarChar).Value = request.SubmittedAt.ToString("yyyy-MM-dd HH:mm:ss");
            command.Parameters.AddWithValue("@verified", false);

            SqlCommand secondCommand = new SqlCommand();
            var values = new StringBuilder();
            for (var i = 0; i < request.KindergardenWishIds.Count; i++)
            {
                if (i == request.KindergardenWishIds.Count - 1)
                    values.Append("(@PendingRequestId, @KindergardenWishId" + i + ")");
                else
                {
                    values.Append("(@PendingRequestId, @KindergardenWishId" + i + "), ");
                }
            }

            secondCommand.CommandText =
                @"INSERT INTO pending_request_wishes (pending_request_id, kindergarden_wish_id) VALUES" +
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

                    secondCommand.Parameters.Add(new SqlParameter("@PendingRequestId", request.Id));

                    secondCommand.ExecuteNonQuery();

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }

            return new PendingRequest
            {
                Id = request.Id,
                ParentEmail = request.ParentEmail,
                ParentName = request.ParentName,
                ParentPhoneNumber = request.ParentPhoneNumber,
                ChildName = request.ChildName,
                ChildBirthDate = request.ChildBirthDate,
                KindergardenWishIds = request.KindergardenWishIds,
                FromKindergardenId = request.FromKindergardenId,
                Verified = false,
                SubmittedAt = request.SubmittedAt
            };
        }


        public PendingRequest GetLatest()
        {
            PendingRequest pendingRequest = null;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _connectionString;
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"SELECT TOP 1 * FROM pending_request WHERE verified = 1 ORDER BY submitted_at DESC;";

                using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
                {
                    DataSet dataSet = new DataSet();

                    dataAdapter.SelectCommand = cmd;
                    dataAdapter.Fill(dataSet, "pending_request");

                    foreach (DataRow row in dataSet.Tables["pending_request"].Rows)
                    {
                        pendingRequest = new PendingRequest
                        {
                            Id = (int)row["id"],
                            FromKindergardenId = (int)row["from_kindergarden_id"],
                            SubmittedAt = row["submitted_at"] == System.DBNull.Value ? DateTime.Now : (DateTime)row["submitted_at"],
                            ParentEmail = row["email"] == System.DBNull.Value ? null : (string)row["email"],
                            ParentName = row["parent_name"] == System.DBNull.Value ? null : (string)row["parent_name"],
                            ParentPhoneNumber = row["phone_number"] == System.DBNull.Value ? null : (string)row["phone_number"],
                            ChildName = row["child_name"] == System.DBNull.Value ? null : (string)row["child_name"],
                            ChildBirthDate = row["child_birth_date"] == System.DBNull.Value ? DateTime.Now : (DateTime)row["child_birth_date"],
                            Verified = row["verified"] != System.DBNull.Value && (bool)row["verified"],

                            KindergardenWishIds = GetPendingWishes((int)row["id"])
                        };
                    }
                }

            }
            return pendingRequest;
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
                deletePendingReqCommand.Connection = connection;
                deletePendingRequestWishes.Connection = connection;
                connection.Open();

                SqlTransaction transaction = connection.BeginTransaction();
                deletePendingReqCommand.Transaction = transaction;
                deletePendingRequestWishes.Transaction = transaction;

                try
                {
                    deletePendingRequestWishes.ExecuteNonQuery();
                    deletePendingReqCommand.ExecuteNonQuery();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public IEnumerable<PendingRequest> GetAll()
        {
            List<PendingRequest> pendingRequests = new List<PendingRequest>();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _connectionString;
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"SELECT * FROM pending_request;";

                using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
                {
                    DataSet dataSet = new DataSet();

                    dataAdapter.SelectCommand = cmd;
                    dataAdapter.Fill(dataSet, "pending_request");

                    foreach (DataRow row in dataSet.Tables["pending_request"].Rows)
                    {
                        pendingRequests.Add(new PendingRequest
                        {
                            Id = (int)row["id"],
                            FromKindergardenId = (int)row["from_kindergarden_id"],
                            SubmittedAt = row["submitted_at"] == System.DBNull.Value ? DateTime.Now : (DateTime)row["submitted_at"],
                            ParentEmail = row["email"] == System.DBNull.Value ? null : (string)row["email"],
                            ParentName = row["parent_name"] == System.DBNull.Value ? null : (string)row["parent_name"],
                            ParentPhoneNumber = row["phone_number"] == System.DBNull.Value ? null : (string)row["phone_number"],
                            ChildName = row["child_name"] == System.DBNull.Value ? null : (string)row["child_name"],
                            ChildBirthDate = row["child_birth_date"] == System.DBNull.Value ? DateTime.Now : (DateTime)row["child_birth_date"],
                            Verified = row["verified"] != System.DBNull.Value && (bool)row["verified"],

                            KindergardenWishIds = GetPendingWishes((int)row["id"])
                        });
                    }
                }

            }
            return pendingRequests;
        }

        private List<int> GetPendingWishes(int id)
        {
            List<int> kindergardenWishIds = new List<int>();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _connectionString;
                conn.Open();

                string newQuery = @"SELECT kindergarden_wish_id from pending_request_wishes WHERE pending_request_id = @id";
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

        public PendingRequest Get(int id)
        {
            PendingRequest pending;
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();
                string query = "select * from pending_request where id =@id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = command.ExecuteReader();
                    if (!reader.Read())
                    {
                        throw new EntityNotFoundException();
                    }
                    pending = new PendingRequest();
                    pending.Id = (int)reader["id"];
                    pending.SubmittedAt = (DateTime)reader["submitted_at"];
                    pending.ParentEmail = reader["email"].ToString();
                    pending.ParentName = reader["parent_name"].ToString();
                    pending.FromKindergardenId = (int)reader["from_kindergarden_id"];
                    pending.ParentPhoneNumber = reader["phone_number"].ToString();
                    pending.ChildName = reader["child_name"].ToString();
                    pending.ChildBirthDate = (DateTime)reader["child_birth_date"];
                    pending.KindergardenWishIds = GetPendingWishes(id);

                }

            }

            return pending;
        }

        public IEnumerable<PendingRequest> GetAllMatchesFor(PendingRequest request)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _connectionString;

                conn.Open();

                SqlCommand cmd = conn.CreateCommand();

                StringBuilder toValues = new StringBuilder();
                for (var i = 0; i < request.KindergardenWishIds.Count; i++)
                {
                    if (i < request.KindergardenWishIds.Count - 1)
                    {
                        toValues.Append("@From" + i + ", ");

                    }
                    else
                    {
                        toValues.Append("@From" + i);
                    }

                }
                //cmd.CommandText = @"SELECT PR.id FROM pending_request AS PR INNER JOIN pending_request_wishes AS Wishes"
                //                + @" ON PR.id = Wishes.pending_request_id "
                //                + @"WHERE PR.verified = 1"
                //                + @" AND Wishes.kindergarden_wish_id = @fromKindergardenId AND PR.from_kindergarden_id IN ("
                //                + toValues + ") ;";

                cmd.CommandText = @"SELECT PR.id FROM pending_request AS PR
                                    INNER JOIN pending_request_wishes AS WISHES
                                    ON PR.id = WISHES.pending_request_id
                                    WHERE PR.verified = 1
                                    AND WISHES.kindergarden_wish_id = @fromKindergardenId
                                    AND PR.from_kindergarden_id IN(" + toValues + ");";

                for (var i = 0; i < request.KindergardenWishIds.Count; i++)
                {
                    cmd.Parameters.Add(new SqlParameter("@From" + i, request.KindergardenWishIds[i]));
                }

                cmd.Parameters.Add(new SqlParameter("@fromKindergardenId", request.FromKindergardenId));

                List<int> pendingRequestIds = new List<int>();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        pendingRequestIds.Add((int)reader["id"]);
                    }
                }

                List<PendingRequest> pendingRequests = new List<PendingRequest>(pendingRequestIds.Count);


                foreach (var id in pendingRequestIds)
                {
                    pendingRequests.Add(Get(id));
                }

                return pendingRequests;

            }
        }

        public void Verify(int id)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();

                string query = @"update pending_request set verified= @verified where id=@id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@verified", true);
                    command.Parameters.AddWithValue("@id", id);

                    command.ExecuteNonQuery();
                }

            }
        }
    }
}
