using GolfCompanion.ViewModels;
using GolfCompanion.Services;

namespace GolfCompanion.Views
{
    public partial class SearchPage : ContentPage
    {
        public SearchPage(SearchViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
} 