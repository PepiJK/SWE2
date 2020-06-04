using PicDb.ViewModels.EventArguments;

namespace PicDb.ViewModels.Photographers
{
    public class PhotographersViewModel : ViewModelBase
    {
        public PhotographerListViewModel PhotographerListViewModel { get; }
        public PhotographerInfoViewModel PhotographerInfoViewModel { get; }
        
        public PhotographersViewModel()
        {
            PhotographerListViewModel = new PhotographerListViewModel();
            PhotographerListViewModel.OnPhotographerChanged += OnPhotographerChanged;
            PhotographerInfoViewModel = new PhotographerInfoViewModel();
        }

        private void OnPhotographerChanged(object sender, PhotographerEventArgs args)
        {
            PhotographerInfoViewModel?.PhotographerChanged(args.Photographer);
        }
    }
}
