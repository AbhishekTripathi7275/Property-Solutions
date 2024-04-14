using WebAPI_PropertySearchingSite.Models;

namespace WebAPI_PropertySearchingSite.Interfaces
{
    public interface IPhotoUpload
    {
        Task<string> UploadPhotoAsync(IFormFile file);
    }
}
