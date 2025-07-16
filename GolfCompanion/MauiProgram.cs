using Microsoft.Extensions.Logging;
using GolfCompanion.Services;
using GolfCompanion.ViewModels;
using GolfCompanion.Views;
using GolfCompanion.Data;
using Microsoft.EntityFrameworkCore;
using CommunityToolkit.Maui;

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
		builder.UseMauiApp<App>().UseMauiCommunityToolkit();
        // Register database context
        builder.Services.AddDbContext<GolfDbContext>(options =>
        options.UseSqlite($"Data Source={Path.Combine(FileSystem.AppDataDirectory, "GolfCompanion.db")}"));

		Console.WriteLine($"DB Path: {Path.Combine(FileSystem.AppDataDirectory, "GolfCompanion.db")}");

        // Register services
        builder.Services.AddSingleton<GolfDataService>();
		builder.Services.AddSingleton<CourseDetailService>();
        builder.Services.AddSingleton<CourseSearchService>();
		builder.Services.AddSingleton<CourseSelectionService>();
        builder.Services.AddSingleton<TeeSelectionService>();
		builder.Services.AddSingleton<ShotInputService>();
		builder.Services.AddSingleton<RoundInputService>();
		

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
		var app = builder.Build();

		var loggerFactory = app.Services.GetService<ILoggerFactory>();
        var logger = loggerFactory.CreateLogger("Startup");
		logger.LogInformation($"DB Path: {Path.Combine(FileSystem.AppDataDirectory, "GolfCompanion.db")}");


        return app;
	}
}
