using Microsoft.Data.SqlClient;

namespace EmployeeLeaveManagement
{
    public class LeaveDBData
    {
        private string connectionString =
            "Data Source=.\\SQLEXPRESS; Initial Catalog=EmployeeLeaveManagement; Integrated Security=True; TrustServerCertificate=True;";

        private SqlConnection sqlConnection;

        public LeaveDBData()
        {
            sqlConnection = new SqlConnection(connectionString);
            AddSeeds();
        }

        private void AddSeeds()
        {
            var existing = GetAllLeaves();

            if (existing.Count == 0)
            {
                AddLeave(new LeaveReq
                {
                    EmployeeID = "EMP001",
                    LeaveType = "Vacation",
                    StartDate = "2025-01-01",
                    EndDate = "2025-01-05",
                    Status = "Pending"
                });

                AddLeave(new LeaveReq
                {
                    EmployeeID = "EMP002",
                    LeaveType = "Sick",
                    StartDate = "2025-02-10",
                    EndDate = "2025-02-11",
                    Status = "Approved"
                });
            }
        }

        public void AddLeave(LeaveReq leaveReq)
        {
            var insertStatement =
                "INSERT INTO Leaves VALUES (@LeaveId, @EmployeeId, @LeaveType, @StartDate, @EndDate, @Status)";

            SqlCommand cmd = new SqlCommand(insertStatement, sqlConnection);
            cmd.Parameters.AddWithValue("@LeaveId", Guid.NewGuid());
            cmd.Parameters.AddWithValue("@EmployeeId", leaveReq.EmployeeID);
            cmd.Parameters.AddWithValue("@LeaveType", leaveReq.LeaveType);
            cmd.Parameters.AddWithValue("@StartDate", leaveReq.StartDate);
            cmd.Parameters.AddWithValue("@EndDate", leaveReq.EndDate);
            cmd.Parameters.AddWithValue("@Status", leaveReq.Status);

            sqlConnection.Open();
            cmd.ExecuteNonQuery();
            sqlConnection.Close();
        }

        public List<LeaveReq> GetAllLeaves()
        {
            string selectStatement =
                "SELECT LeaveId, EmployeeId, LeaveType, StartDate, EndDate, Status FROM Leaves";

            SqlCommand cmd = new SqlCommand(selectStatement, sqlConnection);
            sqlConnection.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            var leaves = new List<LeaveReq>();

            while (reader.Read())
            {
                leaves.Add(new LeaveReq
                {
                    RequestID = reader["LeaveId"].ToString(),
                    EmployeeID = reader["EmployeeId"].ToString(),
                    LeaveType = reader["LeaveType"].ToString(),
                    StartDate = reader["StartDate"].ToString(),
                    EndDate = reader["EndDate"].ToString(),
                    Status = reader["Status"].ToString()
                });
            }

            sqlConnection.Close();
            return leaves;
        }

        public bool UpdateLeave(string requestID, string newStatus)
        {
            var updateStatement =
                "UPDATE Leaves SET Status = @Status WHERE LeaveId = @LeaveId";

            SqlCommand cmd = new SqlCommand(updateStatement, sqlConnection);
            cmd.Parameters.AddWithValue("@Status", newStatus);
            cmd.Parameters.AddWithValue("@LeaveId", requestID);

            sqlConnection.Open();
            int rows = cmd.ExecuteNonQuery();
            sqlConnection.Close();

            return rows > 0;
        }

        public bool DeleteLeave(string requestID)
        {
            var deleteStatement = "DELETE FROM Leaves WHERE LeaveId = @LeaveId";

            SqlCommand cmd = new SqlCommand(deleteStatement, sqlConnection);
            cmd.Parameters.AddWithValue("@LeaveId", requestID);

            sqlConnection.Open();
            int rows = cmd.ExecuteNonQuery();
            sqlConnection.Close();

            return rows > 0;
        }
    }
}