using KinderApi.Models;

namespace KinderApi.DTOs
{
    public class ImageDto
    {
        public string ImagePublicId { get; set; }
        public string ImgPath { get; set; }
        public User User { get; set; }
        public bool IsMain {get;set;}
    }
}