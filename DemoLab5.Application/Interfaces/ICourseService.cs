using DemoLab5.Application.DTOs;
using DemoLab5.Domain.Entities;

namespace DemoLab5.Application.Interfaces;

public interface ICourseService
{
    Task CreateCourseAsync(CreateCourseDTO dto);
    Task TeacherPicksCoursesAsync(TeacherPicksCourseDTO dto);
    
    Task<List<Course>> GetAllCoursesAsync();
}