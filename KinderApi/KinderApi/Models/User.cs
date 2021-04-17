using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KinderApi.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Sexuality Sex { get; set; }
        public Role Role { get; set; }
        public DateTime DateOfBith { get; set; }
        public string AboutMe { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime LastSeen { get; set; }
        public string Coordinate { get; set; }
        public ICollection<Message> SentMessages { get; set; }
        public ICollection<Message> ReceivedMessages { get; set; }
        public ICollection<Like> SentLikes { get; set; }
        public ICollection<Like> ReceivedLikes { get; set; }
        public ICollection<Image> Images { get; set; }

        public ICollection<Preference> Preferences { get; set; }
        public User()
        {
            SentMessages = new List<Message>();
            SentLikes = new List<Like>();
            ReceivedLikes = new List<Like>();
            Preferences = new List<Preference>();
            ReceivedMessages = new List<Message>();
            Images = new List<Image>();
        }
    }
}
