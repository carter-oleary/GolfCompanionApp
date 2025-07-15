using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GolfCompanion.Models
{
    public class Club
    {
        [Key]
        public int ClubId { get; set; }
        
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
        
        [StringLength(2)]
        public string ClubName { get; set; } = string.Empty;
        
        public double ClubDistance { get; set; }
        
        // Navigation properties
        
        public virtual ICollection<Shot> Shots { get; set; } = new List<Shot>();
    }
} 