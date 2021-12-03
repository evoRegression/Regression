﻿using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml;

namespace MLModel
{

    public class MLModel : IMLModel
    {

        private Coefficients _coefficient;
        private Double LearningRate;

        public MLModel(double Slope, double Intercept, double LearningRate)
        {
            _coefficient = new Coefficients(Slope, Intercept);
            this.LearningRate = LearningRate;
        }

        public MLModel()
        {
            _coefficient = new Coefficients(0, 0);
        }

        public double Evaluation()
        {
            return 0;
        }

        public double Predict(double dataPoint)
        {
            return _coefficient.Slope * dataPoint + _coefficient.Intercept;
        }

        public void Train(double[][] data)
        {

            double[] xAxis = data.Select(array => (double)array.GetValue(0)).ToArray();
            double[] yAxis = data.Select(array => (double)array.GetValue(1)).ToArray();

            GradientDescent(xAxis , yAxis);
        }

        internal Coefficients SimpleOrdinaryLeastSquare(double[] xAxis, double[] yAxis)
        {
            int N = yAxis.Length;
            double sumX = xAxis.Sum();
            double sumY = yAxis.Sum();

            double numenator = N * xAxis.Zip(yAxis, (x, y) => x * y).Sum() - sumX * sumY;
            double denominator = N * xAxis.Select(x => x * x).Sum() - sumX * sumX;

            _coefficient.Slope = numenator / denominator;
            _coefficient.Intercept = sumY / N - _coefficient.Slope * sumX / N;
            return _coefficient;
        }

        // https://en.wikipedia.org/wiki/Simple_linear_regression
        internal Coefficients QuadraticOrdinaryLeastSquare(double[] xAxis, double[] yAxis)
        {
            int N = yAxis.Length;
            double sumX = xAxis.Sum();
            double sumY = yAxis.Sum();
            double avgX = sumX / N;
            double avgY = sumY / N;

            double numenator = xAxis.Zip(yAxis, (x, y) => (x - avgX) * (y - avgY)).Sum();
            double denominator = xAxis.Select(x => (x - avgX) * (x - avgX)).Sum();
            _coefficient.Slope = numenator / denominator;
            _coefficient.Intercept = avgY - _coefficient.Slope * avgX;
           
            return _coefficient;
        }

        public void Export(string path)
        {
            using (FileStream fs = new(path, FileMode.Create))
            {
                using (XmlDictionaryWriter writer = XmlDictionaryWriter.CreateTextWriter(fs))
                {
                    DataContractSerializer dcs = new DataContractSerializer(typeof(Coefficients));
                    dcs.WriteObject(writer, _coefficient);
                }
            }
        }

        public void Import(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(fs, new XmlDictionaryReaderQuotas());
                DataContractSerializer dcs = new DataContractSerializer(typeof(Coefficients));

                _coefficient = (Coefficients)dcs.ReadObject(reader);
            }
        }

        public double LeastAbsoluteError(double[] xAxis, double[] yAxis)
        {
            if (xAxis == null || xAxis.Length == 0 || yAxis == null || yAxis.Length == 0)
                return 0;
            return xAxis.Zip(yAxis, (x, y) => Math.Abs(y - Predict(x))).Sum() / xAxis.Length;
        }

        public double LeastSquareError(double[] xAxis, double[] yAxis)
        {
            if (xAxis == null || xAxis.Length == 0 || yAxis == null || yAxis.Length == 0)
                return 0;
            return xAxis.Zip(yAxis, (x, y) => Math.Pow(y - Predict(x), 2)).Sum() / xAxis.Length ;
        }

        internal void GradientDescent(double[] xAxis, double[] yAxis)
        {
            Coefficients prev = new Coefficients(0,0);
            double dSlope, dIntercept;
            double delta = 0.0000000000001;
            int maxIterations = 1000000;
            int i = 0;
            do
            {
                dSlope = SlopeDerivate(xAxis, yAxis);
                dIntercept = InterceptDerivate(xAxis, yAxis);
                _coefficient.Slope -= LearningRate * dSlope;
                _coefficient.Intercept -= LearningRate * dIntercept;
                i++;
            } while (i < maxIterations && (Math.Abs(dSlope) >= delta && Math.Abs(dIntercept) >= delta));
        }

        internal double SlopeDerivate(double[] xAxis, double[] yAxis)
        {
            if (xAxis == null || xAxis.Length == 0 || yAxis == null || yAxis.Length == 0)
                return 0;
            return xAxis.Zip(yAxis, (x, y) => (_coefficient.Slope * x + _coefficient.Intercept - y) * x).Sum() /  xAxis.Length;
        }

        internal double InterceptDerivate(double[] xAxis, double[] yAxis)
        {
            if (xAxis == null || xAxis.Length == 0 || yAxis == null || yAxis.Length == 0)
                return 0;
            return xAxis.Zip(yAxis, (x, y) => _coefficient.Slope * x + _coefficient.Intercept - y ).Sum() / xAxis.Length;
        }
    }
}
