using LinearRegressionBackend.DataProvider;
using LinearRegressionBackend.MLModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LinearRegressionBackend.MLContext
{
    public class MLContext : IMLContext
    {
        private IDataProvider dataProvider;
        private IMLModel model;
        private double[][] data;

        public void Init(IDataProvider dataProvider, IMLModel model)
        {
            this.dataProvider = dataProvider;
            this.model = model;

            data = dataProvider.Import(@"data.txt");       
        }

        public double Predict(double dataPoint)
        {
            return model.Predict(new double[] { dataPoint });
        }

        public void Train()
        {
            if (data == null || model == null)
                throw new NullReferenceException("The MLContext is not Initialized");
            double[] targetData = data.Select(array => (double)array.GetValue(1)).ToArray();
            List<History> hist = model.Fit(data, targetData, 100 );
        }
    }
}
