using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using log4net;
using PicDb.Business;
using PicDb.Data;
using PicDb.Models;
using PicDb.ViewModels.EventArguments;

namespace PicDb.ViewModels.Photographers
{
    public class PhotographerListViewModel : ViewModelBase
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(PhotographerListViewModel));
        private readonly BL _bl = new BL();
        private readonly DelegateCommand _addNewPhotographerCommand;
        private List<Photographer> _photographers;
        private Photographer _selectedPhotographer;
        
        public event EventHandler<PhotographerEventArgs> OnPhotographerChanged;
        public ICommand AddNewPhotographerCommand => _addNewPhotographerCommand;
        public List<Photographer> Photographers
        {
            get => _photographers;
            set =>  SetProperty(ref _photographers, value);
        }
        public Photographer SelectedPhotographer
        {
            get => _selectedPhotographer;
            set
            {
                SetProperty(ref _selectedPhotographer, value);
                OnPhotographerChanged?.Invoke(this, new PhotographerEventArgs{Photographer = value});
                Log.Info("Selected Photographer " + value?.Lastname);
            }
        }
        
        public PhotographerListViewModel()
        {
            _addNewPhotographerCommand = new DelegateCommand(OnAddNewPhotographer);
            UpdatePhotographersList();
        }
        
        public void UpdatePhotographersList()
        {
            Photographers = _bl.GetPhotographers().ToList();
        }
        
        private void OnAddNewPhotographer(object commandParameter)
        {
            var maxMuster = new Photographer
            {
                Firstname = "Max",
                Lastname = "Mustermann"
            };
            _bl.Save(maxMuster);
            UpdatePhotographersList();
            SelectedPhotographer = null;
        }
    }
}