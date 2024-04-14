using Microsoft.EntityFrameworkCore;
using WebAPI_PropertySearchingSite.Data;
using WebAPI_PropertySearchingSite.Interfaces;
using WebAPI_PropertySearchingSite.Models;

namespace WebAPI_PropertySearchingSite.Repo
{
    public class CityRepository : ICityRepository
    {
        private readonly DataContext dc;

        public CityRepository(DataContext dc)
        {
            this.dc = dc;
        }
        public void AddCity(City city)
        {
            dc.Cities.Add(city);
        }

        public void DeleteCity(int CityId)
        {
            var city = dc.Cities.Find(CityId);
            dc.Cities.Remove(city);
        }

        public async Task<City> FindCity(int id)
        {
            return await dc.Cities.FindAsync(id);
        }

        public async Task<IEnumerable<City>> GetCitiesAsync()
        {
            return await dc.Cities.ToListAsync();
        }
    }
}
