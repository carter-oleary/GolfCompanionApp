using System;
using Microsoft.Maui.Controls;

namespace GolfCompanion.Views
{
    public partial class ShotInputDialog : ContentPage
    {
        public ShotInputDialog()
        {
            InitializeComponent();
            SaveButton.Clicked += async (s, e) => {
                await DisplayAlert("Save", "Shot saved (placeholder)", "OK");
                await Navigation.PopModalAsync();
            };
            CancelButton.Clicked += async (s, e) => {
                await Navigation.PopModalAsync();
            };
        }
    }
} 