using GroupTracker.DTOs.Lecturer;
using GroupTracker.Services.Abstraction;
using GroupTracker.Services.Implementation;
using GroupTracker.Services.Abstraction.Group;
using GroupTracker.Services.Implementation.Group;
using GroupTracker.Services.Abstraction.Syllabus;
using GroupTracker.Services.Implementation.Syllabus;
using GroupTracker.Services.Abstraction.FileStorage;
using GroupTracker.Services.Implementation.FileStorage;
using GroupTracker.ENV;
using Microsoft.Extensions.Configuration;
using SMTP.Authorization.EmailVerification;

namespace GroupTracker.Extensions
{
    public static class ServiceConfiguration
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration Configuration)
        {
            services.AddScoped<Microsoft.AspNetCore.Identity.PasswordHasher<LecturerRegistrationInput>>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ILecturerService, LecturerService>();
            services.AddScoped<ILecturerGroupCoordinator, LecturerGroupCoordinator>();
            services.AddScoped<ILectureSessionService, LecturerSessionService>();
            services.AddScoped<IAlternateWeekService, AlternateWeekService>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<ISyllabusTopicService, SyllabusTopicService>();
            services.AddScoped<IFileStorageService, FileStorageService>();
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.AddScoped<EmailVerification>();

        }
    }
}