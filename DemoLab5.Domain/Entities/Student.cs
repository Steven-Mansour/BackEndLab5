namespace DemoLab5.Domain.Entities;

public class Student
{

    public int StudentId { get; set; }
    public required string FullName { get; set; }
    public string? ProfilePictureUrl { get; set; }
        
    public List<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}