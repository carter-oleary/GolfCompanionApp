namespace GolfCompanion;
using GolfCompanion.Views;
using CommunityToolkit.Maui;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute("TeeSelectionDialog", typeof(TeeSelectionDialog));
		Routing.RegisterRoute("SearchPage", typeof(SearchPage));
		Routing.RegisterRoute("RoundInputView", typeof(RoundInputView));
		Routing.RegisterRoute("ShotInputDialog", typeof(ShotInputDialog));
		Routing.RegisterRoute("StartupPage", typeof(StartupPage));
    }
}
