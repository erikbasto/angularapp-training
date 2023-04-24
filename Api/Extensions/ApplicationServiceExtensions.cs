using System;
using Api.Data;
using Api.Helpers;
using Api.Interfaces;
using Api.Services;
using Api.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Extensions
{
    public static class ApplicationServiceExtensions
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
		{
            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddCors();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITokenService, TokenService>();
           // services.AddScoped<IUserRepository, UserRepository>();
            services.Configure<CloudinarySettings>(configuration.GetSection("CloudinarySettings"));
            services.AddScoped<IPhotoService, PhotoService>();
           // services.AddScoped<ILikesRepository, LikesRepository>();
           // services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<LogUserActivity>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddSignalR();
            services.AddSingleton<PresenceTracker>();

            return services;
        }
	}
}

