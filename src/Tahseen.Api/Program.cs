using Tahseen.Api.Extensions;
using Tahseen.Api.Middlewares;
using Tahseen.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Tahseen.Service.Helpers;
using Microsoft.AspNetCore.Http.Features;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//JWT
builder.Services.AddJwtService(builder.Configuration);
//Swagger
builder.Services.AddSwaggerService();
//Database Configuration
builder.Services.AddDbContext<AppDbContext>(option 
    => option.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
    );
builder.Services.AddCustomService();
var jsonFormatter = builder.Configuration.GetSection("Formatters:JsonFormatter");
jsonFormatter["SerializerSettings:DateFormatString"] = "dd-MM-yyyy HH:mm";

// MiddleWares
/*builder.Services.Configure<FormOptions>(options =>
{
    options.ValueLengthLimit = int.MaxValue;
    options.MultipartBodyLengthLimit = long.MaxValue; // In case of multipart
    options.MultipartHeadersLengthLimit = int.MaxValue; // Set the multipart headers length limit
});
builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxRequestBodySize = null; // Unlimited request body size
});
*/
// Logger
var logger = new LoggerConfiguration()
  .ReadFrom.Configuration(builder.Configuration)
  .Enrich.FromLogContext()
  .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admins", policy =>
    {
        policy.RequireRole("Admin");
    });
});

var app = builder.Build();
WebEnvironmentHost.WebRootPath = Path.GetFullPath("wwwroot");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlerMiddleWare>();
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
