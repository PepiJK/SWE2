using System;
using log4net;
using PicDb.Data;
using PicDb.ViewModels.EventArguments;

namespace PicDb.ViewModels.Photographers
{
    /// <summary>
    /// Parent VM for photographer list and info.
    /// </summary>
    public class PhotographersViewModel : ViewModelBase
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(PhotographersViewModel));
        
        /// <summary>
        /// VM for the photographer list.
        /// </summary>
        public PhotographerListViewModel PhotographerListViewModel { get;}
        
        /// <summary>
        /// VM for the photographer info.
        /// </summary>
        public PhotographerInfoViewModel PhotographerInfoViewModel { get;}
        
        /// <summary>
        /// Constructor that initializes the photographer info vm, list vm and their event handlers.
        /// </summary>
        public PhotographersViewModel()
        {
            PhotographerListViewModel = new PhotographerListViewModel();
            PhotographerListViewModel.OnPhotographerChanged += OnPhotographerChanged;
            PhotographerInfoViewModel = new PhotographerInfoViewModel();
            PhotographerInfoViewModel.OnPhotographerUpdated += OnPhotographerUpdated;
        }

        private void OnPhotographerChanged(object sender, PhotographerEventArgs args)
        {
            PhotographerInfoViewModel?.PhotographerChanged(args.Photographer);
        }

        private void OnPhotographerUpdated(object sender, EventArgs args)
        {
            PhotographerListViewModel?.UpdatePhotographersList();
        }
    }
}
