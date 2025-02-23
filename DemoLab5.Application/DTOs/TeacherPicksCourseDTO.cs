namespace DemoLab5.Application.DTOs;

public class TeacherPicksCourseDTO
{
    public int CourseID { get; set; }
    public int TeacherID { get; set; }
    public TimeOnly TimeSlot { get; set; }
}