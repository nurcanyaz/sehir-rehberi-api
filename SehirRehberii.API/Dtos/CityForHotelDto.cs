using SehirRehberii.API.Models;
using System.Collections.Generic;

namespace SehirRehberii.API.Dtos
{
    public class CityForHotelDto
    {
        public int HotelId { get; set; }
        public int CityId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Location { get; set; }

        public List<Photo> Photos { get; set; }
    }
}
