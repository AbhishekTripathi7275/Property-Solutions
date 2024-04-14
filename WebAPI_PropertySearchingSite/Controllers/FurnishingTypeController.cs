using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI_PropertySearchingSite.Dtos;
using WebAPI_PropertySearchingSite.Interfaces;
using WebAPI_PropertySearchingSite.Models;

namespace WebAPI_PropertySearchingSite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FurnishingTypeController : ControllerBase
    {
        private readonly IUnitOfWork uc;
        private readonly IMapper mapper;

        public FurnishingTypeController(IUnitOfWork uc, IMapper mapper)
        {
            this.uc = uc;
            this.mapper = mapper;
        }
        [HttpGet("list")]
        [AllowAnonymous]
        public async Task<IActionResult> GetFurnishingTypes()
        {
            var furnishingTypes = await uc.FurnishingTypeRepository.GetFurnishingTypesAsync();
            var furnishingTypesDto = mapper.Map<IEnumerable<KeyValuePairDto>>(furnishingTypes);
            return Ok(furnishingTypesDto);
        }
    }
}
