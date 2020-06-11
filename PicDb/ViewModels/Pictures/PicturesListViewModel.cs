﻿using System;
using System.Collections.Generic;
using System.Linq;
using log4net;
using PicDb.Business;
using PicDb.Data;
using PicDb.Models;
using PicDb.ViewModels.EventArguments;

namespace PicDb.ViewModels.Pictures
{
    public class PicturesListViewModel : ViewModelBase
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(DALSqlite));
        private readonly BL _bl = new BL();
        
        private List<Picture> _pictures;
        private Picture _selectedPicture;
        
        public event EventHandler<PictureEventArguments> OnPictureChanged;
        
        public PicturesListViewModel()
        {
            _pictures = _bl.GetPictures().ToList();
        }
        
        public List<Picture> Pictures
        {
            get => _pictures;
            set =>  SetProperty(ref _pictures, value);
        }
        
        public Picture SelectedPicture
        {
            get => _selectedPicture;
            set
            {
                SetProperty(ref _selectedPicture, value);
                OnPictureChanged?.Invoke(this, new PictureEventArguments(){Picture = value});
                Log.Info("Selected Picture " + value.Filename);
            }
        }
    }
}