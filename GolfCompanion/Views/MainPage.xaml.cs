using GolfCompanion.ViewModels;
using GolfCompanion.Services;

namespace GolfCompanion.Views
{
    public partial class SearchPage : ContentPage
    {
        public SearchPage()
        {
            InitializeComponent();
            var courseSearchService = new CourseSearchService();
            BindingContext = new SearchViewModel(courseSearchService);
        }
    }
} 