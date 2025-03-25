namespace Mottrist.Domain.Entities.CarDetails
{
    public class CarImage
    {
        public string ImageUrl { get; set; } = null!;
        public int CarId { get; set; }

        #region Navigations

        public virtual Car Car { get; set; } = null!;
        #endregion
    }
}
