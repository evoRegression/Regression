using MLContext;
using DataProvider;
using MLModel;

namespace OOPExercise
{
    class Program
    {
        static void Main(string[] args)
        {
            IMLContext context = new MLContext.MLContext();
            IDataProvider dataProvider = new DataProvider.DataProvider();
            IMLModel model = new MLModel.MLModel();
            context.Init(dataProvider, model);
            context.Train();
            int prediction = context.Predict(100);
            System.Console.WriteLine($"Prediction: {prediction}");

        }
    }
}
