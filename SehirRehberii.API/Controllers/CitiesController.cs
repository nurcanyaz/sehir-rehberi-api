using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SehirRehberii.API.Data;
using SehirRehberii.API.Dtos;
using SehirRehberii.API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SehirRehberii.API.Controllers
{
   // [Produces("application/json")]
    [Route("api/Cities")]
    [ApiController]
    public class CitiesController : Controller
    {
        private IAppRepository _appRepository;
        private IMapper _mapper;

        private DataContext _context;

        public CitiesController(IAppRepository appRepository, IMapper mapper, DataContext context)
        {
            _appRepository = appRepository;
            _mapper = mapper;

            _context = context;
        }

        [HttpGet]       
        public ActionResult GetCities()
        {
            var cities = _appRepository.GetCities();
            var citiesToReturn = _mapper.Map<List<CityForListDto>>(cities);

            return Ok(citiesToReturn);
        }



        [HttpGet]
        [Route("detail")]
        public ActionResult GetCitiesById(int id)
        {
            var city = _appRepository.GetCityById(id);
            var cityToReturn = _mapper.Map<CityForDetailDto>(city);

            return Ok(cityToReturn);
        }


        [HttpGet]
        [Route("Photos")]
        public ActionResult GetPhotosByCity(int cityId)
        {
          var photos = _appRepository.GetPhotosByCity(cityId);
            return Ok(photos);
        }

    }
}
