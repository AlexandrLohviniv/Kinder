using System;
using System.Collections.Generic;
using System.Text;

namespace KinderMobile.DTOs
{
    public enum Role
    {
        SimpleUser,
        Admin
    }
    public enum Sexuality
    {
        Male,
        Female,
        NotDefined
    }
    public enum Rate
    {
        Positive,
        Negative,
        Neutral
    }

    public class UserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Sexuality Sex { get; set; }
        public Role Role { get; set; }
        public DateTime DateOfBith { get; set; }
        public string AboutMe { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; }
        public DateTime LastSeen { get; set; }
    }
}
