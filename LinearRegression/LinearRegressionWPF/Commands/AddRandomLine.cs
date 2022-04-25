using System;
using System.Windows.Input;

using LinearRegressionWPF.ViewModels;

namespace LinearRegressionWPF.Commands
{
    class AddRandomLine : ICommand
    {
        private MainWindowViewModel _viewModel;

        public AddRandomLine(MainWindowViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Random random = new Random();

            // Generate slope

            const double SLOPE_MIN = -15;
            const double SLOPE_MAX = 15;
            const double SLOPE_RANGE = SLOPE_MAX - SLOPE_MIN;
            double slope = SLOPE_MIN + random.NextDouble() * SLOPE_RANGE;

            // Calculate legal intercept values

            double xMin = _viewModel.RegressionPlot.XMin;
            double xRange = _viewModel.RegressionPlot.XRange;
            double yMin = _viewModel.RegressionPlot.YMin;
            double yMax = _viewModel.RegressionPlot.YMax;
            double yRange = _viewModel.RegressionPlot.YRange;

            double rise = slope * xRange;
            double intc_min = Math.Min(yMin - rise, yMin) - slope * xMin;
            double intc_max = Math.Max(yMax - rise, yMax) - slope * xMin;

            // Line padding

            const double LINE_PADDING = 0.1;

            intc_min += yRange * LINE_PADDING;
            intc_max -= yRange * LINE_PADDING;
            double intc_range = intc_max - intc_min;

            // Generate intercept

            double yIntercept = intc_min + random.NextDouble() * intc_range;

            // Round

            int DECIMAL_PLACES = 2;
            MidpointRounding MID_ROUND = MidpointRounding.ToEven;

            slope = Math.Round(slope, DECIMAL_PLACES, MID_ROUND);
            yIntercept = Math.Round(yIntercept, DECIMAL_PLACES, MID_ROUND);

            // Update graph

            _viewModel.UpdateRegressionLine(slope, yIntercept);
        }
    }
}
