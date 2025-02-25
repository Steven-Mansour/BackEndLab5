using System.Globalization;
using Asp.Versioning;
using DemoLab5.Application.Interfaces;
using DemoLab5.Application.Services;
using DemoLab5.Persistence.Context;
using DemoLab5.Persistence.Interfaces;
using DemoLab5.Persistence.Repositories;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OData;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Serilog;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Odata
builder.Services.AddControllers()
    .AddOData(options => options
        .Select() 
        .Filter() 
        .OrderBy()
        .Expand() 
        .Count()) 
    ;
//HangFire
builder.Services.AddHangfire(config =>
{
    config.UseMemoryStorage();
});
builder.Services.AddHangfireServer();
builder.Services.AddScoped<IStudentGradeService, StudentGradeService>();

//Api Versioning
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ApiVersionReader = ApiVersionReader.Combine(
        new UrlSegmentApiVersionReader(),
        new HeaderApiVersionReader("X-Api-Version")
    );
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

//Redis
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost:6379";  
    options.InstanceName = "Lab5";  
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API - V1", Version = "v1.0" });
        c.SwaggerDoc("v2", new OpenApiInfo { Title = "My API - V2", Version = "v2.0" });
    });
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.AddTransient<ICourseService, CourseService>();
builder.Services.AddTransient<ICourseRepository, CourseRepository>();
builder.Services.AddTransient<IStudentService, StudentService>();
builder.Services.AddTransient<IStudentRepository, StudentRepository>();
builder.Services.AddTransient<ITeacherService, TeacherService>();
builder.Services.AddTransient<ITeacherRepository, TeacherRepository>();
builder.Services.AddScoped<IEnrollmentService, EnrollmentService>();
builder.Services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();

builder.Services.AddDbContext<MapDbContext>(options =>
    options.UseNpgsql(("Host=localhost;Port=5432;Database=lab5Db;Username=steven;Password=0000")));
builder.Services.AddMemoryCache();
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(Log.Logger);
builder.Host.UseSerilog();
Log.Information("Application is starting...");

var supportedCultures = new[] {
    new CultureInfo("en-US"),
    new CultureInfo("fr-FR")
};
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("en-US");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
    options.RequestCultureProviders.Insert(0, new QueryStringRequestCultureProvider());
   });
var app = builder.Build();

var locOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>()?.Value;
app.UseRequestLocalization(locOptions);
app.UseHangfireDashboard();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

RecurringJob.AddOrUpdate<IStudentGradeService>(
    "RecalculateGradeAvg",
    service => service.RecalculateGradeAverages(),
    Cron.Hourly
    );

app.Run();