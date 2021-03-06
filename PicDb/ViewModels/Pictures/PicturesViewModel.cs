﻿using System.Collections;
using System.Collections.Generic;
using System.Windows.Documents;
using PicDb.Models;
using PicDb.ViewModels.EventArguments;
using PicDb.ViewModels.Photographers;

namespace PicDb.ViewModels.Pictures
{
    public class PicturesViewModel : ViewModelBase
    {
        private PicturePhotographersViewModel _picturePhotographersViewModel;
        private bool _pictureIsSelected;
        
        public PicturesListViewModel PicturesListViewModel { get; }
        public PictureLargeViewModel PictureLargeViewModel { get; }
        public PicturePhotographersViewModel PicturePhotographersViewModel
        {
            get => _picturePhotographersViewModel;
            set => SetProperty(ref _picturePhotographersViewModel, value);
        }
        public ExifViewModel ExifViewModel { get; }
        public IptcViewModel IptcViewModel { get; }
        public bool PictureIsSelected
        {
            get => _pictureIsSelected;
            set => SetProperty(ref _pictureIsSelected, value);
        }

        public PicturesViewModel(List<Picture> pictures = null)
        {
            PicturePhotographersViewModel = new PicturePhotographersViewModel();
            ExifViewModel = new ExifViewModel();
            IptcViewModel = new IptcViewModel();
            PictureLargeViewModel = new PictureLargeViewModel();
            PicturesListViewModel = new PicturesListViewModel(pictures);
            PicturesListViewModel.OnPictureChanged += OnPictureChanged;
        }

        private void OnPictureChanged(object sender, PictureEventArguments args)
        {
            PictureIsSelected = true;
            PicturePhotographersViewModel?.OnPictureChanged(args.Picture);
            IptcViewModel?.OnPictureChanged(args.Picture);
            ExifViewModel?.OnPictureChanged(args.Picture);
            PictureLargeViewModel?.OnPictureChanged(args.Picture);
        }

        public void UpdatePhotographers()
        {
            PicturePhotographersViewModel.UpdatePhotographers();
        }
    }
}
