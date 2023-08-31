using System.Collections.Generic;

namespace SehirRehberii.API.Models
{
    public class Hotel
    {
        public Hotel() {

            Photos = new List<Photo>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CityId { get; set; }
      
        public List<Photo> Photos { get; set; }

    }
}
