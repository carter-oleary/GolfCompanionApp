using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Input;
using GolfCompanion.Services;
using SharedGolfClasses;
using GolfCompanion.Models;
using Microsoft.EntityFrameworkCore.Metadata;
using CommunityToolkit.Maui.Views;
using GolfCompanion.Views;
using CommunityToolkit.Mvvm.Messaging;

namespace GolfCompanion.ViewModels
{
    public partial class RoundInputViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Models.Hole> holes = new();

        [ObservableProperty]
        private Models.Hole selectedHole;

        private List<Shot> _shots = new List<Shot>();
        private readonly SharedGolfClasses.Tee? _selectedTee;
        private Models.Tee? _tee;
        private readonly int _courseId;
        private readonly int userId = 1; // Placeholder for user ID, should be replaced with actual user context
        private readonly string _gender = TeeSelectionService.GetGender();
        private readonly ShotInputService _shotInputService;
        private readonly RoundInputService _roundInputService;
        private readonly GolfDataService _golfDataService;

        [ObservableProperty]
        private Round round;

        public RoundInputViewModel(GolfDataService gds, ShotInputService sis, RoundInputService ris)
        {
            _courseId = CourseSelectionService.GetSelectedCourse().CourseId;
            _golfDataService = gds;
            _shotInputService = sis;
            _roundInputService = ris;
            _selectedTee = TeeSelectionService.GetSelectedTee();
            _tee = _golfDataService.GetTeeFromDatabaseAsync(_courseId, _gender, _selectedTee.Tee_Name).Result;
            if (_tee == null) return;
            else
            {
                Holes = new ObservableCollection<Models.Hole>(_golfDataService.GetHolesFromDatabaseAsync(_tee.TeeId).Result);
            }
            SelectedHole = ShotInputService.GetSelectedHole() ;
            if (SelectedHole == null)
            {
                SelectedHole = Holes[0];
            }
            Round = new Round
            {
                UserId = userId,
                TeeId = _tee.TeeId,
            };
            _golfDataService.SaveRoundAsync(Round).Wait();
            RoundInputService.SetRound(Round);
        }

        [RelayCommand]
        private async Task AddShot()
        {
            // Open modal dialog for shot input (to be implemented)
            // On dialog result, add to SelectedHole.Shots
            ShotInputService.SetSelectedHole(SelectedHole);
            ShotInputService.SetCurrentShotDistance();
            await Shell.Current.GoToAsync("//ShotInputDialog");

        }
        

        [RelayCommand]
        private async Task SaveRound()
        {
            // Persist all shots and round to database (to be implemented)
            foreach(var hole in Holes)
            {
                if(hole.Shots.Count == 0)
                {
                    await Shell.Current.DisplayAlert("Missing Shots", $"Please add shots for hole {hole.HoleNumber} before saving the round.", "OK");
                    _shots.Clear();
                    return;
                }
                
                _shots.AddRange(hole.Shots);
            }
            // Navigate back to search view
            
            foreach(var hole in Holes)
            {
                foreach (var shot in hole.Shots)
                { 
                    await _golfDataService.AddShotToDatabaseAsync(shot);
                }
            }
            
            // Strokes gained logic to be added here

            // Save the round to the database
            await _golfDataService.SaveRoundAsync(Round);
            await Shell.Current.DisplayAlert("Round Saved", $"You shot a {Round.Score} for a total of {(Round.Score - _tee.Par > 0 ? $"+{Round.Score - _tee.Par}" : (Round.Score - _tee.Par < 0 ? $"{round.Score - _tee.Par}" : "E"))}", "OK");
            await Shell.Current.GoToAsync("//SearchPage");
        }

    }
    

} 