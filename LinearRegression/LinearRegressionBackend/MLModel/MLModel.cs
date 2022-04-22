using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml;

namespace LinearRegressionBackend.MLModel
{

    public class MLModel : IMLModel
    {
        public Coefficients _coefficient;

        public MLModel(double Slope, double Intercept)
        {
            _coefficient = new Coefficients(Slope, Intercept);
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

            _coefficient = QuadraticOrdinaryLeastSquare(xAxis, yAxis);
        }

        public Coefficients SimpleOrdinaryLeastSquare(double[] xAxis, double[] yAxis)
        {
            int N = yAxis.Length;
            double sumX = xAxis.Sum();
            double sumY = yAxis.Sum();
            double avgX = sumX / N;
            double avgY = sumY / N;

            double numenator = N * xAxis.Zip(yAxis, (x, y) => x * y).Sum() - sumX * sumY;
            double denominator = N * xAxis.Select(x => x * x).Sum() - sumX * sumX;

            double slope = numenator / denominator;
            double intercept = avgY - slope * avgX;

            return new Coefficients(slope, intercept);
        }

        // https://en.wikipedia.org/wiki/Simple_linear_regression
        public Coefficients QuadraticOrdinaryLeastSquare(double[] xAxis, double[] yAxis)
        {
            int N = yAxis.Length;
            double avgX = xAxis.Average();
            double avgY = yAxis.Average();

            double numenator = xAxis.Zip(yAxis, (x, y) => (x - avgX) * (y - avgY)).Sum();
            double denominator = xAxis.Select(x => (x - avgX) * (x - avgX)).Sum();

            double slope = numenator / denominator;
            double intercept = avgY - slope * avgX;

            return new Coefficients(slope, intercept);
        }

        public void Save(string path)
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

        public static IMLModel Load(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(fs, new XmlDictionaryReaderQuotas());
                DataContractSerializer dcs = new DataContractSerializer(typeof(Coefficients));

                Coefficients coefficient = (Coefficients)dcs.ReadObject(reader);

                return new MLModel(coefficient.Slope, coefficient.Intercept);
            }
        }

        public double LeastAbsoluteError(double[] xAxis, double[] yAxis)
        {
            return xAxis.Zip(yAxis, (x, y) => Math.Abs(y - Predict(x))).Sum() / xAxis.Length;
        }

        public double LeastSquareError(double[] xAxis, double[] yAxis)
        {
            return xAxis.Zip(yAxis, (x, y) => Math.Pow(y - Predict(x), 2)).Sum() / xAxis.Length;
        }

        internal void GradientDescent(double[] xAxis, double[] yAxis)
        {
            double dSlope, dIntercept;
            int maxIterations = 1000;
            double learningRate = 0.01;
            int i = 0;
            do
            {
                dSlope = SlopeDerivate(xAxis, yAxis);
                dIntercept = InterceptDerivate(xAxis, yAxis);
                _coefficient.Slope -= learningRate * dSlope;
                _coefficient.Intercept -= learningRate * dIntercept;
                i++;
            } while (i < maxIterations);
        }

        internal double SlopeDerivate(double[] xAxis, double[] yAxis)
        {
            return xAxis.Zip(yAxis, (x, y) => (_coefficient.Slope * x + _coefficient.Intercept - y) * x).Sum() / xAxis.Length;
        }

        internal double InterceptDerivate(double[] xAxis, double[] yAxis)
        {
            return xAxis.Zip(yAxis, (x, y) => _coefficient.Slope * x + _coefficient.Intercept - y).Sum() / xAxis.Length;
        }
       

        /// <summary>
        /// Generates output predictions for the input data.
        /// </summary>
        /// <param name="inputData">The matrix representation of the input data.</param>
        /// <returns></returns>
        public double Predict(double[] inputData)
        {
            return 0.0;
        }


        /// <summary>
        /// Evaluates the model on the given data.
        /// </summary>
        /// <param name="inputData">The matrix representation of the input data.</param>
        /// <param name="targetData">The array representation of the target data.</param>
        /// <returns>Returns with the loss value on the given data.</returns>
        public double Evaluation(double[][] inputData, double[] targetData)
        {
            return 0.0;
        }
    }
}
