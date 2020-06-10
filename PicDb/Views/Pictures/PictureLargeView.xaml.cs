using System.Windows.Controls;
using PicDb.ViewModels.Pictures;

namespace PicDb.Views.Pictures
{
    /// <summary>
    /// Interaction logic for PicturesLargeView.xaml
    /// </summary>
    public partial class PictureLargeView : UserControl
    {
        public PictureLargeView()
        {
            InitializeComponent();
            DataContext = new PictureLargeViewModel();
        }
    }
}
