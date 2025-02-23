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

    public async Task<String> AssignGrade(int studentId, int courseId, decimal grade)
    {
        Enrollment enr = await _teacherRepository.AssignGrade(studentId, courseId, grade);
        if (enr != null)
        {
            bool canGoToFrance = enr.CanApplyToFrance;
            var avg = enr.avg;
            if (canGoToFrance) return $"Student {studentId} has an avg of {avg}/100 and can go to France";
            return $"Student {studentId} has an avg of {avg}/100 and CANNOT go to France";
        }

        return "Student is not enrolled in this class";
    }
    
}