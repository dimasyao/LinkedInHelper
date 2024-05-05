using LH.Models;
using LH.Utility;
using LinkedInHelper.Services.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace LinkedInHelper.Services
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;

        public UserService(ILogger<UserService> logger)
        {
            _logger = logger;
        }

        public async Task<string> GetProfilePhotoUrl(string accessToken)
        {
            var userInfo = await GetUserInfo(accessToken);
            if (userInfo == null)
            {
                _logger.LogError("Error obtaining user information.");
                return string.Empty;
            }

            string? imageUrl = userInfo["picture"]?.ToString();
            if (string.IsNullOrEmpty(imageUrl))
            {
                _logger.LogError("User image not found.");
                return string.Empty;
            }

            return imageUrl;
        }

        private async Task<JObject?> GetUserInfo(string accessToken)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var response = await httpClient.GetAsync(UriConstant.UserInfoUri);

                if (!response.IsSuccessStatusCode)
                    return null;

                return JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync()) as JObject;
            }
        }

        public async Task<AccessToken> GetAccessToken(string code)
        {
            string clientId = "781yx90d0ucy7x";
            string clientSecret = "EuS13Ic8CPrZ2epT";

            using (var httpClient = new HttpClient())
            {
                var requestParams = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", "authorization_code"),
                    new KeyValuePair<string, string>("code", code),
                    new KeyValuePair<string, string>("client_id", clientId),
                    new KeyValuePair<string, string>("client_secret", clientSecret),
                    new KeyValuePair<string, string>("redirect_uri", UriConstant.RedirectUri)
                });

                var response = await httpClient.PostAsync(UriConstant.AccessTokenUri, requestParams);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError("The linkedin server is not responding");
                    return null;
                }

                var result = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync()) as JObject;

                var accessToken = new AccessToken()
                {
                    Value = result?["access_token"]?.ToString(),
                    ExpiresIn = DateTime.Now.AddSeconds((int)result["expires_in"]),
                };
                
                return accessToken;
            }
        }
    }
}
