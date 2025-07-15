﻿using Microsoft.Extensions.Logging;
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

		// Register ViewModels
		builder.Services.AddTransient<SearchViewModel>();
		builder.Services.AddTransient<TeeSelectionViewModel>();

		// Register Views
		builder.Services.AddTransient<SearchPage>();
		builder.Services.AddTransient<TeeSelectionDialog>();

		

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
