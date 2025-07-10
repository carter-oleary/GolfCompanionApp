using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedGolfClasses
{
    public class Hole
    {
        public required int Par { get; set; }
        public required int Yardage { get; set; }
        public int? Handicap { get; set; } 
    }
}
