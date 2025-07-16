using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GolfCompanion.Models;
using GolfCompanion.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolfCompanion.ViewModels
{
    public partial class StartupPageViewModel : ObservableObject
    {
        private readonly int _userId;
        [ObservableProperty]
        private ObservableCollection<Round> rounds;

        [ObservableProperty]
        private User user;

        private GolfDataService _golfDataService;
        public StartupPageViewModel(GolfDataService gds)
        {
            _golfDataService = gds;
            _userId = 1;
            User = _golfDataService.GetUserFromDatabaseAsync(_userId).Result;
        }

        [RelayCommand]
        public async Task AddNewRoundAsync()
        {
            await Shell.Current.GoToAsync("//SearchPage");
        }

        [RelayCommand]
        public async Task LoadRoundsAsync()
        {
            try
            {
                var roundsList = await _golfDataService.GetRoundsFromDatabaseAsync(_userId);
                Rounds = new ObservableCollection<Round>(roundsList);
                if (Rounds.Count == 0) await Shell.Current.DisplayAlert("Nada", "No Rounds Found", "Deep Sadness");
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log error, show message to user)
                await Shell.Current.DisplayAlert("Error", $"Error loading rounds: {ex.Message}", "OK");
            }

        }
    }
}
