using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;

using LinearRegressionBackend.DataProvider;
using LinearRegressionBackend.MLModel;
using LinearRegressionBackend.MLCommmons;

using LinearRegressionWPF.BackendDescriptors;
using LinearRegressionWPF.Models;
using LinearRegressionWPF.Commands;
using System.Collections.ObjectModel;

namespace LinearRegressionWPF.ViewModels
{
    public class MainWindowViewModel :  INotifyPropertyChanged
    {
        
        public MainWindowViewModel()
        {
          
        }
       



        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
