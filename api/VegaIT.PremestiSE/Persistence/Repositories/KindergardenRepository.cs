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
        private readonly string _connString = "";
        public IEnumerable<Kindergarden> GetAll()
        {
            List<Kindergarden> kindergardens = new List<Kindergarden>();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.Open();
                conn.ConnectionString = _connString;
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"SELECT * FROM kindergarden;";

                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                DataSet dataSet = new DataSet();

                dataAdapter.SelectCommand = cmd;
                dataAdapter.Fill(dataSet, "kindergardens");

                foreach (DataRow row in dataSet.Tables["kindergardens"].Rows)
                {
                    kindergardens.Add(new Kindergarden
                    {
                        Id = (int)row["id"],
                        Municipality = (string)row["municipality"],
                        Government = (string)row["goverment"],
                        City = (string)row["city"],
                        Name = (string)row["name"],
                        Department = (string)row["department"],
                        Street = (string)row["street"],
                        StreetNumber = (string)row["street_number"],
                        PostalCode = (string)row["postal_code"],
                        LocationType = (int)row["location_type"] == 0 ? LocationType.Base : LocationType.Remote,
                        Longitude = (decimal)row["longitude"],
                        Latitude = (decimal)row["latitude"],
                    });
                }
            }
            return kindergardens;
        }

        public Kindergarden GetById(int id)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.Open();
                conn.ConnectionString = _connString;

                SqlCommand cmd = conn.CreateCommand();

                cmd.CommandText = @"SELECT * FROM kindergardens WHERE id=@Id;";
                cmd.Parameters.Add(new SqlParameter("Id", id));

                SqlDataReader reader = cmd.ExecuteReader();

                if (!reader.Read())
                {
                    throw new EntityNotFoundException();
                }
                int idOrd = reader.GetOrdinal("id");
                int municipalityOrd = reader.GetOrdinal("municipality");
                int governmentOrd = reader.GetOrdinal("goverment");
                int cityOrd = reader.GetOrdinal("city");
                int nameOrd = reader.GetOrdinal("name");
                int departmentOrd = reader.GetOrdinal("department");
                int streetOrd = reader.GetOrdinal("street");
                int streetNumberOrd = reader.GetOrdinal("street_number");
                int postalCodeOrd = reader.GetOrdinal("postal_code");
                int locationTypeOrd = reader.GetOrdinal("location_type");
                int longitudeOrd = reader.GetOrdinal("longitude");
                int latitudeOrd = reader.GetOrdinal("latitude");


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
                    LocationType = reader.GetInt32(locationTypeOrd) == 0 ? LocationType.Base : LocationType.Remote,
                    Longitude = reader.GetDecimal(longitudeOrd),
                    Latitude = reader.GetDecimal(latitudeOrd)
                };
            }
        }
    }
}
