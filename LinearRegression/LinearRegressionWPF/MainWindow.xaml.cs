﻿using System;
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

    // Great for starting. Next step is to use MVVM pattern
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

        private void Train_Click(object sender, RoutedEventArgs e)
        {
            PlotModel model = new PlotModel { Title = "Function Graph" };
            model.Series.Add(new FunctionSeries((x) => x / 2, 0, 4, 0.01, "Line"));
            MainPlot.Model = model;
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