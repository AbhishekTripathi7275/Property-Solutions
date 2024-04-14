using WebAPI_PropertySearchingSite.Models;

namespace WebAPI_PropertySearchingSite.Interfaces
{
    public interface ICityRepository
    {
        Task<IEnumerable<City>> GetCitiesAsync();
        void AddCity(City city);
        void DeleteCity(int CityId);
        Task<City> FindCity(int id);
    }
}
