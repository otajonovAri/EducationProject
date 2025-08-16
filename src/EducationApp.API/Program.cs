using EducationApp.Application.DIContainer;
using EducationApp.Application.Helpers;
using EducationApp.Application.Helpers.GenerateJwt;
using EducationApp.Application.Helpers.PasswordHasher;
using EducationApp.Application.Helpers.Seeder;
using EducationApp.Application.MappingProfiles;
using EducationApp.Application.Service.FileStorageServices;
using EducationApp.Application.Settings;
using EducationApp.DataAccess.Database;
using EducationApp.DataAccess.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Minio;
using Newtonsoft.Json;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Serilog 
var fileName = "logs/log";

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Debug()
    .WriteTo.Console()
    .WriteTo.File(path: $"{fileName}-.txt",
        restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information,
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
        rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

// Serilog End

var configuration = builder.Configuration;

builder.Services.AddDatabase(builder.Configuration)
    .ServiceContainer()
    .AddJwtOption(builder.Configuration)
    .AddAuth(builder.Configuration)
    .AddEmailConfiguration(builder.Configuration);

builder.Services.AddAutoMapper(typeof(AttendanceProfile).Assembly);

builder.Services.AddControllers()
    .AddNewtonsoftJson(opt =>
        opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

// Allow Frontend CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.Configure<JwtOption>(builder.Configuration.GetSection("JwtOption"));

// Minio
builder.Services.AddSingleton<IFileStorageService, MinioFileStorageService>();
builder.Services.Configure<MinioSettings>(configuration.GetSection("MinioSettings"));

builder.Services.AddSingleton<IMinioClient>(sp =>
{
    var minioSettings = sp.GetRequiredService<IOptions<MinioSettings>>().Value;

    // MinioClient obyektini yaratish
    var client = new MinioClient()
        .WithEndpoint(minioSettings.Endpoint)
        .WithCredentials(minioSettings.AccessKey, minioSettings.SecretKey);

    // Agar SSL yoqilgan bo'lsa
    if (minioSettings.UseSSL)
    {
        client = client.WithSSL();
    }

    return client.Build(); // MinioClient ni qurish
});

builder.Services.AddSwaggerGen(c =>
{

    // API haqida umumiy ma'lumotlar
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "EducationApi",
        Version = "v1"
    });

    // JWT Bearer autentifikatsiyasini qo'shish
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: 'Bearer {your token}'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    // JWT Bearer uchun global xavfsizlik talabini qo'shish
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
{
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                    Scheme = "oauth2", // Bu shart emas, lekin qoldirilsa zarar qilmaydi
                    Name = "Bearer",
                    In = ParameterLocation.Header
            },
            new List<string>() // Bu yerda scope'lar bo'lishi mumkin, hozir bo'sh
        }
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


/// seeed
using var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<AppSeeder>();
await seeder.SeedAsync();

/*var scope = app.Services.CreateScope();
if (app.Environment.IsDevelopment())
{
    var context = scope.ServiceProvider.GetRequiredService<EduDbContext>();
    await context.Database.MigrateAsync();

    var passwordHasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher>();
    var databaseSeeder = new DatabaseSeeder(passwordHasher, context);
    databaseSeeder.SeedData();
}*/

if (builder.Environment.IsProduction() && builder.Configuration.GetValue<int?>("PORT") is not null)
    builder.WebHost.UseUrls($"http://*:{builder.Configuration.GetValue<int>("PORT")}");

/*using (var scopeSeeder = app.Services.CreateScope())
{
    var db = scopeSeeder.ServiceProvider.GetRequiredService<EduDbContext>();
    await PermissionSeeder.SeedPermissionsAsync(db);
}*/

app.UseSwagger();
app.UseSwaggerUI();

// Allow CORS for all origins, methods, and headers
app.UseCors("AllowAll");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();


// Connection String :)
/*
 *  "DefaultConnection": "Host=switchback.proxy.rlwy.net;Port=57276;Username=postgres;Password=ducPUjbwfBICWaKaHRefZiBkCPmIWRTO;Database=railway"
 */