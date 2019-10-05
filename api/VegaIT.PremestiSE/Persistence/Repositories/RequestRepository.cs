using Persistence.Interfaces.Contracts;
using Persistence.Interfaces.Entites;
using Persistence.Interfaces.Entites.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Persistence.Repositories
{
    public class RequestRepository<T> where T : Request
    {
        private readonly string _connString = "";
        private readonly IKindergardenRepository _kindergardenRepository = new KindergardenRepository();

        public T Create(T request)
        {
            string tableName = typeof(T).Name.ToLower().StartsWith("pend") ? "pending_request" : "matched_request";
            request.SubmittedAt = DateTime.Now;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _connString;
                conn.Open();

                DateTime matched_at = DateTime.Now;

                SqlTransaction transaction = conn.BeginTransaction();

                SqlCommand cmd = conn.CreateCommand();
                cmd.Transaction = transaction;
                cmd.CommandText = $@"INSERT INTO {tableName} (from_kindergarden_id, submitted_at, parent_email, parent_name, parent_phone_number, child_name, child_birth_date) " +
                    "VALUES (@FromKindergarden, @SubmittedAt, @ParentEmail, @ParentName, @ParentPhoneNumber, @ChildName, @ChildBirthDate);";
                cmd.Parameters.Add(new SqlParameter("@FromKindergarden", request.FromKindergarden.Id));
                cmd.Parameters.Add(new SqlParameter("@SubmittedAt", request.SubmittedAt.ToString("yyyy-MM-dd HH:mm:ss")));
                cmd.Parameters.Add(new SqlParameter("@ParentEmail", request.ParentEmail));
                cmd.Parameters.Add(new SqlParameter("@ParentName", request.ParentName));
                cmd.Parameters.Add(new SqlParameter("@ParentPhoneNumber", request.ParentPhoneNumber));
                cmd.Parameters.Add(new SqlParameter("@ChildName", request.ChildName));
                cmd.Parameters.Add(new SqlParameter("@ChildBirthDate", request.ChildBirthDate.ToString("yyyy-MM-dd HH:mm:ss")));
                cmd.ExecuteNonQuery();

                SqlCommand cmdGetId = conn.CreateCommand();
                cmdGetId.CommandText = @"SELECT CONVERT(int, IDENT_CURRENT('pending_request')) AS id;";
                cmdGetId.Transaction = transaction;

                try
                {
                    int id = (int)cmdGetId.ExecuteScalar();
                    transaction.Commit();
                    request.Id = id;
                    return request;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public void Delete(T request)
        {
            string tableName = typeof(T).Name.ToLower().StartsWith("pend") ? "pending_request" : "matched_request";
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _connString;
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = $@"DELETE FROM {tableName} WHERE id=@Id;";
                cmd.Parameters.Add(new SqlParameter("@Id", request.Id));

                cmd.ExecuteNonQuery();
            }
        }

        public T GetById(int id)
        {
            string tableName = typeof(T).Name.ToLower().StartsWith("pend") ? "pending_request" : "matched_request";
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _connString;
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();

                cmd.CommandText = $@"SELECT * FROM {tableName} WHERE id=@Id;";
                cmd.Parameters.Add(new SqlParameter("Id", id));

                SqlDataReader reader = cmd.ExecuteReader();

                if (!reader.Read())
                {
                    throw new EntityNotFoundException();
                }

                int idOrd = reader.GetOrdinal("id");
                int kindergardenOrd = reader.GetOrdinal("from_kindergarden_id");
                int submittedAtOrd = reader.GetOrdinal("submitted_at");
                int parentEmailOrd = reader.GetOrdinal("parent_email");
                int parentNameOrd = reader.GetOrdinal("parent_name");
                int parentPhoneNumberOrd = reader.GetOrdinal("parent_phone_number");
                int childNameOrd = reader.GetOrdinal("child_name");
                int childBirthDateOrd = reader.GetOrdinal("child_birth_date");


                return (T) new Request
                {
                    Id = reader.GetInt32(idOrd),
                    FromKindergarden = _kindergardenRepository.GetById(reader.GetInt32(kindergardenOrd)),
                    SubmittedAt = reader.GetDateTime(submittedAtOrd),
                    ParentEmail = reader.GetString(parentEmailOrd),
                    ParentName = reader.GetString(parentNameOrd),
                    ParentPhoneNumber = reader.GetString(parentPhoneNumberOrd),
                    ChildName = reader.GetString(childNameOrd),
                    ChildBirthDate = reader.GetDateTime(childBirthDateOrd)
                };
            }
        }
    }
}
