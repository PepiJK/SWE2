using System;
using System.Windows.Input;
using PicDb.Business;
using PicDb.Models;

namespace PicDb.ViewModels.Pictures
{
    public class PictureLargeViewModel : ViewModelBase
    {
        private readonly BL _bl = new BL();
        private string _filename;
        private string _directory;
        private string _fullPath;
        
        public string Filename
        {
            get => _filename;
            set => SetProperty(ref _filename, value);
        }

        public string Directory
        {
            get => _directory;
            set => SetProperty(ref _directory, value);
        }
        
        //public string FullPath => _fullPath;
        public string FullPath
        {
            get => _fullPath;
            set => SetProperty(ref _fullPath, value);
        }

        public void OnPictureChanged(Picture picture)
        {
            Filename = picture.Filename;
            Directory = picture.Directory;
            FullPath = picture.FullPath;
        }
        
    }
}
