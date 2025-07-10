using GolfCompanion.ViewModels;
using GolfCompanion.Services;
using SharedGolfClasses;

namespace GolfCompanion.Views
{
    public partial class TeeSelectionDialog : ContentPage
    {
        public TeeSelectionDialog()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigatedToEventArgs args)
        {
            base.OnNavigatedTo(args);
            
            // Get the course from the selection service
            var selectedCourse = CourseSelectionService.GetSelectedCourse();
            if (selectedCourse != null)
            {
                BindingContext = new TeeSelectionViewModel(selectedCourse);
            }
        }
    }
} 