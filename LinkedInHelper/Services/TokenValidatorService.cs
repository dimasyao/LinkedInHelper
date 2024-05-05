using LH.FileManager.Interfaces;
using LH.Models;
using LH.Utility;
using LinkedInHelper.Services.Interfaces;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;

namespace LinkedInHelper.Services
{
    public class TokenValidatorService : ITokenValidatorService
    {
        private readonly ITextFile _textFile;

        public TokenValidatorService(ITextFile textFile) 
        {
            _textFile = textFile;
        }

        public bool TokenIsValid()
        {
            var accessTokenPath = Directory.GetCurrentDirectory() + "\\" + FilePath.Credentials + FileNameConstant.AccessToken;
            
            if (File.Exists(accessTokenPath))
            {
                var accessToken = JsonConvert.DeserializeObject<AccessToken>(_textFile.ReadDataFromFile(FilePath.Credentials, fileName: FileNameConstant.AccessToken));

                if (accessToken != null && accessToken.IsTokenValid())
                {
                    return true;
                }
            }

            return false;
        }
    }
}
