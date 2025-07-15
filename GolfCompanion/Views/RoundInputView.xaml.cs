using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Maui.Views;
using GolfCompanion.ViewModels;
using GolfCompanion.Views;
using Microsoft.Maui.Controls;
using System;
using CommunityToolkit.Maui.Extensions;

namespace GolfCompanion.Views
{
    public partial class RoundInputView : ContentPage
    {
        public RoundInputView(RoundInputViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;

            WeakReferenceMessenger.Default.Register<ShowShotInputPopupMessage>(this, async (recipient, message) =>
            {
                var popup = new ShotInputDialog(new ShotInputViewModel());
                // Optionally pass message.SelectedHole to the popup if needed
                await this.ShowPopupAsync(popup);
            });
        }

       
    }
} 