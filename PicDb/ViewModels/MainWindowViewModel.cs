using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Input;
using log4net;
using PicDb.Data;

namespace PicDb.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(DALSqlite));
        private readonly DelegateCommand _openDirectoryCommand;
        private readonly DelegateCommand _showPhotographersViewCommand;
        
        public ICommand OpenDirectoryCommand => _openDirectoryCommand;
        public ICommand ShowPhotographersViewCommand => _showPhotographersViewCommand;

        public MainWindowViewModel()
        {
            _openDirectoryCommand = new DelegateCommand(OnOpenDirectory);
            _showPhotographersViewCommand = new DelegateCommand(OnShowPhotographersView);
        }

        private void OnOpenDirectory(object commandParameter)
        {
            // uses windows forms dialog (no other option in wpf)
            using (var fbd = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = fbd.ShowDialog();

                if (result ==  System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    string[] files = Directory.GetFiles(fbd.SelectedPath);
                    System.Windows.Forms.MessageBox.Show("Files found: " + files.Length.ToString(), "Message");
                }
            }
        }

        private void OnShowPhotographersView(object commandParameter)
        {
            _log.Info("show photographerview");
        }
    }
}