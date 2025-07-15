using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GolfCompanion.Models
{
    public class Tee
    {
        [Key]
        public int TeeId { get; set; }
        
        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public Course? Course { get; set; }
        
        public string Gender { get; set; } = string.Empty;
        
        [Required]
        public string TeeName { get; set; } = string.Empty;
        
        public double CourseRating { get; set; }
        
        public int SlopeRating { get; set; }
        
        public int Par { get; set; }
        
        // Navigation properties        
        public virtual ICollection<Round> Rounds { get; set; } = new List<Round>();
        public virtual ICollection<Hole> Holes { get; set; } = new List<Hole>();
    }
} 