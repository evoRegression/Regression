using DataProvider;
using MLModel;
using System;

namespace MLContext
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
            return model.Predict(dataPoint);
        }

        public void Train()
        {
            if (data == null || model == null)
                throw new System.NullReferenceException("The MLContext is not Initialized");
            model.Train(data);
        }
    }
}
