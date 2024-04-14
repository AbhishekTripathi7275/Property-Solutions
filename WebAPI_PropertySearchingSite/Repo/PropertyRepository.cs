using Microsoft.EntityFrameworkCore;
using WebAPI_PropertySearchingSite.Data;
using WebAPI_PropertySearchingSite.Interfaces;
using WebAPI_PropertySearchingSite.Models;

namespace WebAPI_PropertySearchingSite.Repo
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly DataContext dc;
        public PropertyRepository(DataContext dc)
        {
            this.dc = dc;
        }
        public void AddProperty(Property property)
        {
            dc.Properties.Add(property);
        }

        public void DeleteProperty(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Property>> GetPropertiesAsync(int SellRent)
        {
            var properties = await dc.Properties
                .Include(p => p.PropertyType)
                .Include(p => p.City)
                .Include(p => p.FurnishingType)
                .Include(p => p.Photos)
                .Where(p => p.SellRent == SellRent).ToListAsync();
            return properties;
        }

        public async Task<Property> GetPropertyDetailAsync(int id)
        {
            var properties = await dc.Properties
               .Include(p => p.PropertyType)
               .Include(p => p.City)
               .Include(p => p.FurnishingType)
               .Include(p => p.Photos)
               .Where(p => p.Id == id).FirstAsync();
            return properties;
        }

        public async Task<Property> GetPropertyDetailByIdAsync(int id)
        {
            var properties = await dc.Properties
               .Include(p => p.Photos)
               .Where(p => p.Id == id).FirstAsync();

            return properties;
        }
    }
}
