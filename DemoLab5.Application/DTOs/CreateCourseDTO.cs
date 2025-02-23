namespace DemoLab5.Application.DTOs;

public class CreateCourseDTO
{
    public string Title { get; set; } = string.Empty;
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int MaxStudents { get; set; }
}