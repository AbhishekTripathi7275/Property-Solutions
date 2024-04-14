using WebAPI_PropertySearchingSite.Interfaces;

namespace WebAPI_PropertySearchingSite.Services
{
    public class PhotoUploadService : IPhotoUpload
    {
        public async Task<string> UploadPhotoAsync(IFormFile file)
        {
            string ImgPath = "";
            try
            {
                if (file.Length > 0)
                {
                    string path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "UploadedFiles"));
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (var fileStream = new FileStream(Path.Combine(path, file.FileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    ImgPath = "/UploadedFiles/" + file.FileName;
                    return ImgPath;
                }
                else
                {
                    return ImgPath;
                }
            }
            catch (Exception ex)
            {
                return ImgPath;
            }
        }


    }
}
