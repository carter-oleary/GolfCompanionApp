using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SharedGolfClasses;

namespace GolfCompanion.Models
{
    public class Hole
    {
        [Key]
        public int HoleId { get; set; }
        
        [ForeignKey("Tee")]
        public int TeeId { get; set; }
        public Tee? Tee { get; set; }
        
        [Range(1, 18)]
        public int HoleNumber { get; set; }

        public int? Handicap { get; set; }  

        public int Par { get; set; }
        
        public int Length { get; set; }
        
        public string DisplayText => $"Hole {HoleNumber}";

        // Navigation properties        
        public virtual ICollection<Shot> Shots { get; set; } = new List<Shot>();

        
    }
} 