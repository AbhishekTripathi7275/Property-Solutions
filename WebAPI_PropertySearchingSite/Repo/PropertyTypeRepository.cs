using Microsoft.EntityFrameworkCore;
using WebAPI_PropertySearchingSite.Data;
using WebAPI_PropertySearchingSite.Interfaces;
using WebAPI_PropertySearchingSite.Models;

namespace WebAPI_PropertySearchingSite.Repo
{
    public class PropertyTypeRepository : IPropertyTypeRepository
    {
        private readonly DataContext dc;

        public PropertyTypeRepository(DataContext dc)
        {
            this.dc = dc;
        }
        public async Task<IEnumerable<PropertyType>> GetPropertyTypesAsync()
        {
            return await dc.PropertyTypes.ToListAsync();
        }
    }
}
