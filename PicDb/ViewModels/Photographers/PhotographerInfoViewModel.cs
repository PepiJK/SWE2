using System;
using System.Windows.Input;
using PicDb.Business;
using PicDb.Models;

namespace PicDb.ViewModels.Photographers
{
    /// <summary>
    /// VM for the photographer info view.
    /// </summary>
    public class PhotographerInfoViewModel : ViewModelBase
    {
        private readonly BL _bl = new BL();
        private Photographer _selectedPhotographer;
        private string _firstname;
        private string _lastname;
        private DateTime? _birthdate;
        private string _notes;
        private readonly DelegateCommand _savePhotographerCommand;
        private bool _photographerSelected;
        private bool _photographerNotSelected = true;

        /// <summary>
        /// Command to update a photographer.
        /// </summary>
        public ICommand SavePhotographerCommand => _savePhotographerCommand;
        
        /// <summary>
        /// Event handler to tell the parent vm that a photographer has been updated.
        /// </summary>
        public event EventHandler OnPhotographerUpdated;
        
        /// <summary>
        /// The currently selected photographer.
        /// </summary>
        public Photographer SelectedPhotographer
        {
            get => _selectedPhotographer;
            set
            {
                SetProperty(ref _selectedPhotographer, value);
                _savePhotographerCommand.InvokeCanExecuteChanged();
            }
        }
        
        /// <summary>
        /// The firstname of the selected photographer.
        /// </summary>
        public string Firstname
        {
            get => _firstname;
            set
            {
                SetProperty(ref _firstname, value);
                _savePhotographerCommand. InvokeCanExecuteChanged();
            }
        }
        
        /// <summary>
        /// The lastname of the selected photographer.
        /// </summary>
        public string Lastname
        {
            get => _lastname;
            set
            {
                SetProperty(ref _lastname, value);
                _savePhotographerCommand. InvokeCanExecuteChanged();
            }
        }
        
        /// <summary>
        /// The birthdate of the selected photographer.
        /// </summary>
        public DateTime? Birthdate
        {
            get => _birthdate;
            set
            {
                SetProperty(ref _birthdate, value);
                _savePhotographerCommand. InvokeCanExecuteChanged();
            }
        }
        
        /// <summary>
        /// The notes of the selected photographer.
        /// </summary>
        public string Notes
        {
            get => _notes;
            set
            {
                SetProperty(ref _notes, value);
                _savePhotographerCommand. InvokeCanExecuteChanged();
            }
        }
        
        /// <summary>
        /// Bool that is used to represent if a photographer is selected.
        /// </summary>
        public bool PhotographerSelected
        {
            get => _photographerSelected;
            set => SetProperty(ref _photographerSelected, value);
        }
        
        /// <summary>
        /// Bool that is used to represent if a photographer is not selected.
        /// </summary>
        public bool PhotographerNotSelected
        {
            get => _photographerNotSelected;
            set => SetProperty(ref _photographerNotSelected, value);
        } 
        
        /// <summary>
        /// Constructor that initializes the save Photographer command which creates a new photographer.
        /// </summary>
        public PhotographerInfoViewModel()
        {
            _savePhotographerCommand = new DelegateCommand(OnSavePhotographer, CanSavePhotographer);
        }
        
        /// <summary>
        /// Set selected Photographer and its properties from Photographer that was selected in the list vm.
        /// </summary>
        /// <param name="photographer"></param>
        public void PhotographerChanged(Photographer photographer)
        {
            SelectedPhotographer = photographer;
            Firstname = photographer.Firstname;
            Lastname = photographer.Lastname;
            Birthdate = photographer.Birthdate;
            Notes = photographer.Notes;
            PhotographerSelected = true;
            PhotographerNotSelected = false;
        }
        
        private void OnSavePhotographer(object commandParameter)
        {
            _bl.UpdatePhotographer(new Photographer
            {
                Id = SelectedPhotographer.Id,
                Firstname = Firstname,
                Lastname = Lastname,
                Birthdate = Birthdate,
                Notes = Notes
            });
            OnPhotographerUpdated?.Invoke(this, new EventArgs());
            _savePhotographerCommand.InvokeCanExecuteChanged();
        }

        private bool CanSavePhotographer(object commandParameter)
        {
            if (Birthdate.HasValue && Birthdate > DateTime.Now) return false;
            if (string.IsNullOrWhiteSpace(Firstname)) return false;
            if (string.IsNullOrWhiteSpace(Lastname)) return false;
            return true;
        }
    }
}