using Asp.Versioning;
using DemoLab5.Application.DTOs;
using DemoLab5.Application.Interfaces;
using DemoLab5.Domain.Entities;
using DemoLab5.Persistence.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.Extensions.Localization;

namespace DemoLab5.API.Controllers;

[ApiVersion("1.0")]
[ApiController]
[Route("api/students")]
public class StudentController :ControllerBase
{
    private readonly IStudentService _studentService;
    private readonly IEnrollmentService _enrollmentService;
    private readonly IWebHostEnvironment _env;
    private readonly IStringLocalizer<SharedResource> _localizer;
    public StudentController(IStudentService studentService, IEnrollmentService enrollmentService,
        IWebHostEnvironment env, IStringLocalizer<SharedResource> localizer)
    {
        _studentService = studentService;
        _enrollmentService = enrollmentService;
        _env = env;
        _localizer = localizer;
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
        return Ok(_localizer["Create student"].Value);
    }

    [HttpPost]
    [Route("enroll")]
    public async Task<IActionResult> EnrollStudent([FromBody] CreateEnrollmentDTO dto)
    {
        if(dto == null)
            return BadRequest(_localizer["Invalid data"].Value);
        
        var str = await _enrollmentService.CreateEnrollmentAsync(dto);
        return Ok(str);
    }

    [HttpPost]
    [Route("upload-image")]
    public async Task<IActionResult> UploadImage(IFormFile file, int studentID)
    {
        
        string uploadsFolder = Path.Combine("wwwroot", "profile-pictures");
        if (!Directory.Exists(uploadsFolder))
            Directory.CreateDirectory(uploadsFolder);
        var uniqueFileName = $"{studentID}_{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var filePath = Path.Combine(uploadsFolder, uniqueFileName);
        bool isValid = await _studentService.UploadProfilePicAsync(studentID, $"/profile-pictures/{uniqueFileName}");
        if (isValid)
        {
             using (var fileStream = new FileStream(filePath, FileMode.Create))
             { 
                 await file.CopyToAsync(fileStream);
             }
             return Ok(_localizer["Update pic"].Value);
        }
        return BadRequest(_localizer["Something wrong"].Value);
       

    }
    
    
    
    
}