using System;
using System.Threading.Tasks;
using System.Windows.Input;

using LinearRegressionWPF.ViewModels;

namespace LinearRegressionWPF.Commands
{
    class Animate : ICommand 
    {
        private LinearRegressionViewModel _viewModel;
        private const int ANIMATION_DELAY = 250;

        public Animate(LinearRegressionViewModel viewModel)
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
                _viewModel.Step();
                await Task.Delay(ANIMATION_DELAY);
            }
        }

        public async void Execute(object parameter)
        {
            await ExecuteAsync();
        }
    }
}
