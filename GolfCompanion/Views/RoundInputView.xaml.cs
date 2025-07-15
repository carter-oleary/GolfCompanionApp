using System;
using GolfCompanion.ViewModels;
using Microsoft.Maui.Controls;

namespace GolfCompanion.Views
{
    public partial class RoundInputView : ContentPage
    {
        public RoundInputView(RoundInputViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }

        private async void OnAddShotClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new ShotInputDialog());
        }

        private async void OnSaveRoundClicked(object sender, EventArgs e)
        {
            // Placeholder: Navigate back to SearchView
            await Navigation.PopAsync();
        }
    }
} 