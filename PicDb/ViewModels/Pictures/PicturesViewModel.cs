using PicDb.ViewModels.EventArguments;
using PicDb.ViewModels.Photographers;

namespace PicDb.ViewModels.Pictures
{
    public class PicturesViewModel : ViewModelBase
    {
        public PicturesListViewModel PicturesListViewModel { get; }
        
        public PicturesViewModel()
        {
            PicturesListViewModel = new PicturesListViewModel();
            PicturesListViewModel.OnPictureChanged += OnPictureChanged;
        }
        
        private void OnPictureChanged(object sender, PictureEventArguments args)
        {
        }
    }
}
