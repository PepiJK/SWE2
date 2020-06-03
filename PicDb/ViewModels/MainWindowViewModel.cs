using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

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
            // uses windows forms dialog (no other option in wpf)
            using(var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    string[] files = Directory.GetFiles(fbd.SelectedPath);

                    System.Windows.Forms.MessageBox.Show("Files found: " + files.Length.ToString(), "Message");
                }
            }
        }
    }
}
