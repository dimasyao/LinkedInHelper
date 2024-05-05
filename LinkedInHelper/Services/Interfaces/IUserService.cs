using LH.Models;

namespace LinkedInHelper.Services.Interfaces
{
    public interface IUserService
    {
        public Task<string> GetProfilePhotoUrl(string accessToken);
        public Task<AccessToken> GetAccessToken(string code);
    }
}
