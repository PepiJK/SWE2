using System;
using System.Windows.Input;
using PicDb.Business;
using PicDb.Models;

namespace PicDb.ViewModels.Pictures
{
    public class ExifViewModel : ViewModelBase
    {
        private readonly BL _bl = new BL();
        private Picture _selectedPicture;
        private string _model;
        private string _manufacturer;
        private int? _focalLength;
        private DateTime? _dateTimeOriginal;
        private readonly DelegateCommand _saveExifCommand;
        
        public string Manufacturer
        {
            get => _manufacturer;
            set => SetProperty(ref _manufacturer, value);
        }
        public string Model
        {
            get => _model;
            set => SetProperty(ref _model, value);
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
                Manufacturer = picture.Exif.Manufacturer;
                Model = picture.Exif.Model;
                FocalLength = picture.Exif.FocalLength;
                DateTimeOriginal = picture.Exif.DateTimeOriginal;
            }
            else
            {
                Manufacturer = null;
                Model = null;
                FocalLength = null;
                DateTimeOriginal = null;
            }
        }

        private void OnSaveExif(object commandParameter)
        {
            _selectedPicture.Exif.Manufacturer = Manufacturer;
            _selectedPicture.Exif.Model = Model;
            _selectedPicture.Exif.FocalLength = FocalLength;
            _selectedPicture.Exif.DateTimeOriginal = DateTimeOriginal;
            _bl.Update(_selectedPicture);
        }
    }
}
