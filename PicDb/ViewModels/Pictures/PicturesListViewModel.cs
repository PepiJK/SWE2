using System.Collections.Generic;
using System.Linq;
using log4net;
using PicDb.Business;
using PicDb.Data;
using PicDb.Models;
namespace PicDb.ViewModels.Pictures

{
    class PicturesListViewModel : ViewModelBase
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(DALSqlite));
        private readonly BL _bl = new BL();
        
        private List<Picture> _pictures;
        private Picture _selectedPicture;
        
        public PicturesListViewModel()
        {
            _pictures = _bl.GetPictures().ToList();
        }
        
        public List<Picture> Pictures
        {
            get => _pictures;
            set =>  SetProperty(ref _pictures, value);
        }
    }
}
