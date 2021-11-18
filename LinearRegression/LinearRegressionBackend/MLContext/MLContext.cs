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
            try
            {
                data = dataProvider.Import(@"data.txt");
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Environment.Exit(-1);
            }
        }

        public double Predict(double dataPoint)
        {
            return model.Predict(dataPoint);
        }

        public void Train()
        {
            if (data == null || model == null)
                throw new System.Exception("The MLContext is not Initialized");
            model.Train(data);
        }

    }
}
