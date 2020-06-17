using System;
using System.Windows.Input;
using PicDb.Business;
using PicDb.Models;

namespace PicDb.ViewModels.Photographers
{
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
        
        public ICommand SavePhotographerCommand => _savePhotographerCommand;
        public event EventHandler OnPhotographerUpdated;
        public Photographer SelectedPhotographer
        {
            get => _selectedPhotographer;
            set
            {
                SetProperty(ref _selectedPhotographer, value);
                _savePhotographerCommand.InvokeCanExecuteChanged();
            }
        }
        public string Firstname
        {
            get => _firstname;
            set
            {
                SetProperty(ref _firstname, value);
                _savePhotographerCommand.InvokeCanExecuteChanged();
            }
        }
        public string Lastname
        {
            get => _lastname;
            set
            {
                SetProperty(ref _lastname, value);
                _savePhotographerCommand.InvokeCanExecuteChanged();
            }
        }
        public DateTime? Birthdate
        {
            get => _birthdate;
            set
            {
                SetProperty(ref _birthdate, value);
                _savePhotographerCommand.InvokeCanExecuteChanged();
            }
        }
        public string Notes
        {
            get => _notes;
            set
            {
                SetProperty(ref _notes, value);
                _savePhotographerCommand.InvokeCanExecuteChanged();
            }
        }
        public bool PhotographerSelected
        {
            get => _photographerSelected;
            set => SetProperty(ref _photographerSelected, value);
        }
        public bool PhotographerNotSelected
        {
            get => _photographerNotSelected;
            set => SetProperty(ref _photographerNotSelected, value);
        } 
      
        public PhotographerInfoViewModel()
        {
            _savePhotographerCommand = new DelegateCommand(OnSavePhotographer, CanSavePhotographer);
        }
        
        public void PhotographerChanged(Photographer photographer)
        {
            if (photographer != null)
            {
                SelectedPhotographer = photographer;
                Firstname = photographer.Firstname;
                Lastname = photographer.Lastname;
                Birthdate = photographer.Birthdate;
                Notes = photographer.Notes;
                PhotographerSelected = true;
                PhotographerNotSelected = false;
            }
            else
            {
                PhotographerSelected = false;
                PhotographerNotSelected = true;
            }
        }
        
        private void OnSavePhotographer(object commandParameter)
        {
            var newPhotographer = new Photographer
            {
                Id = SelectedPhotographer.Id,
                Firstname = Firstname,
                Lastname = Lastname,
                Birthdate = Birthdate,
                Notes = Notes
            };
            _bl.Update(newPhotographer);
            OnPhotographerUpdated?.Invoke(this, new EventArgs());
            SelectedPhotographer = newPhotographer;
            PhotographerChanged(newPhotographer);
            _savePhotographerCommand.InvokeCanExecuteChanged();
        }

        private bool CanSavePhotographer(object commandParameter)
        {
            if (SelectedPhotographer == null) return false;
            if (Birthdate.HasValue && Birthdate > DateTime.Now) return false;
            if (string.IsNullOrWhiteSpace(Firstname)) return false;
            if (string.IsNullOrWhiteSpace(Lastname)) return false;
            return true;
        }
    }
}