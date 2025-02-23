using DemoLab5.Application.DTOs;
using DemoLab5.Application.Interfaces;
using DemoLab5.Domain.Entities;
using DemoLab5.Persistence.Interfaces;

namespace DemoLab5.Application.Services;

public class TeacherService:ITeacherService
{
    private readonly ITeacherRepository _teacherRepository;

    public TeacherService(ITeacherRepository teacherRepository)
    {
        _teacherRepository = teacherRepository;
    }

    public async Task CreateTeacherAsync(CreateTeacherDTO dto)
    {
        var teacher = new Teacher
        {
            FullName = dto.FullName,
        };
        
        await _teacherRepository.AddAsync(teacher);
    }

    public async Task<Teacher> GetTeacherAsync(int teacherId)
    {
        var teacher = await _teacherRepository.GetTeacherAsync(teacherId);
        return teacher;
    }

    public async Task<List<Teacher>> GetTeachersAsync()
    {
        var teachers = await _teacherRepository.GetTeachersAsync();
        return teachers.ToList();
    }
    
}