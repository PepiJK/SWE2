using System.Collections.Generic;
using System.Linq;
using PicDb.Business;
using PicDb.Models;

namespace PicDb.ViewModels.Pictures
{
    public class PicturePhotographersViewModel : ViewModelBase
    {
        private readonly BL _bl = new BL();
        private Picture _selectedPicture;
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
            set
            {
                if (_selectedPicture != null && value != null && _selectedPhotographer != value)
                {
                    _selectedPicture.PhotographerId = value.Id;
                    _selectedPicture.Photographer = value;
                    _bl.Update(_selectedPicture);
                }
                SetProperty(ref _selectedPhotographer, value);
            }
        }

        public PicturePhotographersViewModel()
        {
            Photographers = _bl.GetPhotographers().ToList();
        }

        public void OnPictureChanged(Picture picture)
        {
            _selectedPicture = picture;
            SelectedPhotographer = Photographers.FirstOrDefault(pho => pho.Id == picture.PhotographerId);
        }

        public void UpdatePhotographers()
        {
            Photographers = _bl.GetPhotographers().ToList();
            SelectedPhotographer = Photographers?.FirstOrDefault(pho => pho.Id == _selectedPicture?.Photographer?.Id);
        }
        
    }
}