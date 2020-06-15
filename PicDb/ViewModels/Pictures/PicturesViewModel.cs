using System.Collections;
using System.Collections.Generic;
using System.Windows.Documents;
using PicDb.Models;
using PicDb.ViewModels.EventArguments;
using PicDb.ViewModels.Photographers;

namespace PicDb.ViewModels.Pictures
{
    public class PicturesViewModel : ViewModelBase
    {
        public PicturesListViewModel PicturesListViewModel { get; }
        public ExifViewModel ExifViewModel { get; }
        
        public PicturesViewModel(List<Picture> pictures = null)
        {
            ExifViewModel = new ExifViewModel();
            PicturesListViewModel = new PicturesListViewModel(pictures);
            PicturesListViewModel.OnPictureChanged += OnPictureChanged;
        }
        
        private void OnPictureChanged(object sender, PictureEventArguments args)
        {
            ExifViewModel?.OnPictureChanged(args.Picture);
        }
    }
}
