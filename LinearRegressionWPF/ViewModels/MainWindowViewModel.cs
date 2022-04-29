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
    public class MainWindowViewModel 
    {
        ObservableCollection<object> _children;
        public MainWindowViewModel()
        {
            _children = new ObservableCollection<object>();
            _children.Add(new LinearRegressionViewModel());
            _children.Add(new ImageToVectorViewModel());

        }
        public ObservableCollection<object> Children { get { return _children; } }



      
    }
}
