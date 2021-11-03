using DataProvider;
using MLModel;

namespace MLContext 
{
    public class MLContext : IMLContext
    {
        private IDataProvider dataProvider;
        private IMLModel model;
        private int[] data;

        public void Init(IDataProvider dataProvider, IMLModel model)
        {
            this.dataProvider = dataProvider;
            this.model = model;
            try
            {
                this.data = this.dataProvider.Read(@"..\..\..\data.txt");
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                System.Environment.Exit(-1);
            }
        }

        public int Predict(int dataPoint)
        {
            return this.model.Predict(dataPoint);
        }

        public void Train()
        {
            if (this.data == null || this.model == null)
                throw new System.Exception("The MLContext is not Initialized");
            this.model.Train(this.data);
        }
    }
}
