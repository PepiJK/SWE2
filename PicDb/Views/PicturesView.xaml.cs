using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PicDb.ViewModels;

namespace PicDb.Views
{
    /// <summary>
    /// Interaction logic for ImagesView.xaml
    /// </summary>
    public partial class PicturesView : UserControl
    {
        public PicturesView()
        {
            InitializeComponent();
            DataContext = new PicturesViewModel();
            SearchViewControl.DataContext = new SearchViewModel();
            ImagesLargeViewControl.DataContext = new PictureLargeViewModel();
            ExifViewControl.DataContext = new ExifViewModel();
            IptcViewControl.DataContext = new IptcViewModel();
            ImagesListViewControl.DataContext = new PicturesListViewModel();
        }
    }
}
