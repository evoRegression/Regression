
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
