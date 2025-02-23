using DemoLab5.Application.DTOs;

namespace DemoLab5.Application.Interfaces;

public interface IEnrollmentService
{

    Task CreateEnrollmentAsync(CreateEnrollmentDTO dto);

}