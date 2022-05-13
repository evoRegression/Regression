
using LinearRegressionWPF.BackendDescriptors;
using LinearRegressionWPF.Commands;
using LinearRegressionWPF.Models;
using LinearRegressionBackend.DataProvider;
using LinearRegressionBackend.MLCommmons;
using LinearRegressionBackend.MLModel;

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
