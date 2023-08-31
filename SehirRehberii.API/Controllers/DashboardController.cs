using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SehirRehberii.API.Data;
using SehirRehberii.API.Dtos;
using SehirRehberii.API.Models;
using System.Collections.Generic;
using System.Linq;

namespace SehirRehberii.API.Controllers
{
    public class DashboardController : Controller
    {
        private DataContext _context;
        private IAppRepository _appRepository;
        private IMapper _mapper;

        public DashboardController(IAppRepository appRepository, IMapper mapper, DataContext context)
        {
           _mapper = mapper;
            _context = context;
            _appRepository = appRepository;
        }

        [HttpGet("totals")]
        public IActionResult GetTotals()
        {
            var totalUsers = _context.Users.Count();
            var totalCities = _context.Cities.Count();
            var totalHotels = _context.Hotels.Count();
           // var totalReservations = _context.Reservations.Count();

            var result = new
            {
                TotalUsers = totalUsers,
                TotalCities = totalCities,
                TotalHotels = totalHotels,
          //      TotalReservations = totalReservations
            };

            return Ok(result);
        }


       
        [HttpGet("cities")]
        public IActionResult GetCities()
        {
            var cities = _appRepository.GetCities();
            return Ok(cities);
        }




        [HttpPost]
        [Route("addcity")]
        public ActionResult Add([FromBody] City city)
        {
            _appRepository.Add(city);
            _appRepository.SaveAll();
            return Ok(city);

        }


        [HttpDelete("deletecity/{id}")]
        public IActionResult DeleteCity(int id)
        {
            var city = _appRepository.GetCityById(id);

            if (city == null)
            {
                return NotFound();
            }

            _appRepository.Delete(city);
            _appRepository.SaveAll();

            return NoContent(); 
        }

        [HttpPut("updatecity/{id}")]
        public IActionResult UpdateCity(int id, [FromBody] CityUpdateDto cityUpdateDto)
        {
            var cityFromRepo = _appRepository.GetCityById(id);

            if (cityFromRepo == null)
            {
                return NotFound();
            }


            cityFromRepo.Name = cityUpdateDto.Name;
            cityFromRepo.Description = cityUpdateDto.Description;
            cityFromRepo.PhotoUrl = cityUpdateDto.PhotoUrl;

            _appRepository.Update(cityFromRepo);
            _appRepository.SaveAll();

            return NoContent();

        }
        [HttpGet("getcity/{id}")]
        public IActionResult GetCity(int id)
        {
            var city = _appRepository.GetCityById(id);

            if (city == null)
            {
                return NotFound();
            }

            return Ok(city);
        }

        //Hotel



        [HttpGet("hotels")]
        public IActionResult GetAllHotels()
        {
            var hotels = _appRepository.GetAllHotels();
            return Ok(hotels);
        }

        [HttpPost]
        [Route("addHotel")]
        public ActionResult Add([FromBody] Hotel hotel)
        {
            _appRepository.Add(hotel);
            _appRepository.SaveAll();
            return Ok(hotel);

        }

        [HttpGet("gethotel/{id}")]
        public IActionResult GetHotel(int id)
        {
            var hotel = _appRepository.GetHotelById(id);

            if (hotel == null)
            {
                return NotFound();
            }

            return Ok(hotel);
        }


    }
}

