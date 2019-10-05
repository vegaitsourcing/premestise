using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Persistence.Interfaces.Contracts;
using Persistence.Interfaces.Entites;

namespace Persistence.Repositories
{
    public class PendingRequestRepository : RequestRepository<PendingRequest>, IPendingRequestRepository
    {
        private readonly string _connString = "";
        private readonly IKindergardenRepository _kindergardenRepository;

        public PendingRequestRepository(IKindergardenRepository kindergardenRepository) : base(kindergardenRepository)
        {
            _kindergardenRepository = kindergardenRepository;
        }

        public List<PendingRequest> GetAllMatchesForRequest(PendingRequest request)
        {
            List<PendingRequest> pendingRequests = new List<PendingRequest>();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _connString;
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"SELECT * FROM pending_request a, pending_request b WHERE a.from_kindergarden = b.to_kindergarden AND a.to_kindergarden = b.from_kindergarden;";

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
                            FromKindergarden = _kindergardenRepository.GetById((int)row["from_kindergarden_id"]),
                            SubmittedAt = (DateTime)row["submitted_at"],
                            ParentEmail = (string)row["parent_email"],
                            ParentName = (string)row["parent_name"],
                            ParentPhoneNumber = (string)row["parent_phone_number"],
                            ChildName = (string)row["child_name"],
                            ChildBirthDate = (DateTime)row["child_birth_date"]
                        });
                    }
                }
            }
            return pendingRequests;
        }
    }
}
