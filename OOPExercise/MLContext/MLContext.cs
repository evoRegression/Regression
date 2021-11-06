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
                // Hasonló módon, mint Balázsnál, be kell állítani a Copy Always-t. Majd lehet használni a "data.txt".
                this.data = this.dataProvider.Read(@"..\..\..\data.txt");
            }
            // Nyugodtan using-old be a System namespace. Ahogy egyik kollégám fogalmazta "Az anyanyelvet nem veszük ki" :D
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
