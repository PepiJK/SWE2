using System.Windows.Controls;
using PicDb.ViewModels;

namespace PicDb.Views
{
    public partial class PhotographerListView : UserControl
    {
        public PhotographerListView()
        {
            InitializeComponent();
            DataContext = new PhotographerListViewModel();
        }
    }
}