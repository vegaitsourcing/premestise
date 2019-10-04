using DataAccessLayer.Contracts.Contracts;
using DataAccessLayer.Contracts.Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DataAccessLayer.Implementation
{
    public class KindergardenRepository : IKindergardenRepository
    {
        private SqlConnection _connection;

        public IEnumerable<Kindergarden> GetAll()
        {
            try
            {
                _connection = Connection.CreateConnection();
                _connection.Open();
                string query = "Select * from Kindergarden";
                SqlCommand command = new SqlCommand(query, _connection);
                List<Kindergarden> kinders = new List<Kindergarden>();
                using(SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Kindergarden k = new Kindergarden();
                        k.Id = (int)reader["Id"];
                        k.Government = reader["Government"].ToString();
                        k.Municipality = reader["Municipality"].ToString();
                        k.City = reader["City"].ToString();
                        k.Name = reader["Name"].ToString();
                        k.Department = reader["Department"].ToString();
                        k.Street = reader["Street"].ToString();
                        k.Street_Number = reader["Street_Number"].ToString();
                        k.PostalCode = (int)reader["Postal_Code"];
                        k.LocationType = (LocationType)reader["Location_Type"];
                        k.Longitude = (decimal)reader["Longitude"];
                        k.Latitude = (decimal)reader["Latitude"];
                        kinders.Add(k);
                    }
                }
                return kinders;
            }
            catch(SqlException ex)
            {
                return null;
            }
            finally
            {
                _connection.Close();
            }
        }

        public Kindergarden GetById(int id)
        {
            _connection = Connection.CreateConnection();
            _connection.Open();
            string query = @"select * from Kindergarden where Id=@id";
            using (SqlCommand command = new SqlCommand(query, _connection))
            {
                Kindergarden k;
                command.Parameters.AddWithValue("@id", id);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        _connection.Close();
                        return null;
                    }
                    k = new Kindergarden();
                    k.Id = (int)reader["Id"];
                    k.Government = reader["Government"].ToString();
                    k.Municipality = reader["Municipality"].ToString();
                    k.City = reader["City"].ToString();
                    k.Name = reader["Name"].ToString();
                    k.Department = reader["Department"].ToString();
                    k.Street = reader["Street"].ToString();
                    k.Street_Number = reader["Street_Number"].ToString();
                    k.PostalCode = (int)reader["Postal_Code"];
                    bool n = (bool)reader["Location_Type"];
                    k.LocationType = n ? LocationType.Base : LocationType.Remote;

                    k.Longitude = (decimal)reader["Longitude"];
                    k.Latitude = (decimal)reader["Latitude"];
                    return k;
                }
            }
        }
    }
}
