using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Parking.FindingSlotManagement.Application.Contracts.Infrastructure
{
    public interface ICloudinaryService
    {
        /// <summary>
        /// Upload ảnh lên Cloudinary
        /// </summary>
        /// <param name="file">File ảnh cần upload</param>
        /// <param name="folder">Thư mục lưu trữ trên Cloudinary (optional)</param>
        /// <returns>URL của ảnh sau khi upload</returns>
        Task<string> UploadImageAsync(IFormFile file, string? folder = null);
        
        /// <summary>
        /// Upload ảnh từ byte array lên Cloudinary
        /// </summary>
        /// <param name="imageBytes">Byte array của ảnh</param>
        /// <param name="fileName">Tên file</param>
        /// <param name="folder">Thư mục lưu trữ trên Cloudinary (optional)</param>
        /// <returns>URL của ảnh sau khi upload</returns>
        Task<string> UploadImageAsync(byte[] imageBytes, string fileName, string? folder = null);
        
        /// <summary>
        /// Xóa ảnh từ Cloudinary
        /// </summary>
        /// <param name="publicId">Public ID của ảnh trên Cloudinary</param>
        /// <returns>True nếu xóa thành công</returns>
        Task<bool> DeleteImageAsync(string publicId);
    }
}
