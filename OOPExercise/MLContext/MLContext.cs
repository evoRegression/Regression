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
            this.data = this.dataProvider.Read("Placeholder text");

        }

        public int Predict(int dataPoint)
        {
            return this.model.Predict(dataPoint);
        }

        public void Train()
        {
            this.model.Train(this.data);
        }
    }
}
