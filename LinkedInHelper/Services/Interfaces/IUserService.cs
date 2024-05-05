namespace LinkedInHelper.Services.Interfaces
{
    public interface IUserService
    {
        public Task<string> GetProfilePhotoUrl(string accessToken);
        public Task<string?> GetAccessToken(string code);
    }
}
