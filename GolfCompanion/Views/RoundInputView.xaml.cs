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

       
    }
} 