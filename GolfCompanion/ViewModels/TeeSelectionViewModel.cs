using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GolfCompanion.Models;
using GolfCompanion.Services;
using SharedGolfClasses;
using System.Collections.ObjectModel;

namespace GolfCompanion.ViewModels
{
    public partial class TeeSelectionViewModel : ObservableObject
    {
        [ObservableProperty]
        private string courseName = string.Empty;

        [ObservableProperty]
        private string clubName = string.Empty;

        [ObservableProperty]
        private ObservableCollection<string> genderOptions = new();

        [ObservableProperty]
        private string selectedGender = string.Empty;

        [ObservableProperty]
        private ObservableCollection<SharedGolfClasses.Tee> availableTees = new();

        [ObservableProperty]
        private Round newRound;

        private readonly GolfCourse _selectedCourse;
        private readonly TeeSelectionService _selectedTeeService;

        public TeeSelectionViewModel(GolfCourse selectedCourse)
        {
            _selectedTeeService = new TeeSelectionService();
            _selectedCourse = selectedCourse;
            CourseName = selectedCourse.CourseName ?? "Unknown Course";
            ClubName = selectedCourse.ClubName ?? "Unknown Club";
            
            // Initialize gender options
            GenderOptions.Add("Male");
            GenderOptions.Add("Female");
            SelectedGender = "Male";
            
            // Load initial tees
            UpdateAvailableTees();
        }

        partial void OnSelectedGenderChanged(string value)
        {
            UpdateAvailableTees();
        }

        private void UpdateAvailableTees()
        {
            AvailableTees.Clear();
            
            if (_selectedCourse?.Tees == null) return;

            var tees = SelectedGender == "Male" ? _selectedCourse.Tees.Male : _selectedCourse.Tees.Female;
            
            if (tees != null)
            {
                foreach (var tee in tees)
                {
                    AvailableTees.Add(tee);
                }
            }
        }

        [RelayCommand]
        private async Task SelectTee(SharedGolfClasses.Tee selectedTee)
        {
            TeeSelectionService.SetSelectedTee(selectedTee);
            TeeSelectionService.SetGender(SelectedGender);
            
            await Shell.Current.DisplayAlert("Tee Selected", 
                $"Selected: {selectedTee.Tee_Name}\nCourse Rating: {selectedTee.Course_Rating}\nSlope Rating: {selectedTee.Slope_Rating}\nTotal Yards: {selectedTee.Total_Yards}", 
                "OK");
            
            // Close the dialog using absolute routing
            await Shell.Current.GoToAsync("//RoundInputView");
        }

        [RelayCommand]
        private async Task Cancel()
        {
            await Shell.Current.GoToAsync("//SearchPage");
        }
    }
} 