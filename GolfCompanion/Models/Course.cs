using System.ComponentModel.DataAnnotations;

namespace GolfCompanion.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }
        
        [Required]
        public string ClubName { get; set; } = string.Empty;
        
        [Required]
        public string CourseName { get; set; } = string.Empty;
        
        // Navigation properties
        public virtual ICollection<Tee> Tees { get; set; } = new List<Tee>();
    }
} 