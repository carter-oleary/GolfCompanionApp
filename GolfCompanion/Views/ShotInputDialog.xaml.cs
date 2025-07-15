using System;
using Microsoft.Maui.Controls;
using GolfCompanion.ViewModels;

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