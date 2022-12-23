using FealtedByEsra.BAL.Abstract.Services;
using FealtedByEsra.BAL.Abstract.Services.CategoryService;
using FealtedByEsra.BAL.Abstract.Services.CommentService;
using FealtedByEsra.BAL.Abstract.Services.ContactService;
using FealtedByEsra.BAL.Abstract.Services.GoogleService;
using FealtedByEsra.BAL.Abstract.Services.MailService;
using FealtedByEsra.BAL.Abstract.Services.PostService;
using FealtedByEsra.BAL.Abstract.Services.ReplyService;
using FealtedByEsra.BAL.Concrete.Managers.CategoryMang;
using FealtedByEsra.BAL.Concrete.Managers.CommentMang;
using FealtedByEsra.BAL.Concrete.Managers.ContactMang;
using FealtedByEsra.BAL.Concrete.Managers.EmailMang;
using FealtedByEsra.BAL.Concrete.Managers.GoogleMang;
using FealtedByEsra.BAL.Concrete.Managers.NewsletterMang;
using FealtedByEsra.BAL.Concrete.Managers.PostMang;
using FealtedByEsra.BAL.Concrete.Managers.ReplyCommentMang;
using FealtedByEsra.BAL.Extensions;
using FealtedByEsra.BAL.Helpers.Image;
using FealtedByEsra.DAL.Concrete.Context;
using FealtedByEsra.Entity.Entities.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IO;
using Serilog;
using Serilog.Core;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Web.DependencyInjection;
using System.Security.Claims;
using System.Text;

namespace FealtedByEsra.BAL
{
    public static class ServiceRegistration
    {
        public static void AddBALService(this IServiceCollection services)
        {
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<IPostService, PostManager>();
            services.AddScoped<FileBaseUploader>();
            services.AddScoped<ImageHelper>();
            services.AddScoped<INewsletterService, NewsletterManager>();
            services.AddScoped<ICommentService, CommentManager>();
            services.AddScoped<IReplyCommentService, ReplyCommentManager>();
            services.AddScoped<IContactService, ContactManager>();
            services.AddScoped<IGoogleService, GoogleManager>();
            services.AddScoped<IEmailService, EmailManager>();
        }

        public static void AddIdentityService(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters =
                "abcçdefgğhijklmnopqrstuüvwxyzABCÇDEFGĞHIİJKLMNOÖPQRSTÜUVWXYZ";
                options.Password.RequiredLength = 3;
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
            }).AddEntityFrameworkStores<FealtedByEsraDbContext>().AddDefaultTokenProviders();
        }

        public static void AddSharpImage(this IServiceCollection services)
        {
            services.AddImageSharp(options =>
            {
                options.Configuration = Configuration.Default;
                options.MemoryStreamManager = new RecyclableMemoryStreamManager();
                options.BrowserMaxAge = TimeSpan.FromDays(7);
                options.CacheMaxAge = TimeSpan.FromDays(365);
                options.CacheHashLength = 8;
                options.OnParseCommandsAsync = _ => Task.CompletedTask;
                options.OnBeforeSaveAsync = _ => Task.CompletedTask;
                options.OnProcessedAsync = _ => Task.CompletedTask;
                options.OnPrepareResponseAsync = _ => Task.CompletedTask;
            });
        }

        public static void AddAuth(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
           .AddJwtBearer(options =>
           {
               options.TokenValidationParameters = new()
               {
                   ValidateAudience = true,
                   ValidateIssuer = true,
                   ValidateLifetime = true,
                   ValidateIssuerSigningKey = true,

                   ValidAudience = builder.Configuration["Token:Audience"],
                   ValidIssuer = builder.Configuration["Token:Issuer"],
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
                   LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false,

                   NameClaimType = ClaimTypes.Name
               };
           });
        }

        public static void AddSerilogService(this IServiceCollection services, WebApplicationBuilder builder)
        {
            Logger logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.Console(
            outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}--{UserName}")
            .WriteTo.File("D:\\USERDATA\\Desktop\\FealtedByEsra\\log\\log.txt", rollingInterval: RollingInterval.Day,
            outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}--{UserName}")
            .WriteTo.Seq(builder.Configuration["Seq:ServerUrl"])
            .MinimumLevel.Information()
            .CreateLogger();

            builder.Host.UseSerilog(logger);
        }
    }
}
