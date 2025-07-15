using System.ComponentModel.DataAnnotations;

namespace GolfCompanion.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        public string FirstName { get; set; } = string.Empty;
        
        [Required]
        public string LastName { get; set; } = string.Empty;
        
        public double Handicap { get; set; }
        
        // Navigation properties
        public virtual ICollection<Club> Clubs { get; set; } = new List<Club>();
        public virtual ICollection<Round> Rounds { get; set; } = new List<Round>();
    }
} 