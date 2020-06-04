using System.Windows.Controls;
using PicDb.ViewModels.Pictures;

namespace PicDb.Views.Pictures
{
    /// <summary>
    /// Interaction logic for ExifView.xaml
    /// </summary>
    public partial class ExifView : UserControl
    {
        public ExifView()
        {
            InitializeComponent();
            DataContext = new ExifViewModel();
        }
    }
}
