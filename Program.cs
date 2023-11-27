using GroupTracker.data;
using Microsoft.EntityFrameworkCore;
using GroupTracker.Extensions;
using GroupTracker.ENV;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.FileProviders;

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
        options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=TrackerProd;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"));


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.
ServiceConfiguration.ConfigureServices(builder.Services, builder.Configuration);


var app = builder.Build();
app.Services.GetRequiredService<IOptions<AppSettings>>();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("AllowAllOrigins");
app.UseHttpsRedirection();

app.UseAuthorization();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
               Path.Combine(builder.Environment.ContentRootPath, "Uploads")),
    RequestPath = "/Images"
});
app.MapControllers();

app.Run();
