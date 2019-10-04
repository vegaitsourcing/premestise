using DataAccessLayer.Contracts.Contracts;
using DataAccessLayer.Contracts.Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class RequestRepository : IRequestRepository
    {
        private SqlConnection _connection;

        public IEnumerable<Request> GetAll()
        {
            try
            {
                _connection = Connection.CreateConnection();
                _connection.Open();
                string query = "select * from Request";
                SqlCommand command = new SqlCommand(query, _connection);
                List<Request> requests = new List<Request>();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Request r = new Request();
                        r.Id = (int)reader["Id"];
                        r.ParentName = reader["Parent_Name"].ToString();
                        r.ChildName = reader["Child_Name"].ToString();
                        r.PhoneNumber = reader["Phone_Number"].ToString();
                        r.Email = reader["Email"].ToString();
                        r.Age = (int)reader["Age_Group"];
                        r.Date = (DateTime)reader["Date_Created"];
                        requests.Add(r);
                    }
                }
                return requests;
            }
            catch (SqlException ex)
            {
                return null;
            }
            finally
            {
                _connection.Close();
            }
        }

        public Request GetById(int id)
        {
            _connection = Connection.CreateConnection();
            _connection.Open();
            string query = @"select * from Request where Id=@id";
            using(SqlCommand command = new SqlCommand(query, _connection))
            {
                Request request;
                command.Parameters.AddWithValue("@id", id);
                using(SqlDataReader reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        _connection.Close();
                        return null;
                    }
                    request = new Request();
                    request.Id = (int)reader["Id"];
                    request.ParentName = reader["Parent_Name"].ToString();
                    request.ChildName = reader["Child_Name"].ToString();
                    request.PhoneNumber = reader["Phone_Number"].ToString();
                    request.Email = reader["Email"].ToString();
                    request.Age = (int)reader["Age_Group"];
                    request.Date = (DateTime)reader["Date_Created"];
                    _connection.Close();
                    return request;
                }
            }
        }
    }
}
