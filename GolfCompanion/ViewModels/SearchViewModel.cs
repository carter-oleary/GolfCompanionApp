using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SharedGolfClasses;
using System.Collections.ObjectModel;
using GolfCompanion.Services;
using GolfCompanion.Views;

namespace GolfCompanion.ViewModels
{
    public partial class SearchViewModel : ObservableObject
    {
        private readonly CourseSearchService _courseSearchService;
        private readonly CourseDetailService _courseDetailService;
        private readonly CourseSelectionService _courseSelectionService;
        private readonly GolfDataService _golfDataService;

        [ObservableProperty]
        private string searchTerm = string.Empty;

        [ObservableProperty]
        private bool isSearching = false;

        [ObservableProperty]
        private ObservableCollection<GolfCourse> searchResults = new();

        [ObservableProperty]
        private string statusMessage = string.Empty;

        public SearchViewModel()
        {
            _courseSearchService = new CourseSearchService();
            _courseDetailService = new CourseDetailService();
            _courseSelectionService = new CourseSelectionService();
            _golfDataService = new GolfDataService();
        }

        [RelayCommand]
        private async Task SearchAsync()
        {
            if (string.IsNullOrWhiteSpace(SearchTerm))
                return;

            IsSearching = true;
            StatusMessage = string.Empty;
            SearchResults.Clear();

            try
            {
                var results = await _courseSearchService.SearchCoursesAsync(SearchTerm);
                foreach (var course in results)
                {
                    SearchResults.Add(course);
                }
                StatusMessage = $"Found {results.Count()} golf course(s)";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: {ex.Message}";
            }
            finally
            {
                IsSearching = false;
            }
        }

        [RelayCommand]
        private async Task SelectCourseAsync(GolfCourse course)
        {
            if (course == null) return;

            try
            {
                // Get detailed course information
                var detailedCourse = await _courseDetailService.GetCourseDetailsAsync(course.CourseId);
                
                if (detailedCourse != null)
                {
                    // Save course and tees to local database
                    await _golfDataService.SaveCourseAndTeesAsync(detailedCourse);
                    
                    // Store the selected course
                    CourseSelectionService.SetSelectedCourse(detailedCourse);
                    
                    // Navigate to tee selection
                    await Shell.Current.GoToAsync("//TeeSelectionDialog");
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", "Could not load course details", "OK");
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Failed to load course details: {ex.Message}", "OK");
            }
        }
    }
} 