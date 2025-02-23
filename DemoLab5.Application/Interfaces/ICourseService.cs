using DemoLab5.Application.DTOs;

namespace DemoLab5.Application.Interfaces;

public interface ICourseService
{
    Task CreateCourseAsync(CreateCourseDTO dto);
    Task TeacherPicksCoursesAsync(TeacherPicksCourseDTO dto);
}