using LH.FileManager;
using LH.FileManager.Interfaces;
using LH.Utility;
using LinkedInHelper.Services;
using LinkedInHelper.Services.Interfaces;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IImageFile, ImageFile>();
builder.Services.AddSingleton<ITextFile, TextFile>();

builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<IProgramExecutorService, ProgramExecutorService>();

builder.Services.AddControllers();

var app = builder.Build();

app.UseAuthorization();

app.MapControllers();

if (File.Exists(Directory.GetCurrentDirectory() + "\\" + FilePath.Credentials + FileNameConstant.AccessToken))
{
    using (var scope = app.Services.CreateScope())
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

app.Run();
