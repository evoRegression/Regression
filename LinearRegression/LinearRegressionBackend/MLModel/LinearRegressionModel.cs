using LinearRegressionBackend.MLCommmons;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml;

namespace LinearRegressionBackend.MLModel
{

    public class LinearRegressionModel : IMLModel
    {
        internal Coefficients _coefficient;
        internal IOptimizer _optimizer;
        internal ILossFunction _lossFunction;

        public LinearRegressionModel( IOptimizer optimizer, ILossFunction lossFunction)
        {
            _coefficient = new Coefficients(0, 0);
            _optimizer = optimizer;
            _lossFunction = lossFunction;
        }

        public LinearRegressionModel(double Slope, double Intercept, IOptimizer optimizer, ILossFunction lossFunction)
        {
            _coefficient = new Coefficients(Slope, Intercept);
            _optimizer = optimizer;
            _lossFunction = lossFunction;
        }

        public double Evaluation(double[][] inputData, double[] targetData)
        {
            throw new NotImplementedException();
        }

        public List<History> Fit(double[][] inputData, double[] targetData, int epochs = 1)
        {
            List<History> history= new List<History>();

            if (_optimizer is QuadraticOrdinaryLeastSquare || _optimizer is SimpleOrdinaryLeastSquare || _optimizer is NormalEquation)
            {
                epochs = 1;
                _coefficient.Slope = 0;
                _coefficient.Intercept = 0;
            }
            
            for (int i = 0; i < epochs; i++)
            {
                double[] newThetas = _optimizer.Minimize(_lossFunction, _coefficient.getThetas(), inputData, targetData);
                _coefficient.Slope += newThetas[MLCommons.SLOPE_INDEX];
                _coefficient.Intercept += newThetas[MLCommons.INTERCEPT_INDEX];
                history.Add(new History(_lossFunction.Loss(newThetas, inputData, targetData), new double[] { _coefficient.Slope, _coefficient.Intercept }));
            }
            return history;
        }

        public double Predict(double[] inputData)
        {
            return _coefficient.Slope * inputData[0] + _coefficient.Intercept;
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

        public IMLModel Load(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(fs, new XmlDictionaryReaderQuotas());
                DataContractSerializer dcs = new DataContractSerializer(typeof(Coefficients));

                Coefficients coefficient = (Coefficients)dcs.ReadObject(reader);

                return new LinearRegressionModel(coefficient.Slope, coefficient.Intercept, null, null);
            }
        }

        public void setLearningRate(double learningRate)
        {
            if (_optimizer is GradientDescent)
            {
                ((GradientDescent)_optimizer)._learningRate = learningRate;
            }
        }

        public double getLearningRate()
        {
            if (_optimizer is GradientDescent)
            {
                return ((GradientDescent)_optimizer)._learningRate;
            }
            return 0;
        }
    }
}
