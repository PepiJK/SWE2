﻿using System.Windows.Controls;
using PicDb.ViewModels.Pictures;

namespace PicDb.Views.Pictures
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
        }
    }
}
