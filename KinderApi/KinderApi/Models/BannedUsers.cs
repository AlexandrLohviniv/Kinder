using System;

namespace KinderApi.Models
{
    public class BannedUsers
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime LastBanDay { get; set; }
    }
}