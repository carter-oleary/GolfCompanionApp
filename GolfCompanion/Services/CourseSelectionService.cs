using SharedGolfClasses;

namespace GolfCompanion.Services
{
    public class CourseSelectionService
    {
        private static GolfCourse? _selectedCourse;

        public static void SetSelectedCourse(GolfCourse course)
        {
            _selectedCourse = course;
        }

        public static GolfCourse? GetSelectedCourse()
        {
            var course = _selectedCourse;
            _selectedCourse = null; // Clear after getting
            return course;
        }
    }
} 