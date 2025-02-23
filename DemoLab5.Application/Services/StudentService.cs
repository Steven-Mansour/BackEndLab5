using DemoLab5.Application.DTOs;
using DemoLab5.Application.Interfaces;
using DemoLab5.Domain.Entities;
using DemoLab5.Persistence.Interfaces;

namespace DemoLab5.Application.Services;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _studentRepository;

    public StudentService(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }
  
    public async Task CreateStudentAsync(CreateStudentDTO dto)
    {
        var student = new Student
        {
            FullName = dto.Fullname
        };

        await _studentRepository.AddAsync(student);
    }
}