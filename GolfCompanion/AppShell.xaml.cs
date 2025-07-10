namespace GolfCompanion;
using GolfCompanion.Views;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute("TeeSelectionDialog", typeof(TeeSelectionDialog));
		Routing.RegisterRoute("SearchPage", typeof(SearchPage));
	}
}
