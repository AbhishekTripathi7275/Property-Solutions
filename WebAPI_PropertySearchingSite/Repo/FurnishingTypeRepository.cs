using Microsoft.EntityFrameworkCore;
using WebAPI_PropertySearchingSite.Data;
using WebAPI_PropertySearchingSite.Interfaces;
using WebAPI_PropertySearchingSite.Models;

namespace WebAPI_PropertySearchingSite.Repo
{
    public class FurnishingTypeRepository : IFurnishingTypeRepository
    {
        private readonly DataContext dc;

        public FurnishingTypeRepository(DataContext dc)
        {
            this.dc = dc;
        }
        public async Task<IEnumerable<FurnishingType>> GetFurnishingTypesAsync()
        {
            return await dc.FurnishingTypes.ToListAsync();
        }
    }
}
