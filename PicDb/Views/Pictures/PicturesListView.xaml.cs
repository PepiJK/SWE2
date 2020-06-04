using System.Windows.Controls;
using PicDb.ViewModels.Pictures;

namespace PicDb.Views.Pictures
{
    /// <summary>
    /// Interaction logic for ImagesListView.xaml
    /// </summary>
    public partial class PicturesListView : UserControl
    {
        public PicturesListView()
        {
            InitializeComponent();
            DataContext = new PicturesListViewModel();
        }
    }
}
