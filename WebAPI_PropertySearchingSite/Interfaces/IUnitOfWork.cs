namespace WebAPI_PropertySearchingSite.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        ICityRepository CityRepository { get; }
        IPropertyRepository PropertyRepository { get; }
        IPropertyTypeRepository PropertyTypeRepository { get; }
        IFurnishingTypeRepository FurnishingTypeRepository { get; }      
        Task<bool> SaveAsync();
    }
}
