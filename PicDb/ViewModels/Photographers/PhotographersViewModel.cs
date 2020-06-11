using System;
using System.Linq;
using log4net;
using PicDb.Business;
using PicDb.Data;
using PicDb.ViewModels.EventArguments;

namespace PicDb.ViewModels.Photographers
{
    public class PhotographersViewModel : ViewModelBase
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(DALSqlite));
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
            PhotographerInfoViewModel?.PhotographerChanged(args.Photographer);
        }

        private void OnPhotographerUpdated(object sender, EventArgs args)
        {
            PhotographerListViewModel?.UpdatePhotographersList();
        }
    }
}
