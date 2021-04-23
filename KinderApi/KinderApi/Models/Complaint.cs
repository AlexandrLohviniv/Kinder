using System.ComponentModel.DataAnnotations.Schema;

namespace KinderApi.Models
{
    public class Complaint
    {
        public int Id { get; set; }
        ComplaintType complaint { get; set; }
        public int? SenderId { get; set; }

        [ForeignKey("SenderId")]
        [InverseProperty("SentComplaints")]
        public virtual User Sender { get; set; }

        public int? ReceiverId { get; set; }
        
        [ForeignKey("ReceiverId")]
        [InverseProperty("ReceivedComplaints")]
        public virtual User Receiver { get; set; }
    }
}