using System.ComponentModel.DataAnnotations.Schema;

namespace HotelListing.API.Models.Country;

public class HotelDto : BaseHotelDto
{
    public int Id { get; set; }
    
}