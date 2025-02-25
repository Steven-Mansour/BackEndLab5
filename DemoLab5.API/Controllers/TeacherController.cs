using DemoLab5.Application.DTOs;
using DemoLab5.Application.Interfaces;
using DemoLab5.Domain.Entities;
using DemoLab5.Persistence.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.Extensions.Localization;

namespace DemoLab5.API.Controllers;

[ApiController]
[Route("api/teachers")]
public class TeacherController:ControllerBase
{
    private readonly ITeacherService _teacherService;
    private readonly IStringLocalizer<SharedResource> _localizer;

    public TeacherController(ITeacherService teacherService, IStringLocalizer<SharedResource> localizer)
    {
        _teacherService = teacherService;
        _localizer = localizer;
    }

    [HttpPost]
    public async Task<IActionResult> AddTeacher([FromBody] CreateTeacherDTO dto)
    {
        if (dto == null)
            return BadRequest(_localizer["Invalid data"].Value);

        await _teacherService.CreateTeacherAsync(dto);
        return Ok(_localizer["Create teacher"].Value);

    }

    [HttpGet]
    [Route("teacher-by-id")]
    [EnableQuery]
    public async Task<IActionResult> GetTeacherByIdAsync([FromQuery] int teacherId)
    {
        var teacher = await _teacherService.GetTeacherAsync(teacherId);
        return Ok(teacher);
    }
    
    [HttpGet]
    [Route("all-teachers")]
    [EnableQuery]
    public async Task<IActionResult> GetTeachers()
    {
        var teachers = await _teacherService.GetTeachersAsync();
        return Ok(teachers);
    }

    [HttpPost]
    [Route("assign-grade")]
    public async Task<IActionResult> AssignGrade([FromQuery] int courseId, [FromQuery] decimal grade,
        [FromQuery] int studentId)
    {
        var str = await _teacherService.AssignGrade(studentId, courseId, grade);
        return Ok(str);
    }

    
}