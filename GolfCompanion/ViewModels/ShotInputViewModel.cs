using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GolfCompanion.Models;
using GolfCompanion.Services;
using System;
using System.Collections.Generic;
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
