using System;
using System.Collections.Generic;
using KinderApi.Models;

namespace KinderApi.DTOs
{
    public class RegisterUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Sexuality Sex { get; set; }
        public Role Role { get; set; }
        public DateTime DateOfBith { get; set; }
        public string AboutMe { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<Preference> Preferences { get; set; }
    }
}