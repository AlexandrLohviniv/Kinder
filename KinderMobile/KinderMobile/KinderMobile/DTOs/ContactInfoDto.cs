using System;
using System.Collections.Generic;
using System.Text;

namespace KinderMobile.DTOs
{
    public class ContactInfoDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }


        private string m_mainPhotoUrl = null;
        public string mainPhotoUrl
        {
            get
            {
                if (string.IsNullOrEmpty(m_mainPhotoUrl))
                    return "defaultUser.png";
                return m_mainPhotoUrl;
            }
            set
            {
                m_mainPhotoUrl = value;
            }
        }

        public ContactInfoDto Instance
        {
            get 
            {
                return this;
            }
        }

    }
}
