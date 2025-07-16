using GolfCompanion.ViewModels;

namespace GolfCompanion.Views;

public partial class StartupPage : ContentPage
{
	public StartupPage(StartupPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
    }
}