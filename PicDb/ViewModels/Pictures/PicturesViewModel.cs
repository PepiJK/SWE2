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
        public PictureLargeViewModel PictureLargeViewModel { get; }
        public PicturePhotographersViewModel PicturePhotographersViewModel { get; }
        public ExifViewModel ExifViewModel { get; }
        public IptcViewModel IptcViewModel { get; }
        
        public PicturesViewModel(List<Picture> pictures = null)
        {
            PicturePhotographersViewModel = new PicturePhotographersViewModel();
            ExifViewModel = new ExifViewModel();
            PictureLargeViewModel = new PictureLargeViewModel();
            IptcViewModel = new IptcViewModel();
            PicturesListViewModel = new PicturesListViewModel(pictures);
            PicturesListViewModel.OnPictureChanged += OnPictureChanged;
        }

        private void OnPictureChanged(object sender, PictureEventArguments args)
        {
            PicturePhotographersViewModel?.OnPictureChanged(args.Picture);
            IptcViewModel?.OnPictureChanged(args.Picture);
            ExifViewModel?.OnPictureChanged(args.Picture);
            PictureLargeViewModel?.OnPictureChanged(args.Picture);
        }

        private void OnPhotographerChanged(object sender, PhotographerEventArgs args)
        {
            
        }
    }
}
