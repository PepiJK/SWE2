using System.Windows.Controls;
using PicDb.ViewModels;

namespace PicDb.Views
{
    public partial class PhotographerInfoView : UserControl
    {
        public PhotographerInfoView()
        {
            InitializeComponent();
            DataContext = new PhotographerInfoViewModel();
            
        }
    }
}