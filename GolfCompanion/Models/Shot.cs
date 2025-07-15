using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GolfCompanion.Models
{
    public enum ShotType {
        Tee, Layup, Approach, Recovery, Chip, Putt 
    }
    public enum Lie
    {
        Tee, Fairway, Rough, Bunker, Penalty, Green
    }
    public enum Result
    {
        Left, Right, Short, Long, OnTarget
    }
    public class Shot
    {
        [Key]
        public int ShotId { get; set; }
        
        [ForeignKey("Hole")]
        public int HoleId { get; set; }
        public Hole? Hole { get; set; }
        
        [ForeignKey("Round")]
        public int RoundId { get; set; }
        public Round? Round { get; set; }
        
        [ForeignKey("Club")]
        public int ClubId { get; set; }
        public Club? Club { get; set; }

        public ShotType ShotType { get; set; }
        
        public required int Distance { get; set; }
        
        public Lie Lie { get; set; } 
        
        public Result Result { get; set; } 
        
        public double StrokesGained { get; set; }
        
    }
} 