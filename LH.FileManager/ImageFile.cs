using LH.FileManager.Interfaces;
using LH.Utility;
using Microsoft.Extensions.Logging;

namespace LH.FileManager
{
    public class ImageFile : IImageFile
    {
        private readonly ILogger<ImageFile> _logger;

        public ImageFile(ILogger<ImageFile> logger)
        {
            _logger = logger;
        }

        public async Task DownloadAndSave(string imageUrl)
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    var imageData = await httpClient.GetByteArrayAsync(imageUrl);
                    File.WriteAllBytes($"{FilePath.ProfileImages}{Guid.NewGuid()}.jpg", imageData);
                    _logger.LogInformation("Image successfully saved.");
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error saving image: " + ex.Message);
                }
            }
        }
    }
}
