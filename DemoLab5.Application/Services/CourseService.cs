using DemoLab5.Application.DTOs;
using DemoLab5.Application.Interfaces;
using DemoLab5.Domain.Entities;
using DemoLab5.Persistence.Interfaces;

namespace DemoLab5.Application.Services;

public class CourseService : ICourseService
{
    private readonly ICourseRepository _courseRepository;

    public CourseService(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }
    public async Task CreateCourseAsync(CreateCourseDTO dto)
    {
        var course = new Course
        {
            Title = dto.Title,
            StartTime = dto.StartTime,
            EndTime = dto.EndTime,
            MaxCapacity = dto.MaxStudents
        };

        await _courseRepository.AddAsync(course);
        
    }
    
}