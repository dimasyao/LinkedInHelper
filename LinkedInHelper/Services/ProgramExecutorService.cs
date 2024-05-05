using LH.FileManager.Interfaces;
using LH.Models;
using LH.Utility;
using LinkedInHelper.Services.Interfaces;
using Newtonsoft.Json;

namespace LinkedInHelper.Services
{
    public class ProgramExecutorService : IProgramExecutorService
    {
        private readonly ITextFile _textFile;
        private readonly IImageFile _imageFile;
        private readonly IUserService _userService;

        private readonly ILogger _logger;

        public ProgramExecutorService(ITextFile textFile, IImageFile imageFile, ILogger<ProgramExecutorService> logger, IUserService userService)
        {
            _textFile = textFile;
            _imageFile = imageFile;
            _userService = userService;

            _logger = logger;
        }

        public async void Execute()
        {
            var accessToken = JsonConvert.DeserializeObject<AccessToken>(_textFile.ReadDataFromFile(FilePath.Credentials, fileName: FileNameConstant.AccessToken));

            if (accessToken == null || accessToken.Value == null)
            {
                _logger.LogError("Error obtaining access token.");

                return;
            }

            var imageUrl = await _userService.GetProfilePhotoUrl(accessToken.Value);

            await _imageFile.DownloadAndSave(imageUrl);

            Environment.Exit(0);
        }
    }
}
