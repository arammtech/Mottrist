namespace Feature.Car.DTOs
{
    public class CarImageDto
    {
        public string ImageUrl { get; set; } = null!;
        public int CarId { get; set; }
        public bool IsMain { get; set; }
    }
}
