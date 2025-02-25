using DemoLab5.Persistence.Interfaces;

namespace DemoLab5.Application.Services;

public interface IStudentGradeService
{
    Task RecalculateGradeAverages();
}
public class StudentGradeService: IStudentGradeService
{
    private readonly IEnrollmentRepository _enrollmentRepository;

    public StudentGradeService(IEnrollmentRepository enrollmentRepository)
    {
        _enrollmentRepository = enrollmentRepository;
    }
    public async Task RecalculateGradeAverages()
    {
        var enrollments = await _enrollmentRepository.GetEnrollmentsAsync();
        
        foreach (var enrollment in enrollments)
        {
            decimal averageGrade = enrollment.DecimalGrades.Any() 
                ? enrollment.DecimalGrades.Average() 
                : 0;

            Console.WriteLine($"Student ID: {enrollment.StudentId}, Course ID: {enrollment.CourseId}, Average Grade: {averageGrade:F2}");
            bool canApply = enrollment.DecimalGrades.Any() && averageGrade > 15;
            Console.WriteLine($"Can apply to France: {canApply}");
        }
        

    }
    
}