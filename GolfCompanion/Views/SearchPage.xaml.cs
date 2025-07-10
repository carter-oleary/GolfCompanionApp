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
            var courseDetailService = new CourseDetailService();
            BindingContext = new SearchViewModel(courseSearchService, courseDetailService);
        }
    }
} 