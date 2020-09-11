using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace GreenhouseUIClient.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(object sender, [CallerMemberName] string propName = "")
        {
            PropertyChanged?.Invoke(sender, new PropertyChangedEventArgs(propName));
        }
    }
}
