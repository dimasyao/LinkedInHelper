using LH.FileManager;
using LH.FileManager.Interfaces;
using LH.Utility;
using LinkedInHelper.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LinkedInHelper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LInCallbackController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IProgramExecutorService _programExecutorService;

        private readonly ITextFile _textFile;

        private readonly ILogger<LInCallbackController> _logger;

        public LInCallbackController(IUserService userService,
            ILogger<LInCallbackController> logger, 
            ITextFile textFile, 
            IProgramExecutorService programExecutorService)
        {
            _userService = userService;
            _textFile = textFile;

            _logger = logger;
            _programExecutorService = programExecutorService;
        }

        public async Task RedirectLinkedIN(string code, string state)
        {
            var accessToken = await _userService.GetAccessToken(code);

            if (accessToken == null)
            {
                _logger.LogError("Error obtaining access token.");

                return;
            }

            await _textFile.Save(JsonConvert.SerializeObject(accessToken), FilePath.Credentials, FileNameConstant.AccessToken);

            _programExecutorService.Execute();
        }
    }
}
