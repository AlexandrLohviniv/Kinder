using System;
using System.Collections.Generic;
using System.Text;

namespace KinderMobile.DTOs
{
    public class MessageDto
    {
        public Guid MessageId { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string Text { get; set; }
        public DateTime sendingTime { get; set; }
        public bool isRead { get; set; }
    }

    public class MessageModel
    {
        public Guid MessageId { get; set; }
        public string Message { get; set; }
        public bool IsOwnerMessage { get; set; }
        public bool IsNotOwnerMessage { get; set; }
    }
}
