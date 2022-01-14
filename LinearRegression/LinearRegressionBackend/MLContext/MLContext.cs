using LinearRegressionBackend.DataProvider;
using LinearRegressionBackend.MLModel;
using System;

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
            double[] dataPointArray= new double[] { dataPoint };
            return model.Predict(dataPointArray);
        }

        public void Train()
        {
            if (data == null || model == null)
                throw new NullReferenceException("The MLContext is not Initialized");
            model.Train(data);
        }
    }
}
