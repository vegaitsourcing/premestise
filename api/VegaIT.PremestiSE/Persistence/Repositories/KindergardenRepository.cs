using System;
using Microsoft.Extensions.Configuration;
using Persistence.Interfaces.Contracts;
using Persistence.Interfaces.Entites;
using Persistence.Interfaces.Entites.Exceptions;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Persistence.Repositories
{
    public class KindergardenRepository : IKindergardenRepository
    {
        private readonly string _connString;

        public KindergardenRepository(IConfiguration config)
        {
            _connString = config.GetConnectionString("DefaultConnection");

        }

        public List<Kindergarden> GetAll()
        {
            List<Kindergarden> kindergardens = new List<Kindergarden>();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _connString;
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"SELECT * FROM kindergarden;";

                using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
                {
                    DataSet dataSet = new DataSet();

                    dataAdapter.SelectCommand = cmd;
                    dataAdapter.Fill(dataSet, "kindergarden");

                    foreach (DataRow row in dataSet.Tables["kindergarden"].Rows)
                    {
                        kindergardens.Add(new Kindergarden
                        {
                            Id = (int)row["id"],
                            Municipality = (string)row["municipality"],
                            Government = (string)row["government"],
                            City = (string)row["city"],
                            Name = (string)row["name"],
                            Department = (string)row["department"],
                            Street = (string)row["street"],
                            StreetNumber = (string)row["street_number"],
                            PostalCode = (string)row["postal_code"],
                            LocationType = (bool)row["location_type"] ? LocationType.Base : LocationType.Remote,
                            Longitude = row["longitude"] == System.DBNull.Value ? null : (decimal?)row["longitude"],
                            Latitude = row["latitude"] == System.DBNull.Value ? null : (decimal?)row["latitude"],
                        });
                    }
                }
            }
            return kindergardens;
        }

        public Kindergarden GetById(int id)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _connString;
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();

                cmd.CommandText = @"SELECT * FROM kindergarden WHERE id=@Id;";
                cmd.Parameters.Add(new SqlParameter("Id", id));

                SqlDataReader reader = cmd.ExecuteReader();

                if (!reader.Read())
                {
                    throw new EntityNotFoundException();
                }
                int idOrd = reader.GetOrdinal("id");
                int municipalityOrd = reader.GetOrdinal("municipality");
                int governmentOrd = reader.GetOrdinal("government");
                int cityOrd = reader.GetOrdinal("city");
                int nameOrd = reader.GetOrdinal("name");
                int departmentOrd = reader.GetOrdinal("department");
                int streetOrd = reader.GetOrdinal("street");
                int streetNumberOrd = reader.GetOrdinal("street_number");
                int postalCodeOrd = reader.GetOrdinal("postal_code");
                int locationTypeOrd = reader.GetOrdinal("location_type");
                int longitudeOrd = reader.GetOrdinal("longitude");
                int latitudeOrd = reader.GetOrdinal("latitude");

                decimal? longitude;
                decimal? latitude;

                try
                {
                    longitude = reader.GetDecimal(longitudeOrd);
                    latitude = reader.GetDecimal(latitudeOrd);
                }
                catch (Exception)
                {
                    longitude = null;
                    latitude = null;
                }


                return new Kindergarden
                {
                    Id = reader.GetInt32(idOrd),
                    Municipality = reader.GetString(municipalityOrd),
                    Government = reader.GetString(governmentOrd),
                    City = reader.GetString(cityOrd),
                    Name = reader.GetString(nameOrd),
                    Department = reader.GetString(departmentOrd),
                    Street = reader.GetString(streetOrd),
                    StreetNumber = reader.GetString(streetNumberOrd),
                    PostalCode = reader.GetString(postalCodeOrd),
                    LocationType = reader.GetBoolean(locationTypeOrd) == false ? LocationType.Base : LocationType.Remote,
                    Longitude = longitude,
                    Latitude = latitude
                };
            }
        }

        public List<Kindergarden> GetToByRequestId(int id)
        {
            List<Kindergarden> kindergardens = new List<Kindergarden>();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _connString;
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"SELECT * FROM kindergarden WHERE id IN (SELECT kindergarden_wish_id FROM pending_request_wishes WHERE pending_request_id=@id);";
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;

                using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
                {
                    DataSet dataSet = new DataSet();

                    dataAdapter.SelectCommand = cmd;
                    dataAdapter.Fill(dataSet, "kindergarden");

                    foreach (DataRow row in dataSet.Tables["kindergarden"].Rows)
                    {
                        kindergardens.Add(new Kindergarden
                        {
                            Id = (int)row["id"],
                            Municipality = (string)row["municipality"],
                            Government = (string)row["government"],
                            City = (string)row["city"],
                            Name = (string)row["name"],
                            Department = (string)row["department"],
                            Street = (string)row["street"],
                            StreetNumber = (string)row["street_number"],
                            PostalCode = (string)row["postal_code"],
                            LocationType = (bool)row["location_type"] ? LocationType.Remote : LocationType.Base,
                            Longitude = row["longitude"] == System.DBNull.Value ? null : (decimal?)row["longitude"],
                            Latitude = row["latitude"] == System.DBNull.Value ? null : (decimal?)row["latitude"],
                        });
                    }
                }
            }
            return kindergardens;
        }
    }
}
