using System;

namespace KinderApi.DTOs
{
    public class MessageDto
    {
        public Guid MessageId { get; set; }
        public int? SenderId { get; set; }
        public int? ReceiverId { get; set; }
        public string Text { get; set; }
        public DateTime sendingTime { get; set; }
        public bool isRead { get; set; }
    }
}