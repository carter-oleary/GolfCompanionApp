using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SharedGolfClasses;
using System.Collections.ObjectModel;
using GolfCompanion.Services;

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

        public SearchViewModel(CourseSearchService courseSearchService)
        {
            _courseSearchService = courseSearchService;
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
        private void SelectCourse(GolfCourse course)
        {
            // Handle course selection - you can navigate to a detail page or store the selection
            StatusMessage = $"Selected: {course.ClubName} - {course.CourseName}";
        }
    }
} 