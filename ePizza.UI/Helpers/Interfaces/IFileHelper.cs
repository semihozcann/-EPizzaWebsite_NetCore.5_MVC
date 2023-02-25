using Microsoft.AspNetCore.Http;

namespace ePizza.UI.Helpers.Interfaces
{
    public interface IFileHelper
    {
        void DeleteFile(string imageUrl);
        string UploadFile(IFormFile file);

    }
}
