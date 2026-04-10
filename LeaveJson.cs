using EmployeeLeaveManagement;
using System.Text.Json;

namespace EmployeeLeaveManagement
{
    public class LeaveJson
    {
        private List<LeaveReq> leaves = new List<LeaveReq>();
        private List<EmployeePoint> points = new List<EmployeePoint>();
        private string _leavesFileName;
        private string _pointsFileName;

        public LeaveJson()
        {
            _leavesFileName = $"{AppDomain.CurrentDomain.BaseDirectory}/leaves.json";
            _pointsFileName = $"{AppDomain.CurrentDomain.BaseDirectory}/points.json";
        }

        private void SaveLeavesToJsonFile()
        {
            using (var outputStream = File.OpenWrite(_leavesFileName))
            {
                JsonSerializer.Serialize<List<LeaveReq>>(
                    new Utf8JsonWriter(outputStream, new JsonWriterOptions
                    { SkipValidation = true, Indented = true })
                    , leaves);
            }
        }

        private void RetrieveLeavesFromJsonFile()
        {
            if (!File.Exists(_leavesFileName)) return;
            using (var jsonFileReader = File.OpenText(_leavesFileName))
            {
                leaves = JsonSerializer.Deserialize<List<LeaveReq>>
                    (jsonFileReader.ReadToEnd(), new JsonSerializerOptions
                    { PropertyNameCaseInsensitive = true })
                    .ToList();
            }
        }

        private void SavePointsToJsonFile()
        {
            using (var outputStream = File.OpenWrite(_pointsFileName))
            {
                JsonSerializer.Serialize<List<EmployeePoint>>(
                    new Utf8JsonWriter(outputStream, new JsonWriterOptions
                    { SkipValidation = true, Indented = true })
                    , points);
            }
        }

        private void RetrievePointsFromJsonFile()
        {
            if (!File.Exists(_pointsFileName)) return;
            using (var jsonFileReader = File.OpenText(_pointsFileName))
            {
                points = JsonSerializer.Deserialize<List<EmployeePoint>>
                    (jsonFileReader.ReadToEnd(), new JsonSerializerOptions
                    { PropertyNameCaseInsensitive = true })
                    .ToList();
            }
        }

        public List<LeaveReq> GetAll()
        {
            RetrieveLeavesFromJsonFile();
            return leaves;
        }

        public LeaveReq? GetById(Guid id)
        {
            RetrieveLeavesFromJsonFile();
            return leaves.FirstOrDefault(x => x.LeaveId == id);
        }

        public void Add(LeaveReq leave)
        {
            RetrieveLeavesFromJsonFile();
            leaves.Add(leave);
            SaveLeavesToJsonFile();
        }

        public void Update(LeaveReq leave)
        {
            RetrieveLeavesFromJsonFile();
            var existing = leaves.FirstOrDefault(x => x.LeaveId == leave.LeaveId);
            if (existing != null)
            {
                existing.EmployeeName = leave.EmployeeName;
                existing.LeaveType = leave.LeaveType;
                existing.StartDate = leave.StartDate;
                existing.EndDate = leave.EndDate;
                existing.Status = leave.Status;
                existing.Reason = leave.Reason;
                existing.PointsDeducted = leave.PointsDeducted;
            }
            SaveLeavesToJsonFile();
        }

        public void Delete(Guid id)
        {
            RetrieveLeavesFromJsonFile();
            var existing = leaves.FirstOrDefault(x => x.LeaveId == id);
            if (existing != null)
                leaves.Remove(existing);
            SaveLeavesToJsonFile();
        }

        public int GetPoints(int employeeId)
        {
            RetrievePointsFromJsonFile();
            var emp = points.FirstOrDefault(x => x.EmployeeId == employeeId);
            return emp != null ? emp.Points : 30;
        }

        public void UpdatePoints(int employeeId, int newPoints)
        {
            RetrievePointsFromJsonFile();
            var emp = points.FirstOrDefault(x => x.EmployeeId == employeeId);
            if (emp != null)
                emp.Points = newPoints;
            else
                points.Add(new EmployeePoint { EmployeeId = employeeId, Points = newPoints });
            SavePointsToJsonFile();
        }
    }
}