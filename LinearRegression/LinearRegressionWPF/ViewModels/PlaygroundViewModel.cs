using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LinearRegressionWPF.ViewModels
{
    internal class PlaygroundViewModel : INotifyPropertyChanged
    {
        private double mySliderValue;

        public double SliderValue
        {
            get { return mySliderValue; }
            set
            {
                mySliderValue = value;
                NotifyPropertyChanged(nameof(SliderValue));
            }
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
