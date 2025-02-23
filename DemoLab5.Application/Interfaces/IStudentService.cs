using DemoLab5.Application.DTOs;
using DemoLab5.Domain.Entities;

namespace DemoLab5.Application.Interfaces;

public interface IStudentService
{
    Task CreateStudentAsync(CreateStudentDTO dto);
    Task<Student> GetStudentAsync(int studentId);
    Task<List<Student>> GetStudentsAsync();
}