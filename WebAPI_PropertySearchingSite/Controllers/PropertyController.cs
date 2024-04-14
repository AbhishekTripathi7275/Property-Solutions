using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI_PropertySearchingSite.Dtos;
using WebAPI_PropertySearchingSite.Interfaces;
using WebAPI_PropertySearchingSite.Models;
using WebAPI_PropertySearchingSite.Repo;
using WebAPI_PropertySearchingSite.Services;

namespace WebAPI_PropertySearchingSite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : BaseController
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        private readonly IPhotoUpload photoUpload;
        private readonly IHttpContextAccessor context;

        public PropertyController(IUnitOfWork uow, IMapper mapper, IPhotoUpload photoUpload, IHttpContextAccessor context)
        {
            this.uow = uow;
            this.mapper = mapper;
            this.photoUpload = photoUpload;
            this.context = context;
        }

        [HttpGet("list/{sellRent}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPropertyList(int SellRent)
        {
            var properties = await uow.PropertyRepository.GetPropertiesAsync(SellRent);
            var PropertyListDTO = mapper.Map<IEnumerable<PropertyListDto>>(properties);
            return Ok(PropertyListDTO);
        }

        [HttpGet("detail/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPropertyDetail(int id)
        {
            var property = await uow.PropertyRepository.GetPropertyDetailAsync(id);
            var PropertyDTO = mapper.Map<PropertyDetailDto>(property);
            return Ok(PropertyDTO);
        }

        [HttpPost("add")]
        [Authorize]
        public async Task<IActionResult> AddProperty(PropertyDto propertyDto)
        {
            var property = mapper.Map<Property>(propertyDto);
            var UserId = GetUserId();
            property.PostedBy = UserId;
            property.LastUpdatedBy = UserId;
            uow.PropertyRepository.AddProperty(property);
            await uow.SaveAsync();
            var LastInsertedID = property.Id;
            return StatusCode(201, LastInsertedID);
        }

        //property/add/photo/1
        [HttpPost("add/photo/{propId}")]
        [Authorize]
        public async Task<IActionResult> AddPropertyPhoto(List<IFormFile> file, int propId)
        {
            string host = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            if (file.Count > 0)
            {
                foreach (var files in file)
                {
                    var path = await photoUpload.UploadPhotoAsync(files);

                    path = host + path;
                    var Property = await uow.PropertyRepository.GetPropertyDetailAsync(propId);
                    var photo = new Photo { ImageUrl = path };


                    if (Property.Photos.Count == 0)
                    {
                        photo.IsPrimary = true;
                    }
                    Property.Photos.Add(photo);
                    await uow.SaveAsync();
                }
                return Ok(201);
            }
            else
            {
                return BadRequest("File Upload Failed");
            }
        }
    }
}
