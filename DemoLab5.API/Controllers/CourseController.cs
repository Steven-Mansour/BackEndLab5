using DemoLab5.Application.DTOs;
using DemoLab5.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

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

    [HttpGet]
    [Route("get-all-courses")]
    [EnableQuery]
    public async Task<IActionResult> GetAllCoursesAsync()
    {
        var courses = await _courseService.GetAllCoursesAsync();
        return Ok(courses);
    }
    
    [HttpGet]
    [Route("get-course-by-id")]
    [EnableQuery]
    public async Task<IActionResult> GetCourseByIdAsync([FromQuery] int id)
    {
        var course = await _courseService.GetCourseByIdAsync(id);
        return Ok(course);
    }
    
    [HttpPost]
    [Route("create-course")]
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