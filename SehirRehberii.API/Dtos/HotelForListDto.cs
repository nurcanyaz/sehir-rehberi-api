namespace SehirRehberii.API.Dtos
{
    public class HotelForListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CityId { get; set; }
 
        public string PhotoUrl { get; set; }
    }
}
