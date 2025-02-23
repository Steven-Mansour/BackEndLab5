using DemoLab5.Application.DTOs;
using DemoLab5.Domain.Entities;

namespace DemoLab5.Application.Interfaces;

public interface ITeacherService
{
    Task CreateTeacherAsync(CreateTeacherDTO dto);
    
    Task<Teacher> GetTeacherAsync(int teacherId);
    Task<List<Teacher>> GetTeachersAsync();
}