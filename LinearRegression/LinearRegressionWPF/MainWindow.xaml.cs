using System;
using System.IO;
using System.Diagnostics;
using System.Windows;
using Microsoft.Win32;

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

        private void OpenDataFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
                Trace.WriteLine(File.ReadAllText(openFileDialog.FileName));
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
