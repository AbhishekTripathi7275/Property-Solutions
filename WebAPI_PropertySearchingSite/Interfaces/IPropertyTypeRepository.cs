using WebAPI_PropertySearchingSite.Models;

namespace WebAPI_PropertySearchingSite.Interfaces
{
    public interface IPropertyTypeRepository
    {
        Task<IEnumerable<PropertyType>> GetPropertyTypesAsync();
    }
}
