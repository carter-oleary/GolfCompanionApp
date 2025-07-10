using SharedGolfClasses;

namespace GolfCompanionAPI.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string? Club_Name { get; set; }
        public string? Course_Name { get; set; }
        public Location? Location { get; set; }
        public required Tees Tees { get; set; }

        public override string ToString()
        {
            return $"{Id}: {Club_Name} - {Course_Name}";
        }

        public GolfCourse ToGolfCourse()
        {
            var course = new GolfCourse
            {
                CourseId = this.Id,
                ClubName = this.Club_Name,
                CourseName = this.Course_Name,
                Tees = this.Tees
            };
            return course;
        }
    }
}
