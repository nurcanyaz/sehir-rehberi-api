using Remotion.Linq.Clauses;
using SehirRehberii.API.Models;
using System.Collections.Generic;

namespace SehirRehberii.API.Data
{
    public interface IAppRepository
    {
        void Add<T>(T entity) where T:class;
        void Delete<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        bool SaveAll();

        List<City> GetCities();
      
        List<Photo> GetPhotosByCity(int cityId);
        City GetCityById(int cityId);
        Photo GetPhoto(int id);
        IEnumerable<Hotel> GetHotelsByCityId(int cityId);
        Hotel GetHotelById(int hotelId);
        IEnumerable<string> GetHotelPhotos(int hotelId);
        List<Hotel> GetAllHotels();

        // List<Hotel> GetAllHotels(int hotelId);
    }
}
