using WebAPI_PropertySearchingSite.Models;

namespace WebAPI_PropertySearchingSite.Interfaces
{
    public interface IPropertyRepository
    {
        Task<IEnumerable<Property>> GetPropertiesAsync(int SellRent);
        Task<Property> GetPropertyDetailAsync(int id);
        Task<Property> GetPropertyDetailByIdAsync(int id);
        void AddProperty(Property property);
        void DeleteProperty(int Id);
    }
}
