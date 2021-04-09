using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KinderApi.Models
{
    [Table("Messages")]
    public class Message
    {
        [Key]
        public Guid MessageId { get; set; }
        public int? SenderId {get;set;}
        [ForeignKey("SenderId")]
        [InverseProperty("SentMessages")]
        public User Sender { get; set; }
        public int? ReceiverId { get; set; }
        [ForeignKey("ReceiverId")]
        [InverseProperty("ReceivedMessages")]
        public User Receiver { get; set; }
        public string Text { get; set; }
        public DateTime sendingTime { get; set; }
        public bool isRead { get; set; }
    }
}
