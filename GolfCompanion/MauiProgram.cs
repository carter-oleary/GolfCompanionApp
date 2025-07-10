using Microsoft.Extensions.Logging;
using GolfCompanion.Services;
using GolfCompanion.ViewModels;
using GolfCompanion.Views;

namespace GolfCompanion;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif
		builder.Services.AddSingleton<CourseSelectionService>();
		builder.Services.AddSingleton<CourseDetailService>();
		builder.Services.AddSingleton<CourseSearchService>();
		builder.Services.AddSingleton<SearchViewModel>();
		builder.Services.AddSingleton<TeeSelectionViewModel>();
		builder.Services.AddSingleton<TeeSelectionDialog>();
		builder.Services.AddSingleton<SearchPage>();

		return builder.Build();
	}
}
