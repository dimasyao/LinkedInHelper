using LH.Models;
using LH.Utility;
using LinkedInHelper.Services.Interfaces;
using Newtonsoft.Json;
using System.Diagnostics;

namespace LinkedInHelper.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void ExecuteOrOpenAuthorizationPage(this IApplicationBuilder app, ITokenValidatorService tokenValidator)
        {
            if (tokenValidator.TokenIsValid())
            {
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    scope.ServiceProvider.GetRequiredService<IProgramExecutorService>().Execute();
                }
            }
            else
            {
                try
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = Directory.GetCurrentDirectory() + "\\" + FilePath.Views + FileNameConstant.AuthorizationPage,
                        UseShellExecute = true
                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred when opening the HTML authorization page: {ex.Message}");
                }
            }
        }
    }
}
