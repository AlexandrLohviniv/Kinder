using System;
using System.Collections.Generic;
using System.Text;

namespace KinderMobile.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Sexuality Sex { get; set; }
        public Role Role { get; set; }
        public DateTime DateOfBith { get; set; }
        public string AboutMe { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; }
        public DateTime LastSeen { get; set; }
        public string mainPhotoUrl { get; set; }


        public string GetMainPhoto 
        {
            get 
            {
                if (string.IsNullOrEmpty(mainPhotoUrl))
                    return "defaultUser.jpg";
                return mainPhotoUrl;
            } 
        }

        public string GetFullName 
        {
            get 
            {
                return string.Format("{0} {1}", FirstName, LastName);
            }
        }
   }
}
