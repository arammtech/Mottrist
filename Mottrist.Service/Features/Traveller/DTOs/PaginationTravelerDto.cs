namespace Mottrist.Service.Features.Traveller.DTOs
{
    public class PaginationTravelerDto
    {
        public List<GetTravelerDto>? Travelers { get; set; } = null!;

        public int? TotalRecords { get; set; }

    }
}
