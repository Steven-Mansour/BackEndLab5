using DemoLab5.Application.Interfaces;
using DemoLab5.Application.Services;
using DemoLab5.Persistence.Context;
using DemoLab5.Persistence.Interfaces;
using DemoLab5.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OData;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddOData(options => options
        .Select() 
        .Filter() 
        .OrderBy()
        .Expand() 
        .Count()) 
    ;
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<ICourseService, CourseService>();
builder.Services.AddTransient<ICourseRepository, CourseRepository>();
builder.Services.AddTransient<IStudentService, StudentService>();
builder.Services.AddTransient<IStudentRepository, StudentRepository>();
builder.Services.AddTransient<ITeacherService, TeacherService>();
builder.Services.AddTransient<ITeacherRepository, TeacherRepository>();
builder.Services.AddTransient<IEnrollmentService, EnrollmentService>();
builder.Services.AddTransient<IEnrollmentRepository, EnrollmentRepository>();

builder.Services.AddDbContext<MapDbContext>(options =>
    options.UseNpgsql(("Host=localhost;Port=5432;Database=lab5Db;Username=steven;Password=0000")));


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