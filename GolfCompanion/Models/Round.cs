using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GolfCompanion.Models
{
    public class Round
    {
        [Key]
        public int RoundId { get; set; }
        
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User? User { get; set; }
        
        [ForeignKey("Tee")]
        public int TeeId { get; set; }
        public Tee? Tee { get; set; }
        
        public double SG_Tee { get; set; }
        
        public double SG_App { get; set; }
        
        public double SG_Short { get; set; }
        
        public double SG_Putt { get; set; }
        
        // Navigation properties        
        public virtual ICollection<Shot> Shots { get; set; } = new List<Shot>();
    }
} 