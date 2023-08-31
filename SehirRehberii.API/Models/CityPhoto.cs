namespace SehirRehberii.API.Models
{
    public class CityPhoto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public bool IsMain { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
    }
}
