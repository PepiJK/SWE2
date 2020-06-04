using System.Windows.Controls;
using PicDb.ViewModels.Pictures;

namespace PicDb.Views.Pictures
{
    /// <summary>
    /// Interaction logic for IptcView.xaml
    /// </summary>
    public partial class IptcView : UserControl
    {
        public IptcView()
        {
            InitializeComponent();
            DataContext = new IptcViewModel();
        }
    }
}
