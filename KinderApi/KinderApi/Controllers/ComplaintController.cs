using System.Threading.Tasks;
using KinderApi.helper;
using KinderApi.Models;
using KinderApi.ServiceProtos;
using Microsoft.AspNetCore.Mvc;

namespace KinderApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ComplaintController : ControllerBase
    {
        private readonly IComplaintService complaintService;

        public ComplaintController(IComplaintService complaintService)
        {
            this.complaintService = complaintService;
        }

        [HttpPost("{fromId}/complain/{toId}")]
        public async Task<IActionResult> Complain(int fromId, int toId, [FromBody] ComplaintType type)
        {
            await complaintService.complain(fromId, toId, type);
            return Ok("Your complain is received");
        }

        [HttpGet("allComplaints")]
        public async Task<IActionResult> GetAllComplaints([FromQuery] PaginationParams paginationParams)
        {
            PagedList<Complaint> complaints = await complaintService.GetAllComplaints(paginationParams);

            Response.AddPagination(complaints.CurrentPage, complaints.PageSize,
                complaints.TotalCount, complaints.TotalPages);

            return Ok(complaints);
        }

        [HttpGet("fromUserComplaints/{fromId}")]
        public async Task<IActionResult> GetAllComplaintsFromUser(int fromId, [FromQuery] PaginationParams paginationParams)
        {
            PagedList<Complaint> complaints = await complaintService.GetAllFromUserComplaints(fromId, paginationParams);

            Response.AddPagination(complaints.CurrentPage, complaints.PageSize,
                complaints.TotalCount, complaints.TotalPages);

            return Ok(complaints);
        }

        [HttpGet("forUserComplaints/{fromId}")]
        public async Task<IActionResult> GetAllComplaintsForUser(int toId, [FromQuery] PaginationParams paginationParams)
        {
            PagedList<Complaint> complaints = await complaintService.GetAllForUserComplaints(toId, paginationParams);

            Response.AddPagination(complaints.CurrentPage, complaints.PageSize,
                complaints.TotalCount, complaints.TotalPages);

            return Ok(complaints);
        }
    }
}