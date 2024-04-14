using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI_PropertySearchingSite.Data;
using WebAPI_PropertySearchingSite.Dtos;
using WebAPI_PropertySearchingSite.Interfaces;

namespace WebAPI_PropertySearchingSite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public CityController(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        // GET api/city
        [HttpGet("cities")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCities()
        {
            var cities = await uow.CityRepository.GetCitiesAsync();

            var citiesDto = mapper.Map<IEnumerable<CityDto>>(cities);
            return Ok(citiesDto);
        }
    }
}
