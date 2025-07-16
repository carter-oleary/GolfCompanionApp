using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
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
        private int distance;
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

        private ShotInputService _shotInputService;
        private RoundInputService _roundInputService;

        private Round round;

        public ShotInputViewModel(ShotInputService sis, RoundInputService ris)
        {
            // Initialize the selected hole and new shot
            SelectedHole = ShotInputService.GetSelectedHole() ?? new Hole();
            _shotInputService = sis;   
            _roundInputService = ris;

            Clubs = new ObservableCollection<Club>(RoundInputService.GetClubs());
            Distance = ShotInputService.GetCurrentShotDistance(); 
            ShotTypes = new ObservableCollection<ShotType>(Enum.GetValues<ShotType>());
            Lies = new ObservableCollection<Lie>(Enum.GetValues<Lie>());
            Results = new ObservableCollection<Result>(Enum.GetValues<Result>());

            if(round == null)
            {
                round = RoundInputService.GetRound();
            }
            
        }

        [RelayCommand]
        public async Task AddShotAsync()
        {
            NewShot = new Shot
            {
                Round = round,
                HoleId = SelectedHole.HoleId,
                ClubId = SelectedClub.ClubId,
                ShotType = SelectedShotType,
                Lie = SelectedLie,
                Result = SelectedResult,
                Distance = Distance  // Default distance, can be set later
            };
            
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
