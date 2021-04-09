using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KinderApi.Models
{
    [Table("Prefernces")]
    public class Preference
    {
        [Key]
        public Guid PreferenceId { get; set; }
        public Rate SmokeRate { get; set; }
        public Rate BabyRate { get; set; }
        public Rate HeightRate { get; set; }
        public Rate PetsRate { get; set; }
        public Rate RelationshipRate { get; set; }
        public Sexuality Sex { get; set; }
        public Rate DrinkingRate { get; set; }
        public ICollection<User> Users { get; set; }
        public Preference()
        {
            Users = new List<User>();
        }
    }
}
