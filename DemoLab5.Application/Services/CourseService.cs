using System.Runtime.CompilerServices;
using DemoLab5.Application.DTOs;
using DemoLab5.Application.Interfaces;
using DemoLab5.Domain.Entities;
using DemoLab5.Persistence.Interfaces;
using Microsoft.Extensions.Logging;

namespace DemoLab5.Application.Services;

public class CourseService : ICourseService
{
    private readonly ICourseRepository _courseRepository;
    private readonly ITeacherRepository _teacherRepository;
    private readonly ILogger<CourseService> _logger;
    public CourseService(ICourseRepository courseRepository, ITeacherRepository teacherRepository, ILogger<CourseService> logger)
    {
        _courseRepository = courseRepository;
        _teacherRepository = teacherRepository;
        _logger = logger;
    }

    public async Task<Course> GetCourseByIdAsync(int id)
    {
        var course = await _courseRepository.GetAsync(id);
        _logger.LogInformation("Fetched course with id: {id}", id);
        return course;
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
        _logger.LogInformation("Creating course: {name}", dto.Title);

        await _courseRepository.AddAsync(course);
        
    }

    public async Task TeacherPicksCoursesAsync(TeacherPicksCourseDTO dto)
    {
        var courseID = dto.CourseID;
        var teacherID = dto.TeacherID;
        TimeOnly timeSlot = dto.TimeSlot;
        
        var courseExists = await _courseRepository.ExistsAsync(courseID);
        if (!courseExists)
        {
            throw new ArgumentException($"Course with ID {courseID} does not exist.");
        }
        
        var teacherExists = await _teacherRepository.ExistsAsync(teacherID);
        if (!teacherExists)
        {
            _logger.LogWarning($"Teacher with ID {teacherID} does not exist.");
            throw new ArgumentException($"Teacher with ID {teacherID} does not exist.");
        }
        
        await _courseRepository.TeacherPicksCoursesAsync(courseID, timeSlot, teacherID);
        _logger.LogInformation("Teacher {teacherID} is trying to pick course {courseID}", teacherID, courseID);
    }

    public async Task<List<Course>> GetAllCoursesAsync()
    {
        var courses = await _courseRepository.GetAllAsync();
        return courses.ToList();
    }
    
}