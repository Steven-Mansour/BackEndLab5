using DemoLab5.Application.DTOs;
using DemoLab5.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DemoLab5.API.Controllers;

[ApiController]
[Route("api/courses")]
public class CourseController: ControllerBase
{
    private readonly ICourseService _courseService;

    public CourseController(ICourseService courseService)
    {
        _courseService = courseService;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateCourse([FromBody] CreateCourseDTO dto)
    {
        if (dto == null)
            return BadRequest("Invalid course data.");

        await _courseService.CreateCourseAsync(dto);
        return Ok("Course created successfully.");
    }

    [HttpPost]
    [Route("teacher-course")]
    public async Task<IActionResult> TeacherPicksCoursesAsync(TeacherPicksCourseDTO dto)
    {
        if (dto == null)
            return BadRequest("Invalid data.");
        
        await _courseService.TeacherPicksCoursesAsync(dto);
        return Ok("Courses updated successfully.");
        
    }
    
}