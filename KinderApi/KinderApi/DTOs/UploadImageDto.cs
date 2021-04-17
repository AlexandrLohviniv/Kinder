using System;
using Microsoft.AspNetCore.Http;

namespace KinderApi.DTOs
{
    public class UploadImageDto
    {
        public string Url{get;set;}
        public IFormFile File {get;set;}
        public string Describtion { get; set; }
        public DateTime DateAdded { get; set; }
        public string PublicID { get; set; }

        public UploadImageDto()
        {
            DateAdded = DateTime.Now;
        }
    }
}