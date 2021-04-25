using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KinderApi.helper;
using KinderApi.Models;
using KinderApi.ServiceProtos;

namespace KinderApi.Services
{
    public class ComplaintService : IComplaintService
    {
        private readonly DatabaseContext context;
        private readonly UserService userService;

        public ComplaintService(DatabaseContext context, UserService userService)
        {
            this.context = context;
            this.userService = userService;
        }

        public async Task complain(int fromId, int toId, ComplaintType complainType)
        {
            User fromUser = await userService.GetUserById(fromId);
            User toUser = await userService.GetUserById(toId);

            if (fromUser == null || toUser == null)
                throw new Exception("invalid user id in ComplaintService");

            Complaint newComplaint = new Complaint()
            {
                Receiver = toUser,
                Sender = fromUser,
                complaint = complainType
            };

            await context.Complaints.AddAsync(newComplaint);
            await context.SaveChangesAsync();
        }

        public async Task<PagedList<Complaint>> GetAllComplaints(PaginationParams paginationParams)
        {
            var complaintsQuery = context.Complaints;

            return await PagedList<Complaint>.CreateAsync(complaintsQuery, paginationParams.PageNumber, paginationParams.PageSize);
        }

        public async Task<PagedList<Complaint>> GetAllForUserComplaints(int forId, PaginationParams paginationParams)
        {
            var complaintsQuery = context.Complaints.Where(u => u.ReceiverId == forId);

            return await PagedList<Complaint>.CreateAsync(complaintsQuery, paginationParams.PageNumber, paginationParams.PageSize);
        }

        public async Task<PagedList<Complaint>> GetAllFromUserComplaints(int fromId, PaginationParams paginationParams)
        {
            var complaintsQuery = context.Complaints.Where(u => u.SenderId == fromId);

            return await PagedList<Complaint>.CreateAsync(complaintsQuery, paginationParams.PageNumber, paginationParams.PageSize);
        }
    }
}