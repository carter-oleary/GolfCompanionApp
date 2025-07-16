using System;
using Microsoft.Maui.Controls;
using GolfCompanion.ViewModels;
using CommunityToolkit.Maui.Views;

namespace GolfCompanion.Views
{
    public partial class ShotInputDialog : ContentPage
    {
        public ShotInputDialog(ShotInputViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
} 