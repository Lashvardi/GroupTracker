using GroupTracker.data;
using Microsoft.EntityFrameworkCore;
using GroupTracker.Extensions;
using GroupTracker.ENV;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200") // Angular app's URL
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .AllowCredentials(); // Allow credentials
        });
});

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(options =>
        options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=TrackerProd;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"));


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.
ServiceConfiguration.ConfigureServices(builder.Services, builder.Configuration);


var app = builder.Build();
app.Services.GetRequiredService<IOptions<AppSettings>>();

app.UseHttpsRedirection();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
           Path.Combine(builder.Environment.ContentRootPath, "Uploads")),
    RequestPath = "/Images"
});

app.UseRouting(); // UseRouting comes first after static files and HTTPS redirection

app.UseCors("AllowSpecificOrigin");

app.UseAuthentication(); // Only if you have authentication
app.UseAuthorization();

app.UseEndpoints(endpoints => // UseEndpoints comes after UseRouting and authentication/authorization
{
    endpoints.MapControllers();
    endpoints.MapHub<ChatHub>("/chatHub");
});

app.UseSwagger();
app.UseSwaggerUI();

app.Run();
