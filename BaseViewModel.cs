using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfHostingSkeleton
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        private int _busyCount;

        public int BusyCount
        {
            get => _busyCount;
            set
            {
                _busyCount = value;
                OnPropertyChanged(nameof(BusyCount));
                OnPropertyChanged(nameof(IsBusy));
                OnPropertyChanged(nameof(IsNotBusy));
            }
        }

        public bool IsBusy => BusyCount > 0;
        public bool IsNotBusy => !IsBusy;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}