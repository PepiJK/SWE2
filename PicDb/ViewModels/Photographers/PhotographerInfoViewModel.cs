using PicDb.Business;
using PicDb.Models;

namespace PicDb.ViewModels.Photographers
{
    public class PhotographerInfoViewModel : ViewModelBase
    {
        private readonly BL _bl = new BL();
        private Photographer _selectedPhotographer;

        public Photographer SelectedPhotographer
        {
            get => _selectedPhotographer;
            set => SetProperty(ref _selectedPhotographer, value);
        }

        public void PhotographerChanged(Photographer photographer)
        {
            SelectedPhotographer = photographer;
        }
    }
}