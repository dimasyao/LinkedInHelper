using LH.FileManager.Interfaces;
using LH.Utility;
using LinkedInHelper.Services.Interfaces;

namespace LinkedInHelper.Services
{
    public class ProgramExecutorService : IProgramExecutorService
    {
        private readonly ITextFile _fileHandler;
        private readonly IImageFile _imageHandler;
        private readonly IUserService _userService;

        private readonly ILogger _logger;

        public ProgramExecutorService(ITextFile fileHandler, IImageFile imageHandler, ILogger<ProgramExecutorService> logger, IUserService userService)
        {
            _fileHandler = fileHandler;
            _imageHandler = imageHandler;
            _userService = userService;

            _logger = logger;
        }

        public async void Execute()
        {
            var accessToken = _fileHandler.ReadDataFromFile(FilePath.Credentials, fileName: "AccessToken.txt");

            if (accessToken == null)
            {
                _logger.LogError("Error obtaining access token.");

                return;
            }

            var imageUrl = await _userService.GetProfilePhotoUrl(accessToken);

            await _imageHandler.DownloadAndSave(imageUrl);

            Environment.Exit(0);
        }
    }
}
