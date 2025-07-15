using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Input;
using GolfCompanion.Services;
using SharedGolfClasses;
using GolfCompanion.Models;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GolfCompanion.ViewModels
{
    public partial class RoundInputViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Models.Hole> holes = new();

        [ObservableProperty]
        private Models.Hole selectedHole;

        private readonly SharedGolfClasses.Tee? _selectedTee;
        private readonly int _courseId;
        private readonly string _gender = TeeSelectionService.GetGender();
        private readonly ShotInputService _shotInputService;
        private readonly GolfDataService _golfDataService;

        public RoundInputViewModel()
        {
            _courseId = CourseSelectionService.GetSelectedCourse().CourseId;
            _golfDataService = new GolfDataService();
            _shotInputService = new ShotInputService();
            _selectedTee = TeeSelectionService.GetSelectedTee();
            var tee = _golfDataService.GetTeeFromDatabaseAsync(_courseId, _gender, _selectedTee.Tee_Name).Result;
            if (tee == null) return;
            else
            {
                Holes = new ObservableCollection<Models.Hole>(_golfDataService.GetHolesFromDatabaseAsync(tee.TeeId).Result);
            }
            SelectedHole = ShotInputService.GetSelectedHole() ;
            if (SelectedHole == null)
            {
                SelectedHole = Holes[0];
            }
        }

        [RelayCommand]
        private async Task AddShot()
        {
            // Open modal dialog for shot input (to be implemented)
            // On dialog result, add to SelectedHole.Shots
            ShotInputService.SetSelectedHole(SelectedHole);
            await Shell.Current.GoToAsync("//ShotInputDialog");
        }

        [RelayCommand]
        private async Task SaveRound()
        {
            // Persist all shots and round to database (to be implemented)
            // Navigate back to search view
            await Shell.Current.GoToAsync("//SearchView");
        }
    }

} 