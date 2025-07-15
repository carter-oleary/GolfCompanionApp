using Microsoft.Extensions.Logging;
using GolfCompanion.Services;
using GolfCompanion.ViewModels;
using GolfCompanion.Views;
using GolfCompanion.Data;
using Microsoft.EntityFrameworkCore;

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

		// Register database context
		builder.Services.AddDbContext<GolfDbContext>(options =>
			options.UseSqlite($"Data Source={Path.Combine(FileSystem.AppDataDirectory, "GolfCompanion.db")}"));

		// Register services
		builder.Services.AddScoped<GolfDataService>();
		builder.Services.AddSingleton<CourseDetailService>();
        builder.Services.AddSingleton<CourseSearchService>();
		builder.Services.AddSingleton<TeeSelectionService>();
		builder.Services.AddSingleton<ShotInputService>();

        // Register ViewModels
        builder.Services.AddTransient<SearchViewModel>();
		builder.Services.AddTransient<TeeSelectionViewModel>();
		builder.Services.AddTransient<RoundInputViewModel>();
		builder.Services.AddTransient<ShotInputViewModel>();

        // Register Views
        builder.Services.AddTransient<SearchPage>();
		builder.Services.AddTransient<TeeSelectionDialog>();
		builder.Services.AddTransient<RoundInputView>();
		builder.Services.AddTransient<ShotInputDialog>();



#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
