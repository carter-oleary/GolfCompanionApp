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
        [ObservableProperty]
        private string searchText = string.Empty;

        [ObservableProperty]
        private ObservableCollection<GolfCourse> searchResults = new();

        [ObservableProperty]
        private bool isSearching = false;

        [ObservableProperty]
        private string statusMessage = string.Empty;

        private readonly CourseSearchService _courseSearchService;
        private readonly CourseDetailService _courseDetailService;

        public SearchViewModel(CourseSearchService courseSearchService, CourseDetailService courseDetailService)
        {
            _courseSearchService = courseSearchService;
            _courseDetailService = courseDetailService;
        }

        [RelayCommand]
        private async Task Search()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                StatusMessage = "Please enter a course to search for";
                return;
            }

            IsSearching = true;
            StatusMessage = string.Empty;
            SearchResults.Clear();

            try
            {
                var courses = await _courseSearchService.SearchCoursesAsync(SearchText);

                if (courses != null && courses.Any())
                {
                    foreach (var course in courses)
                    {
                        SearchResults.Add(course);
                    }
                    StatusMessage = $"Found {courses.Count()} golf course(s)";
                }
                else
                {
                    StatusMessage = "No golf courses found";
                }
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
        private async Task SelectCourse(GolfCourse course)
        {
            try
            {
                // Get detailed course information including tees
                var detailedCourse = await _courseDetailService.GetCourseDetailsAsync(course.CourseId);
                
                if (detailedCourse != null)
                {
                    // Store the course in the selection service
                    CourseSelectionService.SetSelectedCourse(detailedCourse);
                    
                    // Navigate to TeeSelectionDialog using absolute routing
                    await Shell.Current.GoToAsync("//TeeSelectionDialog");
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", "Could not load course details", "OK");
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Error loading course details: {ex.Message}", "OK");
            }
        }
    }
} 