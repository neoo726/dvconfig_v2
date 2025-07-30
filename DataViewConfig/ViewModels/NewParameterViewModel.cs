using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using DataView_Configuration;

namespace DataViewConfig.ViewModels
{
    internal class NewParameterViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler  PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this,new PropertyChangedEventArgs(propertyName));
            }
        }
        private  RequestSystemEnum requestSystemName;
        public RequestSystemEnum RequestSystemName
        {
            get => requestSystemName;
            set { requestSystemName = value;OnPropertyChanged("RequestSystemName"); }
        }

        public Command OpenPageCommand { get; set; }
        public NewParameterViewModel()
        {
            OpenPageCommand = new Command(OpenPage);
        }
        private void OpenPage(object o)
        {
        }
    }
}
