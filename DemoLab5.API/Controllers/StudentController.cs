using DemoLab5.Application.DTOs;
using DemoLab5.Application.Interfaces;
using DemoLab5.Persistence.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DemoLab5.API.Controllers;

[ApiController]
[Route("api/students")]
public class StudentController :ControllerBase
{
    private readonly IStudentService _studentService;

    public StudentController(IStudentService studentService)
    {
        _studentService = studentService;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateStudent([FromBody] CreateStudentDTO dto)
    {
        if (dto == null)
            return BadRequest("Invalid student data.");

        await _studentService.CreateStudentAsync(dto);
        return Ok("Student created successfully.");
    }
    
    
}