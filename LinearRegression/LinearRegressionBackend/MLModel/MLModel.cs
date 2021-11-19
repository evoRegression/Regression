using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml;

namespace MLModel
{

    public class MLModel : IMLModel
    {
        // Rename to myCoefficient or _coefficient
        private Coefficients coefficient;

        public MLModel(double Slope, double Intercept)
        {
            coefficient = new Coefficients(Slope, Intercept);
        }

        public MLModel()
        {
            coefficient = new Coefficients(0, 0);
        }

        public double Evaluation()
        {
            return 0;
        }

        public double Predict(double dataPoint)
        {
            return coefficient.Slope * dataPoint + coefficient.Intercept;
        }

        public void Train(double[][] data)
        {
            // Good job! Great algorithm, extract to SimpleOrdinaryLeastSquare method
            int N = data.Length;

            double[] xAxis = data.Select(array => (double)array.GetValue(0)).ToArray();
            double[] yAxis = data.Select(array => (double)array.GetValue(1)).ToArray();

            double sumX = xAxis.Sum();
            double sumY = yAxis.Sum();

            double numenator = N * xAxis.Zip(yAxis, (x, y) => x * y).Sum() - sumX * sumY;
            double denominator = N * xAxis.Select(x => x * x).Sum() - sumX * sumX;

            coefficient.Slope = numenator / denominator;
            coefficient.Intercept = sumY / N  - coefficient.Slope * sumX / N;
        }


        internal Coefficients SimpleOrdinaryLeastSquare(double[] xAxis, double[] yAxis)
        {

        }

        internal Coefficients QuadraticOrdinaryLeastSquare(double[] xAxis, double[] yAxis)
        {
            // https://en.wikipedia.org/wiki/Simple_linear_regression
        }

        public void Export(string path)
        {
            // Wrap the filestream with using keyword
            FileStream fs = new(path, FileMode.Create);
            // Wrape the writer with using keyword
            XmlDictionaryWriter writer = XmlDictionaryWriter.CreateTextWriter(fs);
            DataContractSerializer dcs = new DataContractSerializer(typeof(Coefficients));
            dcs.WriteObject(writer, coefficient);
            writer.Close();
            fs.Close();
        }

        public void Import(string path)
        {
            // Wrap the filestream with using keyword
            FileStream fs = new FileStream(path, FileMode.OpenOrCreate);
            XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(fs, new XmlDictionaryReaderQuotas());
            DataContractSerializer dcs = new DataContractSerializer(typeof(Coefficients));

            coefficient = (Coefficients)dcs.ReadObject(reader);

            fs.Close();
        }
    }
}
