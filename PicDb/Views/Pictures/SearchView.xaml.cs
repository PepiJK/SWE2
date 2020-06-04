using System.Windows.Controls;
using PicDb.ViewModels.Pictures;

namespace PicDb.Views.Pictures
{
    /// <summary>
    /// Interaction logic for SearchView.xaml
    /// </summary>
    public partial class SearchView : UserControl
    {
        public SearchView()
        {
            InitializeComponent();
            DataContext = new SearchViewModel();
        }

    }
}
