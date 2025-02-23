using DemoLab5.Application.DTOs;

namespace DemoLab5.Application.Interfaces;

public interface IStudentService
{
    Task CreateStudentAsync(CreateStudentDTO dto);
}