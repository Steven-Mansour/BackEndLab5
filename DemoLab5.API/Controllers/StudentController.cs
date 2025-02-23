using DemoLab5.Application.DTOs;
using DemoLab5.Application.Interfaces;
using DemoLab5.Domain.Entities;
using DemoLab5.Persistence.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace DemoLab5.API.Controllers;

[ApiController]
[Route("api/students")]
public class StudentController :ControllerBase
{
    private readonly IStudentService _studentService;
    private readonly IEnrollmentService _enrollmentService;

    public StudentController(IStudentService studentService, IEnrollmentService enrollmentService)
    {
        _studentService = studentService;
        _enrollmentService = enrollmentService;
    }

    [HttpGet]
    [Route("student/{id:int}")]
    [EnableQuery]
    public async Task<IActionResult> GetStudent(int id)
    {
        var student = await _studentService.GetStudentAsync(id);
        return Ok(student);
    }

    [HttpGet]
    [Route("all-students")]
    [EnableQuery]
    public async Task<IActionResult> GetAllStudents()
    {
        var students = await _studentService.GetStudentsAsync();
        return Ok(students);
    }
    [HttpPost]
    public async Task<IActionResult> CreateStudent([FromBody] CreateStudentDTO dto)
    {
        if (dto == null)
            return BadRequest("Invalid student data.");

        await _studentService.CreateStudentAsync(dto);
        return Ok("Student created successfully.");
    }

    [HttpPost]
    [Route("enroll")]
    public async Task<IActionResult> EnrollStudent([FromBody] CreateEnrollmentDTO dto)
    {
        if(dto == null)
            return BadRequest("Invalid  data.");
        
        var str = await _enrollmentService.CreateEnrollmentAsync(dto);
        return Ok(str);
    }
    
    
    
    
}