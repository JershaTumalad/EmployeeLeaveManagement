using Microsoft.AspNetCore.Mvc;
using EmployeeLeaveManagement;
using System.Collections.Generic;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/leave")]
    [ApiController]
    public class LeaveApiController : ControllerBase
    {
        private readonly LeaveService _leaveService = new LeaveService(new LeaveDBData());

        [HttpGet]
        public IActionResult GetAllLeaves()
        {
          
            var leaves = _leaveService.GetAllLeaves(); 

            if (leaves == null || leaves.Count == 0)
            {
                return NotFound(new { message = "No leave records found." });
            }

            return Ok(leaves);
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetLeaveById(Guid id)
        {
            var leave = _leaveService.GetById(id);

            if (leave == null)
            {
                return NotFound();
            }

            return Ok(leave);
        }

        [HttpGet("employee/{employeeId:int}")]
        public IActionResult GetLeavesByEmployee(int employeeId)
        {
            var db = new LeaveDBData();
            var results = db.GetLeavesByEmployee(employeeId);
            return Ok(results);
        }

        [HttpGet("status/{status}")]
        public IActionResult GetLeavesByStatus(string status)
        {
            if (!Enum.TryParse(typeof(LeaveStatus), status, true, out var leaveStatus))
            {
                return BadRequest("Invalid leave status.");
            }

            var db = new LeaveDBData();
            var results = db.GetLeavesByStatus((LeaveStatus)leaveStatus);
            return Ok(results);
        }

        [HttpGet("employee/{employeeId:int}/points")]
        public IActionResult GetEmployeePoints(int employeeId)
        {
            var db = new LeaveDBData();
            var points = db.GetPoints(employeeId);
            return Ok(new { EmployeeId = employeeId, Points = points });
        }

        [HttpPut("employee/{employeeId:int}/points")]
        public IActionResult UpdateEmployeePoints(int employeeId, [FromBody] int newPoints)
        {
            var db = new LeaveDBData();
            db.UpdatePoints(employeeId, newPoints);
            return Ok(new { message = "Points updated successfully!", EmployeeId = employeeId, Points = newPoints });
        }

        [HttpPost]
        public IActionResult CreateLeave([FromBody] WebApplication1.Models.LeaveViewModel leave)
        {
            if (leave == null)
            {
                return BadRequest(new { message = "Leave data is required." });
            }

            int leaveDays = (leave.EndDate - leave.StartDate).Days + 1;
            int pointsNeeded = leave.LeaveType switch
            {
                "Vacation" => leaveDays * 2,
                "Sick" => leaveDays * 1,
                "Emergency" => leaveDays * 1,
                _ => leaveDays * 1
            };

            int currentPoints = _leaveService.GetPoints(leave.EmployeeId);
            if (currentPoints < pointsNeeded)
            {
                return BadRequest(new { message = "Insufficient points!" });
            }

            var newLeave = new LeaveReq
            {
                LeaveId = Guid.NewGuid(), 
                EmployeeId = leave.EmployeeId,
                EmployeeName = leave.EmployeeName,
                LeaveType = (LeaveType)Enum.Parse(typeof(LeaveType), leave.LeaveType),
                StartDate = leave.StartDate,
                EndDate = leave.EndDate,
                PointsDeducted = pointsNeeded,
                Status = LeaveStatus.Pending, 
                Reason = leave.Reason
            };

            var db = new LeaveDBData();
            db.Add(newLeave);
            
            return CreatedAtAction(
                nameof(GetLeaveById),
                new { id = newLeave.LeaveId },
                newLeave
            );
        }

        [HttpPatch("{id:guid}")]
        public IActionResult UpdateLeave(Guid id, [FromBody] WebApplication1.Models.LeaveViewModel leave)
        {
            if (leave == null)
            {
                return BadRequest("Leave data is required.");
            }

            var db = new LeaveDBData();
            var existingLeave = db.GetAll().FirstOrDefault(x => x.LeaveId == id);

            if (existingLeave == null)
            {
                return NotFound();
            }

            var updatedLeave = new LeaveReq
            {
                LeaveId = id,
                EmployeeId = leave.EmployeeId,
                EmployeeName = leave.EmployeeName,
                LeaveType = (LeaveType)Enum.Parse(typeof(LeaveType), leave.LeaveType),
                StartDate = leave.StartDate,
                EndDate = leave.EndDate,
                Reason = leave.Reason,
                Status = existingLeave.Status,
                PointsDeducted = existingLeave.PointsDeducted 
            };

            db.Edit(updatedLeave);

            return Ok(updatedLeave);
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteLeave(Guid id)
        {
           
            var db = new LeaveDBData();
            var existingLeave = db.GetAll().FirstOrDefault(x => x.LeaveId == id);

            if (existingLeave == null)
            {
                return NotFound();
            }

            db.Delete(id);

            return NoContent();
        }
    }
}