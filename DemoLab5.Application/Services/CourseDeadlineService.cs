using DemoLab5.Infrastructure;
using DemoLab5.Persistence.Interfaces;
using DemoLab5.Persistence.Repositories;

namespace DemoLab5.Application.Services;

public interface ICourseDeadlineService
{
    Task NotifyStudents();
}
public class CourseDeadlineService: ICourseDeadlineService
{
    private readonly ICourseRepository _courseRepository;
    private readonly IStudentRepository _studentRepository;

    public CourseDeadlineService(ICourseRepository courseRepository, IStudentRepository studentRepository)
    {
        _courseRepository = courseRepository;   
        _studentRepository = studentRepository;
    }

    public async Task NotifyStudents()
    {
        var students = await _studentRepository.GetStudentsAsync();
        var courses = await _courseRepository.GetAllAsync();
        var upcomingCourses = courses.Where(c => c.EndTime > DateTime.UtcNow).ToList();
        var studentEmails = students.Select(s => s.Email).Where(email => !string.IsNullOrEmpty(email)).ToList();
        var courseDetails = string.Join("<br>", upcomingCourses.Select(c => $"{c.Title} - Ends on {c.EndTime:yyyy-MM-dd}"));

        var emailService = new EmailService(
            "smtp.gmail.com",
            587, 
            "",  // Email
            ""  // Password
            );
        
               await emailService.SendEmailAsync(studentEmails, "Registration Deadline", courseDetails);
               Console.WriteLine($"Sent email:  ==> {courseDetails}");
            
        
    }
}