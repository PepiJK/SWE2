using System;
using System.Windows.Input;
using log4net;
using PicDb.Business;
using PicDb.Models;

namespace PicDb.ViewModels.Pictures
{
    public class ExifViewModel : ViewModelBase
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(ExifViewModel));
        private BL _bl = new BL();
        private Picture _selectedPicture;
        private string _model;
        private string _lens;
        private int? _focalLength;
        private DateTime? _dateTimeOriginal;
        private readonly DelegateCommand _saveExifCommand;
        
        public string Model
        {
            get => _model;
            set => SetProperty(ref _model, value);
        }

        public string Lens
        {
            get => _lens;
            set => SetProperty(ref _lens, value);
        }

        public int? FocalLength
        {
            get => _focalLength;
            set => SetProperty(ref _focalLength, value);
        }

        public DateTime? DateTimeOriginal
        {
            get => _dateTimeOriginal;
            set => SetProperty(ref _dateTimeOriginal, value);
        }
        
        public ICommand SaveExifCommand => _saveExifCommand;

        public ExifViewModel()
        {
            _saveExifCommand = new DelegateCommand(OnSaveExif);
        }

        public void OnPictureChanged(Picture picture)
        {
            _selectedPicture = picture;
            
            if (picture?.Exif != null)
            {
                Model = picture.Exif.Model;
                Lens = picture.Exif.Lens;
                FocalLength = picture.Exif.FocalLength;
                DateTimeOriginal = picture.Exif.DateTimeOriginal;
            }
            else
            {
                Model = null;
                Lens = null;
                FocalLength = null;
                DateTimeOriginal = null;
            }
        }

        private void OnSaveExif(object commandParameter)
        {
            if(_selectedPicture == null) throw new NullReferenceException();
            _selectedPicture.Exif.Lens = Lens;
            _selectedPicture.Exif.Model = Model;
            _selectedPicture.Exif.FocalLength = FocalLength;
            _selectedPicture.Exif.DateTimeOriginal = DateTimeOriginal;
            _bl.Update(_selectedPicture);
        }


    }
}
