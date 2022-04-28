using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

using LinearRegressionBackend.MLNeuralNetwork;

using LinearRegressionWPF.Commands;

namespace LinearRegressionWPF.ViewModels
{
    internal class NeuronPlaygroundViewModel : INotifyPropertyChanged
    {
        public NeuronPlaygroundViewModel()
        {
            InitWeights();
            InitActivation();
            InitCommands();
        }

        public void InitCommands()
        {
            // pass
        }

        #region Weights

        public void InitWeights()
        {
            // pass
        }

        #endregion

        #region Activation

        public void InitActivation()
        {
            // pass
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
