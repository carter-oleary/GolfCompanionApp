using SharedGolfClasses;

namespace GolfCompanion.Services
{
    public class TeeSelectionService
    {
        private static Tee? _selectedTee;
        private static string _gender = string.Empty;
        public static void Clear() => _selectedTee = null;
        public static Tee? GetSelectedTee()
        {
            var tee = _selectedTee;
            _selectedTee = null; // Clear after getting to ensure single use
            return tee;
        }

        public static void SetSelectedTee(Tee tee)
        {
            _selectedTee = tee;
        }

        public static void SetGender(string gender)
        {
            _gender = gender;
        }

        public static string GetGender()
        {
            return _gender;
        }

    }
} 