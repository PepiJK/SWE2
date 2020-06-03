using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;

namespace PicDb.ViewModels
{
    class MainWindowViewModel
    {
        private readonly DelegateCommand _openDirectoryCommand;
        public ICommand OpenDirectoryCommand => _openDirectoryCommand;
 
        public MainWindowViewModel()
        {
            _openDirectoryCommand = new DelegateCommand(OnOpenDirectory);
        }
 
        private void OnOpenDirectory(object commandParameter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
        }
    }
}
