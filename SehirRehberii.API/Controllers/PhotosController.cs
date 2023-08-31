using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Options;
using SehirRehberii.API.Data;
using SehirRehberii.API.Dtos;
using SehirRehberii.API.Helpers;
using SehirRehberii.API.Models;
using System.Linq;
using System.Security.Claims;

namespace SehirRehberii.API.Controllers
{
    [Produces("application/json")]
    [Route("api/cities/{cityId}/photos")]
 
    public class PhotosController : Controller
    {
        private IAppRepository _appRepository;
        private IMapper _mapper;
        private IOptions<CloudinarySettingscs> _cloudinaryConfig;

        private Cloudinary _cloudinary;
        public PhotosController(IAppRepository appRepository, IMapper mapper, IOptions<CloudinarySettingscs> cloudinaryConfig)
        {
            _appRepository = appRepository;
            _mapper = mapper;
            _cloudinaryConfig = cloudinaryConfig;

            Account account = new Account(
                cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _cloudinaryConfig.Value.ApiSecret);
            _cloudinary = new Cloudinary(account);
        }



        [HttpPost]
        public ActionResult AddPhotoForCity(int cityId, [FromForm] PhotoForCreationDto photoForCreationDto)
        {
            var city = _appRepository.GetCityById(cityId);
            if (city == null)
            {
                return BadRequest("Could not find the city.");
            }

            var currenUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (currenUserId != city.UserId)
            {
                return Unauthorized();
            }

            var file = photoForCreationDto.File;
            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.Name, stream)
                    };
                    uploadResult = _cloudinary.Upload(uploadParams);
                }
            }

            photoForCreationDto.Url = uploadResult.Url.ToString();
            photoForCreationDto.PublicId = uploadResult.PublicId;

            var photo = _mapper.Map<Photo>(photoForCreationDto);
            photo.City = city;

            if (!city.Photos.Any(p => p.IsMain))
            {
                photo.IsMain = true;
            }

            city.Photos.Add(photo);

            if (_appRepository.SaveAll())
            {
                var photoToReturn = _mapper.Map<PhotoForReturnDto>(photo);
                return CreatedAtRoute("GetPhoto", new { id = photo.Id }, photoToReturn);
            }

            return BadRequest("Could not add the photo");
        }

        [HttpPut("{photoId}")]
        public ActionResult UpdateCityPhoto(int cityId, int photoId, [FromBody] PhotoForUpdateDto cityPhotoUpdateDto)
        {
            var city = _appRepository.GetCityById(cityId);
            if (city == null)
            {
                return NotFound("City not found");
            }

            var currenUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (currenUserId != city.UserId)
            {
                return Unauthorized();
            }

            var photo = city.Photos.FirstOrDefault(p => p.Id == photoId);
            if (photo == null)
            {
                return NotFound("Photo not found");
            }

            // Update photo properties based on cityPhotoUpdateDto
            photo.Description = cityPhotoUpdateDto.Description;
            photo.IsMain = cityPhotoUpdateDto.IsMain;

            if (_appRepository.SaveAll())
            {
                return NoContent();
            }

            return BadRequest("Could not update the photo");
        }

        [HttpDelete("{photoId}")]
        public ActionResult DeleteCityPhoto(int cityId, int photoId)
        {
            var city = _appRepository.GetCityById(cityId);
            if (city == null)
            {
                return NotFound("City not found");
            }

            var currenUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (currenUserId != city.UserId)
            {
                return Unauthorized();
            }

            var photo = city.Photos.FirstOrDefault(p => p.Id == photoId);
            if (photo == null)
            {
                return NotFound("Photo not found");
            }

            if (photo.IsMain)
            {
                return BadRequest("You cannot delete the main photo");
            }

            if (photo.PublicId != null)
            {
                var deleteParams = new DeletionParams(photo.PublicId);
                var result = _cloudinary.Destroy(deleteParams);

                if (result.Result == "ok")
                {
                    city.Photos.Remove(photo);
                    if (_appRepository.SaveAll())
                    {
                        return Ok();
                    }
                }
            }
            else
            {
                city.Photos.Remove(photo);
                if (_appRepository.SaveAll())
                {
                    return Ok();
                }
            }

            return BadRequest("Failed to delete the photo");
        }
    }
}