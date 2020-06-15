using System.Windows.Input;
using Accessibility;
using PicDb.Business;
using PicDb.Models;
using PicDb.ViewModels.EventArguments;

namespace PicDb.ViewModels.Pictures
{
    public class IptcViewModel : ViewModelBase
    {
        private readonly BL _bl = new BL();
        private string _caption;
        private string _keywords;
        private string _credit;
        private string _copyright;
        private Picture _picture;
        private readonly DelegateCommand _saveIptcCommand;

        public ICommand SaveIptcCommand => _saveIptcCommand;

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

        public IptcViewModel()
        {
            _saveIptcCommand = new DelegateCommand(OnSaveIptc);    
        }

        private void OnSaveIptc(object commandParameter)
        {
            _picture.Iptc.Caption = Caption;
            _picture.Iptc.Credit = Credit;
            _picture.Iptc.Keywords = Keywords;
            _picture.Iptc.Copyright = Copyright;
            _bl.Update(_picture);
        }
        
        public void OnPictureChanged(Picture picture)
        {
            _picture = picture;
            if (picture.Iptc != null)
            {
                Caption = picture.Iptc.Caption;
                Keywords = picture.Iptc.Keywords;
                Credit = picture.Iptc.Credit;
                Copyright = picture.Iptc.Copyright;
            }
            else
            {
                Caption = null;
                Keywords = null;
                Credit = null;
                Copyright = null;
            }
            
        }
    }
}
