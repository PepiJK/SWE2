using System.Collections.Generic;
using System.Linq;
using PicDb.Business;
using PicDb.Models;

namespace PicDb.ViewModels.Pictures
{
    public class PicturePhotographersViewModel : ViewModelBase
    {
        private readonly BL _bl = new BL();
        private Photographer _selectedPhotographer;
        private List<Photographer> _photographers;

        public List<Photographer> Photographers
        {
            get => _photographers;
            set => SetProperty(ref _photographers, value);
        }
        
        public Photographer SelectedPhotographer
        {
            get => _selectedPhotographer;
            set => SetProperty(ref _selectedPhotographer, value);
        }
        
        public PicturePhotographersViewModel()
        {
            Photographers = _bl.GetPhotographers().ToList();
        }

        public void OnPictureChanged(Picture picture)
        {
            SelectedPhotographer = picture.Photographer;
        }
    }
}