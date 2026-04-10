using System;
using Microsoft.Data.SqlClient;

namespace EmployeeLeaveManagement
{
    public class LeaveDBData : LeaveRep
    {
        private string connectionString = "Server=DESKTOP-0OM4UEV\\SQLEXPRESS;Database=EmployeeLeaveManagement;Integrated Security=True;TrustServerCertificate=True;";

        public LeaveReq? GetById(Guid id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Leaves WHERE LeaveId = @LeaveId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@LeaveId", id);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new LeaveReq
                    {
                        LeaveId = (Guid)reader["LeaveId"],
                        EmployeeId = (int)reader["EmployeeId"],
                        EmployeeName = reader["EmployeeName"].ToString(),
                        LeaveType = (LeaveType)Enum.Parse(typeof(LeaveType), reader["LeaveType"].ToString()),
                        StartDate = (DateTime)reader["StartDate"],
                        EndDate = (DateTime)reader["EndDate"],
                        PointsDeducted = (int)reader["PointsDeducted"],
                        Status = (LeaveStatus)Enum.Parse(typeof(LeaveStatus), reader["Status"].ToString()),
                        Reason = reader["Reason"].ToString()
                    };
                }
                return null;
            }
        }

        public List<LeaveReq> GetAll()
        {
            List<LeaveReq> results = new List<LeaveReq>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM Leaves";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        results.Add(new LeaveReq
                        {
                            LeaveId = Guid.Parse(reader["LeaveId"].ToString()),
                            EmployeeId = Convert.ToInt32(reader["EmployeeId"]),
                            EmployeeName = reader["EmployeeName"].ToString(),
                            LeaveType = (LeaveType)Enum.Parse(typeof(LeaveType), reader["LeaveType"].ToString()),
                            StartDate = Convert.ToDateTime(reader["StartDate"]),
                            EndDate = Convert.ToDateTime(reader["EndDate"]),
                            PointsDeducted = Convert.ToInt32(reader["PointsDeducted"]),
                            Status = (LeaveStatus)Enum.Parse(typeof(LeaveStatus), reader["Status"].ToString()),
                            Reason = reader["Reason"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sa GetAll: {ex.Message}");
            }
            return results;
        }

        public void Add(LeaveReq Leaves)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var insert = "INSERT INTO Leaves (EmployeeId, EmployeeName, LeaveType, StartDate, EndDate, Status, PointsDeducted, Reason) VALUES (@EmployeeId, @EmployeeName, @LeaveType, @StartDate, @EndDate, @Status, @PointsDeducted, @Reason)";
                SqlCommand cmd = new SqlCommand(insert, conn);
                cmd.Parameters.AddWithValue("@EmployeeId", Leaves.EmployeeId);
                cmd.Parameters.AddWithValue("@EmployeeName", Leaves.EmployeeName);
                cmd.Parameters.AddWithValue("@LeaveType", Leaves.LeaveType.ToString());
                cmd.Parameters.AddWithValue("@StartDate", Leaves.StartDate);
                cmd.Parameters.AddWithValue("@EndDate", Leaves.EndDate);
                cmd.Parameters.AddWithValue("@Status", Leaves.Status.ToString());
                cmd.Parameters.AddWithValue("@PointsDeducted", Leaves.PointsDeducted);
                cmd.Parameters.AddWithValue("@Reason", Leaves.Reason);
                cmd.ExecuteNonQuery();
            }
        }

        public void Edit(LeaveReq Leaves)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var update = "UPDATE Leaves SET Status = @Status WHERE LeaveId = @LeaveId";
                SqlCommand cmd = new SqlCommand(update, conn);
                cmd.Parameters.AddWithValue("@Status", Leaves.Status.ToString());
                cmd.Parameters.AddWithValue("@LeaveId", Leaves.LeaveId);
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(Guid id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var delete = "DELETE FROM Leaves WHERE LeaveId = @LeaveId";
                SqlCommand cmd = new SqlCommand(delete, conn);
                cmd.Parameters.AddWithValue("@LeaveId", id);
                cmd.ExecuteNonQuery();
            }
        }

        public int GetPoints(int employeeId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var getPoints = "SELECT Points FROM EmployeePoints WHERE EmployeeId = @EmployeeId";
                SqlCommand cmd = new SqlCommand(getPoints, conn);
                cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                    return (int)reader["Points"];
                return 0;
            }
        }

        public void UpdatePoints(int employeeId, int points)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var update = "UPDATE EmployeePoints SET Points = @Points WHERE EmployeeId = @EmployeeId";
                SqlCommand cmd = new SqlCommand(update, conn);
                cmd.Parameters.AddWithValue("@Points", points);
                cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                cmd.ExecuteNonQuery();
            }
        }
    }
}