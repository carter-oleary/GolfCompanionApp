using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GolfCompanion.Models
{
    public enum ShotType {
        Tee, Layup, Approach, Recovery, Chip, Putt 
    }
    public class Shot
    {
        [Key]
        public int ShotId { get; set; }
        
        [ForeignKey("Hole")]
        public int HoleId { get; set; }
        public Hole Hole { get; set; }
        
        [ForeignKey("Round")]
        public int RoundId { get; set; }
        public Round Round { get; set; }
        
        [ForeignKey("Club")]
        public int ClubId { get; set; }
        public Club Club { get; set; }

        public ShotType ShotType { get; set; }
        
        public required int Distance { get; set; }
        
        public string Lie { get; set; } = string.Empty;
        
        public string Result { get; set; } = string.Empty;
        
        public double StrokesGained { get; set; }
        
    }
} 