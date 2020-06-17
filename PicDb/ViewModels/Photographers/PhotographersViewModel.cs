using System;
using PicDb.ViewModels.EventArguments;

namespace PicDb.ViewModels.Photographers
{
    public class PhotographersViewModel : ViewModelBase
    {
        public event EventHandler PhotographersUpdated;
        public PhotographerListViewModel PhotographerListViewModel { get;}
        public PhotographerInfoViewModel PhotographerInfoViewModel { get;}
        
        public PhotographersViewModel()
        {
            PhotographerListViewModel = new PhotographerListViewModel();
            PhotographerListViewModel.OnPhotographerChanged += OnPhotographerChanged;
            PhotographerInfoViewModel = new PhotographerInfoViewModel();
            PhotographerInfoViewModel.OnPhotographerUpdated += OnPhotographerUpdated;
        }

        private void OnPhotographerChanged(object sender, PhotographerEventArgs args)
        {
            PhotographersUpdated?.Invoke(this, new EventArgs());
            PhotographerInfoViewModel?.PhotographerChanged(args.Photographer);
        }
        
        private void OnPhotographerUpdated(object sender, EventArgs args)
        {
            PhotographersUpdated?.Invoke(this, new EventArgs());
            PhotographerListViewModel?.UpdatePhotographersList();
        }
    }
}
