using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using log4net;
using PicDb.Business;
using PicDb.Data;
using PicDb.Models;

namespace PicDb.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(DALSqlite));
        private readonly IDAL _dal = DALFactory.GetDAL();
        private readonly BL _bl = new BL();
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
                    string[] dirs = Directory.GetFiles(fbd.SelectedPath);
                    _bl.Save(dirs);
                }
            }
        }

        private void OnShowPhotographersView(object commandParameter)
        {
            _log.Info("show photographerview");
        }
    }
}