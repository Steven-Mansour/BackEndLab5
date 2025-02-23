using DemoLab5.Application.DTOs;
using DemoLab5.Application.Interfaces;
using DemoLab5.Domain.Entities;
using DemoLab5.Persistence.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DemoLab5.API.Controllers;

[ApiController]
[Route("api/teachers")]
public class TeacherController:ControllerBase
{
    public readonly ITeacherService _teacherService;

    public TeacherController(ITeacherService teacherService)
    {
        _teacherService = teacherService;
    }

    [HttpPost]
    public async Task<IActionResult> AddTeacher([FromBody] CreateTeacherDTO dto)
    {
        if (dto == null)
            return BadRequest("Invalid teacher data.");

        await _teacherService.CreateTeacherAsync(dto);
        return Ok("Teacher created successfull");

    }
    
}