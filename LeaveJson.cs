using System.Text.Json;


namespace EmployeeLeaveManagement
{
    public class LeaveJson
    {
        private List<LeaveReq> leaveRequests = new List<LeaveReq>();
        private string _jsonFileName;

        public LeaveJson()
        {
            _jsonFileName = $"{AppDomain.CurrentDomain.BaseDirectory}/LeaveRequests.json";
            PopulateJsonFile();
        }

        private void PopulateJsonFile()
        {
            RetrieveDataFromJsonFile(); // basahin muna yung file

            if (leaveRequests.Count <= 0) // kung walang laman, lagyan ng sample
            {
                leaveRequests.Add(new LeaveReq { RequestID = Guid.NewGuid().ToString(), EmployeeID = "EMP001", LeaveType = "Vacation", StartDate = "2025-01-01", EndDate = "2025-01-05", Status = "Pending" });
                leaveRequests.Add(new LeaveReq { RequestID = Guid.NewGuid().ToString(), EmployeeID = "EMP002", LeaveType = "Sick", StartDate = "2025-02-10", EndDate = "2025-02-11", Status = "Approved" });

            }
            
            }
        
        private void SaveDataToJsonFile()
        {
            using (var outputStream = File.OpenWrite(_jsonFileName))
            {
                JsonSerializer.Serialize<List<LeaveReq>>(
                    new Utf8JsonWriter(outputStream, new JsonWriterOptions
                    { SkipValidation = true, Indented = true })
                    , leaveRequests);
            }
        }

        private void RetrieveDataFromJsonFile()
        {
            if (!File.Exists(_jsonFileName))
            {
                leaveRequests = new List<LeaveReq>();
                return;
            }

            using (var jsonFileReader = File.OpenText(_jsonFileName))
            {
                leaveRequests = JsonSerializer.Deserialize<List<LeaveReq>>
                    (jsonFileReader.ReadToEnd(), new JsonSerializerOptions
                    { PropertyNameCaseInsensitive = true })
                    .ToList();
            }
        }
        public void AddLeave(LeaveReq leaveReq)
        {
            RetrieveDataFromJsonFile();
           leaveReq.RequestID = Guid.NewGuid().ToString();
            leaveRequests.Add(leaveReq);
            SaveDataToJsonFile();
        }

        public List<LeaveReq> GetAllLeaves()
        {
            RetrieveDataFromJsonFile();
            return leaveRequests;
        }

        public bool UpdateLeave(string requestID, string newStatus)
        {
            RetrieveDataFromJsonFile();
            var existing = leaveRequests.FirstOrDefault(x => x.RequestID == requestID);
            if (existing != null)
            {
                existing.Status = newStatus;
                SaveDataToJsonFile();
                return true;
            }
            return false;
        }

        public bool DeleteLeave(string requestID)
        {
            RetrieveDataFromJsonFile();
            var existing = leaveRequests.FirstOrDefault(x => x.RequestID == requestID);
            if (existing != null)
            {
                leaveRequests.Remove(existing);
                SaveDataToJsonFile();
                return true;
            }
            return false;
        }
    }
}