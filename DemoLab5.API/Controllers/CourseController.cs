using Asp.Versioning;
using DemoLab5.Application.DTOs;
using DemoLab5.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.Extensions.Localization;

namespace DemoLab5.API.Controllers;

[ApiVersion("1.0")]
[ApiController]
[Route("api/courses")]
public class CourseController: ControllerBase
{
    private readonly ICourseService _courseService;
    private readonly IStringLocalizer<SharedResource> _localizer;

    public CourseController(ICourseService courseService, IStringLocalizer<SharedResource> localizer)
    {
        _courseService = courseService;
        _localizer = localizer;
    }

    [HttpGet]
    [Route("get-all-courses")]
    [EnableQuery]
    public async Task<IActionResult> GetAllCoursesAsync()
    {
        var courses = await _courseService.GetAllCoursesAsync();
        var str = _localizer["GetAllCourses"].Value;
        Console.WriteLine(str);

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
            return BadRequest(_localizer["Invalid data"].Value);

        await _courseService.CreateCourseAsync(dto);
        var str = _localizer["CreateCourse"].Value;
        return Ok(str);
    }

    [HttpPost]
    [Route("teacher-course")]
    public async Task<IActionResult> TeacherPicksCoursesAsync(TeacherPicksCourseDTO dto)
    {
        if (dto == null)
            return BadRequest(_localizer["Invalid data"].Value);
        
        await _courseService.TeacherPicksCoursesAsync(dto);
        var str = _localizer["Update course"].Value;
        return Ok(str);
        
    }
    
}