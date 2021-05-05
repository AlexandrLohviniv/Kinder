using System.Collections.Generic;
using System.Threading.Tasks;
using KinderApi.helper;
using KinderApi.Models;

namespace KinderApi.ServiceProtos
{
    public interface IComplaintService
    {
         Task complain(int fromId, int toId, ComplaintType type);
         Task<PagedList<Complaint>> GetAllComplaints(PaginationParams paginationParams);
         Task<PagedList<Complaint>> GetAllForUserComplaints(int forId, PaginationParams paginationParams);
         Task<PagedList<Complaint>> GetAllFromUserComplaints(int fromId, PaginationParams paginationParams);
    }
}