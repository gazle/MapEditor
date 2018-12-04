using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MapEditor.ViewModels
{
    class ViewModelBase : INotifyPropertyChanged
    {
        //public bool IsChanged => throw new System.NotImplementedException();

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            field = value; OnPropertyChanged(propertyName);
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
