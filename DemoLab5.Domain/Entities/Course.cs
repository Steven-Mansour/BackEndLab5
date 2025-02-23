namespace DemoLab5.Domain.Entities;

public class Course
{
   
    public int CourseId { get; set; }
    public required string Title { get; set; }
        
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    
    public TimeOnly TimeSlot { get; set; }
    public int MaxCapacity { get; set; }

    public int? TeacherId { get; set; }
    public Teacher Teacher { get; set; }
        
    public List<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

}