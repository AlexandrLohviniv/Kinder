using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KinderApi.Models
{
    [Table("Likes")]
    public class Like
    {
        [Key]
        public int Id { get; set; }
        public int? SenderId { get; set; }
        [ForeignKey("SenderId")]
        [InverseProperty("SentLikes")]
        public User Sender { get; set; }
        public int? ReceiverId { get; set; }
        [ForeignKey("ReceiverId")]
        [InverseProperty("ReceivedLikes")]
        public User Receiver { get; set; }
        public DateTime sendingTime { get; set; }
    }
    //[InverseProperty("Gamers")]
    //[ForeignKey("MainId")]
    //public virtual Team GamersTeam { get; set; }

}
