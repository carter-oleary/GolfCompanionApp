using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Maui.Views;
using GolfCompanion.ViewModels;
using GolfCompanion.Services;
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
        }

       
    }
} 