using System;
using System.Windows;

namespace LinearRegressionWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary> 

    using OxyPlot;
    using OxyPlot.Series;

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }

    public class FunctionPlotModel
    {
        public PlotModel OxyModel { get; private set; }

        public FunctionPlotModel()
        {
            OxyModel = new PlotModel { Title = "Function Graph" };
            OxyModel.Series.Add(new FunctionSeries((x) => Math.Pow(x, 2), 0, 4, 0.01, "Parabola"));
        }
    }
}
