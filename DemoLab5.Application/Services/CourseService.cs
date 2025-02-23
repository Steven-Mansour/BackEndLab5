using DemoLab5.Application.DTOs;
using DemoLab5.Application.Interfaces;
using DemoLab5.Domain.Entities;
using DemoLab5.Persistence.Interfaces;

namespace DemoLab5.Application.Services;

public class CourseService : ICourseService
{
    private readonly ICourseRepository _courseRepository;
    private readonly ITeacherRepository _teacherRepository;

    public CourseService(ICourseRepository courseRepository, ITeacherRepository teacherRepository)
    {
        _courseRepository = courseRepository;
        _teacherRepository = teacherRepository;
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
            throw new ArgumentException($"Teacher with ID {teacherID} does not exist.");
        }
        
        await _courseRepository.TeacherPicksCoursesAsync(courseID, timeSlot, teacherID);
    }

    public async Task<List<Course>> GetAllCoursesAsync()
    {
        var courses = await _courseRepository.GetAllAsync();
        return courses.ToList();
    }
    
}