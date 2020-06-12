using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PicDb.ViewModels
{
    /// <summary>
    /// View model base that implements INotifyPropertyChanged.
    /// source: https://intellitect.com/getting-started-model-view-viewmodel-mvvm-pattern-using-windows-presentation-framework-wpf/
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Property changed event handler to update a property.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
 
        protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName]string propertyName = null)
        {
            if(!EqualityComparer<T>.Default.Equals(field, newValue))
            {
                field = newValue;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                return true;
            }
            return false;
        }
    }
}