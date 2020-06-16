using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using log4net;
using PicDb.Business;
using PicDb.Data;
using PicDb.ViewModels.EventArguments;
using PicDb.ViewModels.Photographers;
using PicDb.ViewModels.Pictures;

namespace PicDb.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(MainWindowViewModel));
        private readonly BL _bl = new BL();
        private readonly DelegateCommand _openDirectoryCommand;
        private readonly DelegateCommand _showPicturesViewCommand;
        private readonly DelegateCommand _showPhotographersViewCommand;
        private bool _picturesToggleIsChecked;
        private bool _photographersToggleIsChecked;
        private PicturesViewModel _picturesViewModel;
        private PhotographersViewModel _photographersViewModel;
        
        public bool PicturesToggleIsChecked
        {
            get => _picturesToggleIsChecked;
            set => SetProperty(ref _picturesToggleIsChecked, value);
        }
        public bool PhotographersToggleIsChecked
        {
            get => _photographersToggleIsChecked;
            set => SetProperty(ref _photographersToggleIsChecked, value);
        }

        public PicturesViewModel PicturesViewModel
        {
            get => _picturesViewModel;
            set => SetProperty(ref _picturesViewModel, value);
        }

        public PhotographersViewModel PhotographersViewModel
        {
            get => _photographersViewModel;
            set => SetProperty(ref _photographersViewModel, value);
        }
        public ICommand OpenDirectoryCommand => _openDirectoryCommand;
        public ICommand ShowPicturesViewCommand => _showPicturesViewCommand;
        public ICommand ShowPhotographersViewCommand => _showPhotographersViewCommand;
        
        public MainWindowViewModel()
        {
            PicturesViewModel = new PicturesViewModel();
            PhotographersViewModel = new PhotographersViewModel();
            PicturesToggleIsChecked = true;
            PhotographersToggleIsChecked = false;
            _openDirectoryCommand = new DelegateCommand(OnOpenDirectory);
            _showPhotographersViewCommand = new DelegateCommand(OnShowPhotographersView);
            _showPicturesViewCommand = new DelegateCommand(OnShowPicturesView);
        }

        private void OnOpenDirectory(object commandParameter)
        {
            // uses windows forms dialog (no other option in wpf to open a folder only browser)
            using (var fbd = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = fbd.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    var dir = Directory.GetFiles(fbd.SelectedPath);
                    var pictures = _bl.SavePicturesFromDir(dir).ToList();
                    PicturesViewModel = new PicturesViewModel(pictures);
                }
            }
        }

        private void OnShowPhotographersView(object commandParameter)
        {
            PhotographersToggleIsChecked = true;
            PicturesToggleIsChecked = false;
        }
        
        private void OnShowPicturesView(object commandParameter)
        {
            PhotographersToggleIsChecked = false;
            PicturesToggleIsChecked = true;
        }
    }
}