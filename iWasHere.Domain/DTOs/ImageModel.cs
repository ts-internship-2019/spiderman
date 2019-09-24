namespace iWasHere.Domain.DTOs
{
    public class ImageModel
    {
        public int ImageId { get; set; }
        public int TouristAttractionId { get; set; }
        public IFormFile Photo { get; set; }
        public String PhotoPath { get; set; }
    }
}
