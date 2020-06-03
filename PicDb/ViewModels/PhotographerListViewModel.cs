using System.Collections.Generic;
using PicDb.Business;
using PicDb.Models;

namespace PicDb.ViewModels
{
    public class PhotographerListViewModel
    {
        private readonly BL _bl = new BL();
        private IEnumerable<Photographer> _photographers;

        public IEnumerable<Photographer> Photographers
        {
            get => _photographers;
            set => _photographers = value;
        }

        public PhotographerListViewModel()
        {
            _photographers = _bl.GetPhotographers();
        }


    }
}