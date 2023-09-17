using GroupTracker.data;
using Pomelo;
using Microsoft.EntityFrameworkCore;
using GroupTracker.Services.Abstraction;
using GroupTracker.Services.Implementation;
using GroupTracker.DTOs.Lecturer;
using GroupTracker.Services.Abstraction.Group;
using GroupTracker.Services.Implementation.Group;
using GroupTracker.Services.Abstraction.Syllabus;
using GroupTracker.Services.Implementation.Syllabus;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin();
            builder.AllowAnyHeader();
            builder.AllowAnyMethod();
        });
});
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(options =>
        options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"));






builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<Microsoft.AspNetCore.Identity.PasswordHasher<LecturerRegistrationInput>>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<ILecturerService, LecturerService>();

builder.Services.AddScoped<ILecturerGroupCoordinator, LecturerGroupCoordinator>();
builder.Services.AddScoped<ILectureSessionService, LecturerSessionService>();
builder.Services.AddScoped<IAlternateWeekService, AlternateWeekService>();
builder.Services.AddScoped<IGroupService, GroupService>();
builder.Services.AddScoped<ISyllabusTopicService, SyllabusTopicService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
