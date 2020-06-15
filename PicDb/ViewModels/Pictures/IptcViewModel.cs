using Accessibility;
using PicDb.Business;
using PicDb.Models;
using PicDb.ViewModels.EventArguments;

namespace PicDb.ViewModels.Pictures
{
    public class IptcViewModel : ViewModelBase
    {
        private readonly BL _bl = new BL();
        private int? _id;
        private string _caption;
        private string _keywords;
        private string _credit;
        private string _copyright;

        public int? Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        public string Caption
        {
            get => _caption;
            set => SetProperty(ref _caption, value);
        }

        public string Keywords
        {
            get => _keywords;
            set => SetProperty(ref _keywords, value);
        }

        public string Credit
        {
            get => _credit;
            set => SetProperty(ref _credit, value);
        }

        public string Copyright
        {
            get => _copyright;
            set => SetProperty(ref _copyright, value);
        }

        public void OnPictureChanged(Picture picture)
        {
            if (picture.Iptc != null)
            {
                Id = picture.Iptc.Id;
                Caption = picture.Iptc.Caption;
                Keywords = picture.Iptc.Keywords;
                Credit = picture.Iptc.Credit;
                Copyright = picture.Iptc.Copyright;
            }
            else
            {
                Id = null;
                Caption = null;
                Keywords = null;
                Credit = null;
                Copyright = null;
            }
            
        }
    }
}
