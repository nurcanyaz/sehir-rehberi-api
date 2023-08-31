using System;
using System.Collections.Generic;

namespace SehirRehberii.API.Models
{
    public class City
    {
        public City()
        {
            
            Photos = new List<CityPhoto>();
            
//
        }
      
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int HotelId { get; set; }
        public Hotel Hotel { get; }
        public List<CityPhoto> Photos { get; set; }
      
        public User User { get; set; }
        public string PhotoUrl { get;  set; }
    }
}
