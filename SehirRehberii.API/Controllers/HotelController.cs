using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SehirRehberii.API.Data;
using SehirRehberii.API.Dtos;
using SehirRehberii.API.Models;
using System.Collections.Generic;

namespace SehirRehberii.API.Controllers
{

    [Produces("application/json")]
    // [Route("api/Cities/{cityId}/Hotels")]
    [Route("api/")]
    [ApiController]
    public class HotelController : Controller
    {
        private IAppRepository _appRepository;
        private IMapper _mapper;

        public HotelController(IAppRepository appRepository, IMapper mapper)
        {
            _appRepository = appRepository;
            _mapper = mapper;
        }

        [HttpGet("hotel")]

        public ActionResult GetAllHotels()
        {
            var hotel = _appRepository.GetAllHotels();
            var hotelToReturn = _mapper.Map<List<HotelForListDto>>(hotel);
            return Ok(hotelToReturn);
        }





        [HttpGet]
        public ActionResult GetCities()
        {
            var cities = _appRepository.GetCities();
            var citiesToReturn = _mapper.Map<List<CityForListDto>>(cities);

            return Ok(citiesToReturn);
        }



        [HttpGet("Cities/{cityId}/Hotels")]
        public ActionResult GetHotelsByCityId(int cityId)
        {
            var hotels = _appRepository.GetHotelsByCityId(cityId);
            var hotelsToReturn = _mapper.Map<IEnumerable<HotelForListDto>>(hotels);
            return Ok(hotelsToReturn);
        }




        [HttpGet("{id}")]
        public ActionResult GetHotelById(int id)
        {
            var hotel = _appRepository.GetHotelById(id);
            var hotelToReturn = _mapper.Map<HotelForDetailDto>(hotel);
            return Ok(hotelToReturn);
        }

        [HttpGet("{hotelId}/photos")]
        public ActionResult GetHotelPhotos(int hotelId)
        {
            var photos = _appRepository.GetHotelPhotos(hotelId);
            return Ok(photos);
        }




        [Route("add")]
        public ActionResult Add([FromBody] Hotel hotel)
        {
            _appRepository.Add(hotel);
            _appRepository.SaveAll();
            return Ok(hotel);
        }
    }
}

