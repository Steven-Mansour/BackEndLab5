using System.ComponentModel.DataAnnotations.Schema;

namespace DemoLab5.Domain.Entities;

public class Enrollment
{
    public int StudentId { get; set; }
    public Student Student { get; set; }
        
    public int CourseId { get; set; }
    public Course Course { get; set; }
        
    public decimal[] DecimalGrades { get; set; } = new decimal[0];
    
    [NotMapped] 
    public bool CanApplyToFrance => DecimalGrades.Length > 0 && DecimalGrades.Average() > 15;
    [NotMapped] 
    public decimal avg => DecimalGrades.Average();
}