using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI_PropertySearchingSite.Dtos;
using WebAPI_PropertySearchingSite.Interfaces;

namespace WebAPI_PropertySearchingSite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyTypeController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public PropertyTypeController(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }
        //api/propertytype/list
        [HttpGet("list")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPropertyType()
        {
            var PropertyTypes = await uow.PropertyTypeRepository.GetPropertyTypesAsync();
            var ProperTypeDto = mapper.Map<IEnumerable<KeyValuePairDto>>(PropertyTypes);
            return Ok(PropertyTypes);
        }
    }
}
