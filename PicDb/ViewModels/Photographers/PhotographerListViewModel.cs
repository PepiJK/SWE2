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
    /// <summary>
    /// VM for the photographer list view.
    /// </summary>
    public class PhotographerListViewModel : ViewModelBase
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(DALSqlite));
        private readonly BL _bl = new BL();
        private readonly DelegateCommand _addNewPhotographerCommand;
        private List<Photographer> _photographers;
        private Photographer _selectedPhotographer;
        
        /// <summary>
        /// Event handler to tell the parent vm that a photographer has been selected from the list.
        /// The PhotographerEventArgs holds the selected photographer.
        /// </summary>
        public event EventHandler<PhotographerEventArgs> OnPhotographerChanged;
        
        /// <summary>
        /// Command to add a new photographer.
        /// </summary>
        public ICommand AddNewPhotographerCommand => _addNewPhotographerCommand;
        
        /// <summary>
        /// List of all available photographers.
        /// </summary>
        public List<Photographer> Photographers
        {
            get => _photographers;
            set =>  SetProperty(ref _photographers, value);
        }
        
        /// <summary>
        /// The photographer that is selected from the list of photographers.
        /// When a new photographer gets selected the parent is notified with the defined event handler.
        /// </summary>
        public Photographer SelectedPhotographer
        {
            get => _selectedPhotographer;
            set
            {
                SetProperty(ref _selectedPhotographer, value);
                OnPhotographerChanged?.Invoke(this, new PhotographerEventArgs{Photographer = value});
                Log.Info("Selected Photographer " + value.Lastname);
            }
        }
        
        /// <summary>
        /// Constructor that initializes the add new photographer command and updates the photographer list with
        /// photographers from the database.
        /// </summary>
        public PhotographerListViewModel()
        {
            _addNewPhotographerCommand = new DelegateCommand(OnAddNewPhotographer);
            UpdatePhotographersList();
        }
        
        /// <summary>
        /// Get the all photographers from the database and populate the photographer list.
        /// </summary>
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
            SelectedPhotographer = maxMuster;
        }
    }
}