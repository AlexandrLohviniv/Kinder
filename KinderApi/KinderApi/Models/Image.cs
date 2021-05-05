namespace KinderApi.Models
{
    public class Image
    {
        public int id { get; set; }
        public string ImgPath { get; set; }
        public int? Userid { get; set; }
        public User User { get; set; }
        public string ImagePublicId { get; set; }
        public bool IsMain {get;set;}
    }
}