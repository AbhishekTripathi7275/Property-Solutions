using WebAPI_PropertySearchingSite.Models;

namespace WebAPI_PropertySearchingSite.Interfaces
{
    public interface IFurnishingTypeRepository
    {
        Task<IEnumerable<FurnishingType>> GetFurnishingTypesAsync();
    }
}
