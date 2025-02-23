using DemoLab5.Application.DTOs;

namespace DemoLab5.Application.Interfaces;

public interface ITeacherService
{
    Task CreateTeacherAsync(CreateTeacherDTO dto);
}