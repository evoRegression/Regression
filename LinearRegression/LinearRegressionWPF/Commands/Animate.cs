using System;
using System.Threading.Tasks;
using System.Windows.Input;

using LinearRegressionWPF.ViewModels;

namespace LinearRegressionWPF.Commands
{
    class Animate : ICommand 
    {
        private MainWindowViewModel _viewModel;
        private const int ANIMATION_DELAY = 250;

        public Animate(MainWindowViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async Task ExecuteAsync()
        {
            while (_viewModel.StepEnabled) {
                _viewModel.step();
                await Task.Delay(ANIMATION_DELAY);
            }
        }

        public async void Execute(object parameter)
        {
            await ExecuteAsync();
        }
    }
}
