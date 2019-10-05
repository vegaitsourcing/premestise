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

        public PendingRequest Create(PendingRequest request)
        {
            request.SubmittedAt = DateTime.Now;

            SqlCommand command = new SqlCommand();
            command.CommandText = $@"INSERT INTO pending_request (parent_email, parent_phone_number, child_name, child_birth_date, from_kindergarden_id, submitted_at, verified) 
                                    VALUES (@email, @phoneNumber, @childName, @childBirthDate, @fromKindergardenId, @submittedAt, @verified)
                                    SELECT SCOPE_IDENTITY()";

            command.Parameters.Add("@email", SqlDbType.NVarChar).Value = request.ParentEmail;
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
                values.Append("(@PendingRequestId, @KindergardenWishId" + i + "), ");
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

        //public IEnumerable<PendingRequest> GetAll()
        //{
        //    List<Kindergarden> kindergardens = new List<Kindergarden>();
        //    using (SqlConnection conn = new SqlConnection())
        //    {
        //        conn.ConnectionString = _connectionString;
        //        conn.Open();
        //        SqlCommand cmd = conn.CreateCommand();
        //        cmd.CommandText = @"SELECT * FROM Pending_Request;";

        //        using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
        //        {
        //            DataSet dataSet = new DataSet();

        //            dataAdapter.SelectCommand = cmd;
        //            dataAdapter.Fill(dataSet, "Kindergarden");

        //            foreach (DataRow row in dataSet.Tables["Kindergarden"].Rows)
        //            {
        //                kindergardens.Add(new Kindergarden
        //                {
        //                    Id = (int)row["id"],
        //                    Municipality = (string)row["municipality"],
        //                    Government = (string)row["government"],
        //                    City = (string)row["city"],
        //                    Name = (string)row["name"],
        //                    Department = (string)row["department"],
        //                    Street = (string)row["street"],
        //                    StreetNumber = (string)row["street_number"],
        //                    PostalCode = (string)row["postal_code"],
        //                    LocationType = (bool)row["location_type"] ? LocationType.Base : LocationType.Remote,
        //                    Longitude = row["longitude"] == System.DBNull.Value ? null : (decimal?)row["longitude"],
        //                    Latitude = row["latitude"] == System.DBNull.Value ? null : (decimal?)row["latitude"],
        //                });
        //            }
        //        }
        //    }
        //    return kindergardens;
        //}

        public IEnumerable<PendingRequest> GetAll()
        {
            throw new NotImplementedException();
        }

        public PendingRequest Get(int id)
        {

            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();
                string query = "select * from pending_request where id =@id";
                PendingRequest pending;
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
                    pending.ParentPhoneNumber = reader["phone-number"].ToString();
                    pending.ChildName = reader["child_name"].ToString();
                    pending.ChildBirthDate = (DateTime)reader["child_birth_date"];


                }

                string newQuery = @"select kindergarden_wish_id from pending_request_wishes where pending_request_id =@id";
                using (SqlCommand command = new SqlCommand(newQuery, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    pending.KindergardenWishIds = new List<int>();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            pending.KindergardenWishIds.Add((int)reader["kindergarden_wish_id"]);
                        }
                    }
                }
                return pending;
            }


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
                cmd.CommandText = @"SELECT PR.id FROM pending_request AS PR INNER JOIN pending_request_wishes AS Wishes"
                                + @" ON Pr.id == Wishes.pending_request_id "
                                + @" WHERE Wishes.kindergarden_wish_id = @fromKindergardenId AND Pr.from_kindergarden_id IN ("
                                + toValues + ") AND verified = 1;";

                for (var i = 0; i < request.KindergardenWishIds.Count; i++)
                {
                    cmd.Parameters.Add("@From" + i, SqlDbType.Int).Value = request.KindergardenWishIds[i];
                }

                cmd.Parameters.Add(new SqlParameter("fromKindergardenId", request.FromKindergardenId));

                SqlDataReader reader = cmd.ExecuteReader();

                if (!reader.Read())
                {
                    throw new EntityNotFoundException();
                }

                List<int> pendingRequestIds = new List<int>();
                while (reader.Read())
                {
                    pendingRequestIds.Add((int)reader["id"]);
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

                string query = @"update table pending_request set verified= @verified where id=@id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@verified", true);
                    command.Parameters.AddWithValue("@id", id);
                }

            }
        }
    }
}
