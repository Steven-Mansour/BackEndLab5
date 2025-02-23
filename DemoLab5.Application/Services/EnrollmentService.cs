using DemoLab5.Application.DTOs;
using DemoLab5.Application.Interfaces;
using DemoLab5.Domain.Entities;
using DemoLab5.Persistence.Interfaces;

namespace DemoLab5.Application.Services;

public class EnrollmentService: IEnrollmentService
{
    private readonly IEnrollmentRepository _enrollmentRepository;
    private readonly ICourseRepository _courseRepository;
    private readonly IStudentRepository _studentRepository;

    public EnrollmentService(IEnrollmentRepository enrollmentRepository
    , ICourseRepository courseRepository, IStudentRepository studentRepository)
    {
        _enrollmentRepository = enrollmentRepository;
        _courseRepository = courseRepository;
        _studentRepository = studentRepository;
    }

    public async Task<String> CreateEnrollmentAsync(CreateEnrollmentDTO dto)
    {
        var studentExists = await _studentRepository.ExistAsync(dto.StudentId);
        if (!studentExists)
        {
            throw new ArgumentException($"Student with ID {dto.StudentId} does not exist.");
        }
        var courseExists = await _courseRepository.ExistsAsync(dto.CourseId);
        if (!courseExists)
        {
            throw new ArgumentException($"Course with ID {dto.CourseId} does not exist.");
        }
        var course = await _courseRepository.GetAsync(dto.CourseId);
        DateTime now = DateTime.Now;

        if (now >= course.StartTime && now <= course.EndTime && course.Enrollments.Count < course.MaxCapacity)
        {
            var enrollment = new Enrollment
            {
                CourseId = dto.CourseId,
                StudentId = dto.StudentId,
                DecimalGrades = [0]
            };
            await _enrollmentRepository.AddAsync(enrollment);
            return "You have successfully registered this course";
        }
        else
        {
            return "You are not allowed to enroll";
        }
       
    }
}