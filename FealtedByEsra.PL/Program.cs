using FealtedByEsra.BAL;
using FealtedByEsra.BAL.Helpers.GoogleHelpers;
using FealtedByEsra.DAL;
using FealtedByEsra.DAL.Concrete.Context;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Net.Http.Headers;
using Serilog.Context;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
CookieBuilder cookieBuilder = new();
// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddMvc();

builder.Services.AddMemoryCache();

builder.Services.AddDbContext<FealtedByEsraDbContext>();

builder.Services.Configure<GoogleHelper>(builder.Configuration.GetSection("GoogleReCaptcha"));

builder.Services.AddDALService();

builder.Services.AddBALService();

builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddIdentityService();

builder.Services.AddSharpImage();

builder.Services.AddAuth(builder);

builder.Services.AddSerilogService(builder);

cookieBuilder.Name = "Esrarga";
cookieBuilder.HttpOnly = false;
cookieBuilder.SecurePolicy = CookieSecurePolicy.SameAsRequest;
cookieBuilder.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict;

builder.Services.ConfigureApplicationCookie(opts =>
{
    opts.LoginPath = new PathString("/user/login");
    opts.LogoutPath = new PathString("/user/logout");
    opts.Cookie = cookieBuilder;
    opts.ExpireTimeSpan = System.TimeSpan.FromDays(10);
    opts.SlidingExpiration = true;
});

builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
});

builder.Services.AddCors(opts =>
{
    opts.AddPolicy("AllowSites", builder =>
    {
        builder.WithOrigins("https://localhost:7043", "http://localhost:5043").WithHeaders(HeaderNames.ContentType, "x-custom-header").AllowAnyMethod();
    });

    opts.AddPolicy("AllowSites2", builder =>
    {
        builder.WithOrigins("https://esrarga.com", "http://esrarga.com").WithMethods("POST", "GET").AllowAnyHeader();
    });

    opts.AddPolicy("AllowSites3", builder =>
    {
        builder.WithOrigins("https://localhost:7043", "http://localhost:5043").WithMethods("POST", "GET").AllowAnyHeader();
    });
});

builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    options.Providers.Add<BrotliCompressionProvider>();
    options.Providers.Add<GzipCompressionProvider>();
});

builder.Services.AddDataProtection();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseResponseCompression();

app.UseCors("AllowSites2");

app.UseAuthentication();

app.UseAuthorization();

app.Use(async (context, next) =>
{
    var username = context!.User!.Identity!.IsAuthenticated ? context.User.Identity.Name : "Site kullanýcýsý";

    LogContext.PushProperty("UserName", username);

    await next.Invoke();
});

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "post",
        pattern: "/post/{id?}/{title?}",
        new { controller = "Post", action = "Details" });

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
