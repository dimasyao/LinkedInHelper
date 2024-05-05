using LH.FileManager;
using LH.FileManager.Interfaces;
using LinkedInHelper.Extensions;
using LinkedInHelper.Services;
using LinkedInHelper.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IImageFile, ImageFile>();
builder.Services.AddSingleton<ITextFile, TextFile>();

builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<IProgramExecutorService, ProgramExecutorService>();
builder.Services.AddSingleton<ITokenValidatorService, TokenValidatorService>();

builder.Services.AddControllers();

var app = builder.Build();

app.UseAuthorization();

app.MapControllers();

app.ExecuteOrOpenAuthorizationPage(app.Services.GetRequiredService<ITokenValidatorService>());

app.Run();
