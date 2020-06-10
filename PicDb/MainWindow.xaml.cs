using log4net;
using log4net.Config;
using PicDb.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace PicDb
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }

        private void PicturesButton_Click(object sender, RoutedEventArgs e)
        {
            PicturesButton.IsChecked = true;
            UsersButton.IsChecked = false;
            PicturesViewControl.Visibility = Visibility.Visible;
            UsersViewControl.Visibility = Visibility.Collapsed;
        }

        private void UsersButton_Click(object sender, RoutedEventArgs e)
        {
            UsersButton.IsChecked = true;
            PicturesButton.IsChecked = false;
            PicturesViewControl.Visibility = Visibility.Collapsed;
            UsersViewControl.Visibility = Visibility.Visible;
        }
    }
}