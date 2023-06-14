using DigiLearn.Web.Infrastructure.JwtUtil;
using TicketModule;
using UserModule.Core;
using CoreModule.Configuration;
using Common.Application.FileUtil.Interfaces;
using Common.Application.FileUtil.Services;
using DigiLearn.Web.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
// Add services to the container.
builder.Services.AddScoped<ILocalFileService, LocalFileService>();
builder.Services.AddScoped<IFtpFileService, FtpFileService>();
builder.Services.AddTransient<TeacherActionFilter>();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.InitUserModule(builder.Configuration);
builder.Services.InitTicketModule(builder.Configuration);
builder.Services.InitCoreModule(builder.Configuration);

services.AddJwtAuthentication(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.Use(async (context, next) =>
{
    var token = context.Request.Cookies["token"]?.ToString();
    if (string.IsNullOrWhiteSpace(token) == false)
    {
        context.Request.Headers.Append("Authorization", $"Bearer {token}");
    }
    await next();
});


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();

app.Run();
