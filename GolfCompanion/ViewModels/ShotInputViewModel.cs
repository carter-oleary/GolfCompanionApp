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
    public partial class ShotInputViewModel : ObservableObject
    {
        [ObservableProperty]
        private Hole selectedHole;

        [ObservableProperty]
        private Shot newShot;
        [ObservableProperty]
        private ObservableCollection<Club> clubs;
        [ObservableProperty]
        private Club selectedClub;
        [ObservableProperty]
        private ObservableCollection<ShotType> shotTypes;
        [ObservableProperty]
        private ShotType selectedShotType;
        [ObservableProperty]
        private ObservableCollection<Lie> lies;
        [ObservableProperty]
        private Lie selectedLie;
        [ObservableProperty]
        private ObservableCollection<Result> results;
        [ObservableProperty]
        private Result selectedResult;

        public ShotInputViewModel()
        {
            // Initialize the selected hole and new shot
            SelectedHole = ShotInputService.GetSelectedHole() ?? new Hole();
            
        }

        [RelayCommand]
        public async Task AddShotAsync()
        {
            SelectedHole.Shots.Add(NewShot);
            await Shell.Current.GoToAsync("//RoundInputView");
        }

        [RelayCommand]
        async Task CancelShotAsync()
        {
            await Shell.Current.GoToAsync("//RoundInputView");
        }
    }
}
